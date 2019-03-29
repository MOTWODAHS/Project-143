using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class NetworkingController : MonoBehaviour
{
    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipEnd;
    string sendStr;
    byte[] sendData = new byte[1024];

    //string connectAddress = null;

    void InitSocket()
    {
        
        ipEnd = new IPEndPoint(IPAddress.Parse(GetLocalIPv4()), 8001);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("waiting for sending UDP dgram");   
    }

    void SocketSend(string sendStr)
    {
        sendData = new byte[1024];   
        sendData = Encoding.ASCII.GetBytes(sendStr);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }

    void SocketQuit()
    {
        if (socket != null)
            socket.Close();

        // This part is for reciving the host IP
        /* 
        if(receiveThread != null)
        {
            receiveThread.Interrupt();
            receiveThread.Abort();
        }
        if(sock != null)
        {
            sock.Close();
        }
        */
        print("disconnect");
    }
    public void SendAction(int interactionCode, int selectedObjectNumber, string message)
    {
        string sendingMessage = interactionCode.ToString() + selectedObjectNumber.ToString() + message;
        sendData = new byte[1024];   
        sendData = Encoding.ASCII.GetBytes(sendingMessage);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }

    void Start()
    {
        InitSocket();
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }

    public static string GetLocalIPv4()
    {
        string hostName = Dns.GetHostName(); 
        IPHostEntry iPEntry = Dns.GetHostEntry(hostName);
        for (int i = 0; i < iPEntry.AddressList.Length; i++)
        {
            if (iPEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                return iPEntry.AddressList[i].ToString();
        }
        return null;
    }

    // This part is for reciving IP from the host
    /* 

    Socket sock;
    IPEndPoint iep;

    EndPoint ep;

    byte[] data;

    Thread receiveThread;
    void ReceiveIP()
    {
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        iep = new IPEndPoint(IPAddress.Any, 8002);
        sock.Bind(iep);
        ep = (EndPoint)iep;
        print("Ready to receive");

        receiveThread = new Thread(new ThreadStart(ReceiveOperate));
        receiveThread.Start();

    }

    void ReceiveOperate()
    {
        data = new byte[1024];
        int recv = sock.ReceiveFrom(data, ref ep);
        connectAddress = Encoding.ASCII.GetString(data,0,recv);
        print(connectAddress);
        // pattern matching here
        InitSocket();
        sock.Close();
    }

    */
}
