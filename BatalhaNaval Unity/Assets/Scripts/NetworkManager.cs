using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    public bool startAsServer = false;
    public string host = "127.0.0.1";
    public int port = 7777;
    public int maxConnectAttempts = 10;

    private TcpClient tcpClient;
    private TcpListener tcpListener;
    private StreamWriter writer;
    private StreamReader reader;
    private Thread receiveThread;
    private readonly Queue<string> incomingMessages = new Queue<string>();
    private readonly object syncRoot = new object();
    private bool isConnected;
    private bool isRetryingConnect;
    private int connectAttemptCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // A inicialização acontece de forma explícita pelo Client,
        // para evitar conexão prematura com valores padrão.
    }

    public void InitializeNetworkMode()
    {
        if (startAsServer)
        {
            StartServer();
        }
        else
        {
            ConnectToServer();
        }
    }

    private void Update()
    {
        ProcessIncomingMessages();
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    public void StartServer()
    {
        if (tcpListener != null)
            return;

        if (isConnected || receiveThread != null)
            return;

        Thread serverThread = new Thread(ServerLoop);
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    public void ConnectToServer()
    {
        if (isConnected || isRetryingConnect)
            return;

        isRetryingConnect = true;

        try
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(host, port);

            writer = new StreamWriter(tcpClient.GetStream());
            reader = new StreamReader(tcpClient.GetStream());

            receiveThread = new Thread(ReceiveLoop);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            isConnected = true;
            isRetryingConnect = false;
            connectAttemptCount = 0;

            Debug.Log("Conectado ao servidor TCP.");
        }
        catch (Exception e)
        {
            connectAttemptCount++;

            if (connectAttemptCount <= maxConnectAttempts)
            {
                Debug.LogWarning(
                    $"Servidor ainda não respondeu em {host}:{port}. Tentativa {connectAttemptCount}/{maxConnectAttempts}. Detalhe: {e.Message}"
                );

                Invoke(nameof(ConnectToServer), 1f);
                return;
            }

            isRetryingConnect = false;

            Debug.LogError(
                $"Falha ao conectar ao servidor TCP em {host}:{port}. " +
                $"Verifique se alguma instância do jogo está com startAsServer = true e ouvindo nessa porta. Detalhe: {e.Message}"
            );
        }
    }

    private void ServerLoop()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();

            Debug.Log($"Servidor TCP ouvindo em {IPAddress.Any}:{port}.");

            tcpClient = tcpListener.AcceptTcpClient();
            writer = new StreamWriter(tcpClient.GetStream());
            reader = new StreamReader(tcpClient.GetStream());

            receiveThread = new Thread(ReceiveLoop);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            Debug.Log("Cliente conectado ao servidor TCP.");
        }
        catch (Exception e)
        {
            Debug.LogError(
                $"Erro no servidor TCP em {port}: {e.Message}. " +
                "Verifique se já existe outro servidor ouvindo nessa porta."
            );
        }
    }

    private void ReceiveLoop()
    {
        while (tcpClient != null && tcpClient.Connected)
        {
            try
            {
                string message = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(message))
                    continue;

                lock (syncRoot)
                {
                    incomingMessages.Enqueue(message);
                }
            }
            catch (Exception)
            {
                break;
            }
        }
    }

    private void ProcessIncomingMessages()
    {
        while (true)
        {
            string message = null;

            lock (syncRoot)
            {
                if (incomingMessages.Count == 0)
                    return;

                message = incomingMessages.Dequeue();
            }

            HandleIncomingMessage(message);
        }
    }

    private void HandleIncomingMessage(string message)
    {
        string[] parts = message.Split('|');

        if (parts.Length < 5)
            return;

        if (!parts[0].Equals("MOVE", StringComparison.OrdinalIgnoreCase))
            return;

        int fromRow;
        int fromColumn;
        int toRow;
        int toColumn;

        if (!int.TryParse(parts[1], out fromRow) ||
            !int.TryParse(parts[2], out fromColumn) ||
            !int.TryParse(parts[3], out toRow) ||
            !int.TryParse(parts[4], out toColumn))
        {
            return;
        }

        MoveManager.Instance.ApplyRemoteMove(
            fromRow,
            fromColumn,
            toRow,
            toColumn
        );
    }

    public void SendMove(int fromRow, int fromColumn, int toRow, int toColumn, bool isRed)
    {
        if (writer == null || tcpClient == null || !tcpClient.Connected)
            return;

        string message =
            $"MOVE|{fromRow}|{fromColumn}|{toRow}|{toColumn}|{isRed}";

        writer.WriteLine(message);
        writer.Flush();
    }

    public void Disconnect()
    {
        CancelInvoke(nameof(ConnectToServer));

        if (writer != null)
        {
            writer.Close();
            writer.Dispose();
        }

        if (reader != null)
        {
            reader.Close();
            reader.Dispose();
        }

        if (tcpClient != null)
        {
            tcpClient.Close();
        }

        if (tcpListener != null)
        {
            tcpListener.Stop();
        }

        isConnected = false;
        isRetryingConnect = false;
    }
}
