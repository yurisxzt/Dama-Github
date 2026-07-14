using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    private TcpClient cliente;
    private StreamWriter writer;

    void Start()
    {
        try
        {
            cliente = new TcpClient();

            cliente.Connect("127.0.0.1", 7777);

            Debug.Log("Conectado ao servidor!");

            writer = new StreamWriter(cliente.GetStream());

            writer.WriteLine("Olá servidor!");

            writer.Flush();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}