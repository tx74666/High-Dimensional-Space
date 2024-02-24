using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class TCPClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    void Start()
    {
        ConnectToServer();
    }


    void ConnectToServer()
    {
        try
        {
            // 连接服务器
            client = new TcpClient();
            client.Connect("127.0.0.1", 8000);
            Debug.Log("Connected to server.");
            stream = client.GetStream();
            Receive();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.ToString());
        }
    }

    public void Send(string message)
    {
        if (client != null && client.Connected)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }
    public void Receive()
    {
        byte[] buffer = new byte[1024];
        stream.BeginRead(buffer,0,buffer.Length,new AsyncCallback(OnReceive),buffer);
    }
    public void OnReceive(IAsyncResult ar)
    {
        try
        {
            int bytesRead = stream.EndRead(ar);
            if (bytesRead > 0)
            {
                string message = Encoding.UTF8.GetString((byte[])ar.AsyncState, 0, bytesRead);
                Debug.Log("Done:"+message);
                Receive();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Receive error:" + e.ToString());
        }
    }
}
