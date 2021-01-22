using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGUI : MonoBehaviour
{
    NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    public void StartServer()
    {
        networkManager.StartServer();
    }

    public void StartClient()
    {
        networkManager.StartClient();
    }

    public void SetAddress(string addr)
    {
        networkManager.networkAddress = addr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
