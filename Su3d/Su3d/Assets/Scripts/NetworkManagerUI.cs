using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private InputField _ipInput;
    public void Awake()
    {
        serverBtn.onClick.AddListener(StartServer);
        hostBtn.onClick.AddListener(StartHost);
        clientBtn.onClick.AddListener(StartClient);
    }
    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        UpdateButtonStates(false);
    }
    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        UpdateButtonStates(false);
    }
    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        UpdateButtonStates(false);
    }
    private void UpdateButtonStates(bool enabled)
    {
        serverBtn.interactable = enabled;
        hostBtn.interactable = enabled;
        clientBtn.interactable = enabled;
    }
}
