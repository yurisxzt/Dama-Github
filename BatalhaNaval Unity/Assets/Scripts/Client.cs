using UnityEngine;

public class Client : MonoBehaviour
{
    public enum NetworkRole
    {
        Server,
        Client
    }

    public NetworkRole role = NetworkRole.Client;
    public string host = "127.0.0.1";
    public int port = 7777;

    private bool startAsServer;

    private void Start()
    {
        ParseStartupMode();
        startAsServer = role == NetworkRole.Server;

        NetworkManager existingNetworkManager =
            GetComponent<NetworkManager>();

        if (existingNetworkManager == null)
        {
            existingNetworkManager =
                gameObject.AddComponent<NetworkManager>();
        }

        NetworkManager.Instance = existingNetworkManager;
        NetworkManager.Instance.startAsServer = startAsServer;
        NetworkManager.Instance.host = host;
        NetworkManager.Instance.port = port;
        NetworkManager.Instance.InitializeNetworkMode();

        Debug.Log(
            $"Rede iniciada como {(startAsServer ? "servidor" : "cliente")} em {host}:{port}."
        );
    }

    private void ParseStartupMode()
    {
        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].Equals("-server", System.StringComparison.OrdinalIgnoreCase))
            {
                role = NetworkRole.Server;
            }
            else if (args[i].Equals("-client", System.StringComparison.OrdinalIgnoreCase))
            {
                role = NetworkRole.Client;
            }
            else if (args[i].Equals("-host", System.StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
            {
                host = args[i + 1];
            }
            else if (args[i].Equals("-port", System.StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
            {
                int parsedPort;

                if (int.TryParse(args[i + 1], out parsedPort))
                {
                    port = parsedPort;
                }
            }
        }
    }
}