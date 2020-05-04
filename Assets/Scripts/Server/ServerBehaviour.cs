using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class ServerBehaviour : MonoBehaviour
{
    public NetworkDriver m_driver;
    private NativeList<NetworkConnection> m_connections;

    private bool isServerRunning = false;

    void Start() {
        
    }


    

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return)) {
            StartServer();
        }

        if (m_driver.IsCreated) {

            m_driver.ScheduleUpdate().Complete();

            //Clean up connections
            for (int i = 0; i < m_connections.Length; i++) {
                if (!m_connections[i].IsCreated) {
                    m_connections.RemoveAtSwapBack(i);
                    --i;
                }
            }

            //Accept new connections
            NetworkConnection c;
            while ((c = m_driver.Accept()) != default(NetworkConnection)) {
                m_connections.Add(c);
                Debug.Log("Accepted a connection");
            }

            //Process messages
            DataStreamReader stream;
            for (int i = 0; i < m_connections.Length; i++) {
                if (!m_connections[i].IsCreated)
                    continue;
                NetworkEvent.Type cmd;
                while ((cmd = m_driver.PopEventForConnection(m_connections[i], out stream)) != NetworkEvent.Type.Empty) {

                    if (cmd == NetworkEvent.Type.Data) {
                        Debug.Log("Data message received");
                    }
                    else if (cmd == NetworkEvent.Type.Disconnect) {
                        Debug.Log("Client disconnected from server");
                        m_connections[i] = default(NetworkConnection);
                    }

                }
            }
        }

    }


    public void StartServer() {
        m_driver = NetworkDriver.Create();
        var endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = 42422;

        if (m_driver.Bind(endpoint) != 0) {
            Debug.Log("Failed to bind server to port " + endpoint.Port);
        }
        else {
            m_driver.Listen();
            Debug.Log("Server Listening...");
        }

        m_connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
    }

    private void OnDestroy() {
        if (m_driver.IsCreated) {
            Debug.Log("Disposing of server driver");
            m_driver.Dispose();
        }


        if (m_connections.IsCreated) {
            Debug.Log("Disposing of connections list");
            m_connections.Dispose();
        }
    }
}
