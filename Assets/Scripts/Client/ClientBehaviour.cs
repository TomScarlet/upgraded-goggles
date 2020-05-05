using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{

    public NetworkDriver m_Driver;
    public NetworkConnection m_Connection;
    public bool done;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            StartClient();
        }*/
    }

    public void StartClient() {
        m_Driver = NetworkDriver.Create();
        m_Connection = default(NetworkConnection);

        NetworkEndPoint endpoint = NetworkEndPoint.LoopbackIpv4;
        endpoint.Port = 42424;
        m_Connection = m_Driver.Connect(endpoint);

        UI_Manager.Instance.SetScreen(E_Screens.Lobby);
    }


    private void OnDestroy() {
        if (m_Driver.IsCreated) {
            Debug.Log("Disposing of client driver");
            m_Driver.Dispose();
        }
    }
}
