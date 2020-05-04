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
        if (m_Driver.IsCreated)
            m_Driver.ScheduleUpdate().Complete();

        if (!m_Connection.IsCreated && m_Driver.IsCreated) {
            if (!done) {
                Debug.Log("CLIENT: Something went wrong during connection");
            }
            return;
        }

        if (m_Connection.IsCreated) {

            

            DataStreamReader stream;
            NetworkEvent.Type cmd;
            while ((cmd = m_Connection.PopEvent(m_Driver, out stream)) != NetworkEvent.Type.Empty) {
                if (cmd == NetworkEvent.Type.Connect) {
                    Debug.Log("CLIENT: Connection with server successful");
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.Space)) {
            StartClient();
        }
    }

    private void StartClient() {
        m_Driver = NetworkDriver.Create();
        m_Connection = default(NetworkConnection);

        NetworkEndPoint endpoint = NetworkEndPoint.LoopbackIpv4;
        endpoint.Port = 42422;
        m_Connection = m_Driver.Connect(endpoint);
    }

    private void OnDestroy() {
        

        if (m_Driver.IsCreated) {
            Debug.Log("Disposing of client driver");
            //m_Driver.Disconnect(m_Connection);

            m_Driver.Dispose();
        }
        
        
    }
}
