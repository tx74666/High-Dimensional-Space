using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private Text statusText; // Assuming you have a Text component to show status

    private void Awake()
    {
        serverBtn.onClick.AddListener(StartServer);
        hostBtn.onClick.AddListener(StartHost);
        clientBtn.onClick.AddListener(StartClient);
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        UpdateUI("Server started");
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        UpdateUI("Host started");
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        UpdateUI("Client started");
    }

    private void UpdateUI(string status)
    {
        statusText.text = status;
        serverBtn.gameObject.SetActive(false);
        hostBtn.gameObject.SetActive(false);
        clientBtn.gameObject.SetActive(false);
    }
}
