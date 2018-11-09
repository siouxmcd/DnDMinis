using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetwork : NetworkBehaviour
{
    public Text ipTExt;
    public Text portText;

    public void StartServer()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        if (ipTExt.text.Length > 0 && ipTExt.text != null)
        {
            NetworkManager.singleton.networkAddress = ipTExt.text;
            int x;
            int.TryParse(portText.text, out x);
            NetworkManager.singleton.networkPort = x;
        }
        NetworkManager.singleton.StartClient();
    }
    

}
