using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;
using System;

public class NetworkingController : MonoBehaviour
{
    /* This part is for UDP
    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipEnd;
    string sendStr;
    byte[] sendData;
    */
    IPAddress myIp;
    TcpClient client = new TcpClient();
    NetworkStream stream;
    StreamWriter sw = null;
    Thread connectServer;

    //string connectAddress = null;

    void InitSocket()
    {
        /* This part is for UDP
        ipEnd = new IPEndPoint(IPAddress.Parse(GetLocalIPv4()), 8001);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("waiting for sending UDP dgram");
        */
        myIp = IPAddress.Parse(GetLocalIPv4());
        connectServer = new Thread(ConnectToServer);
        connectServer.Start();
    }

    void ConnectToServer()
    {
        try
        {
            client.Connect(myIp,8001);
            stream = client.GetStream();
            sw = new StreamWriter(stream);
            print("Init TCP Client");
        }
        catch(Exception)
        {
            Thread.Sleep(5000);
            print("Reconnect");
            ConnectToServer();
        }
    }
    /*
    void SocketSend(string sendStr)
    {
        sendData = new byte[1048576];   
        sendData = Encoding.ASCII.GetBytes(sendStr);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }
     */

    void SocketQuit()
    {
        /* This part is for UDP
        if (socket != null)
            socket.Close();
        */

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
        client.Close();
        connectServer.Interrupt();
        connectServer.Abort();
        print("disconnect");
    }
    public void SendAction(int interactionCode, int selectedObjectNumber, string message)
    {
        /* This part is for UDP
        string sendingMessage = interactionCode.ToString() + selectedObjectNumber.ToString() + message;
        sendData = new byte[1048576];   
        sendData = Encoding.ASCII.GetBytes(sendingMessage);
        print(sendData.Length);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
        */
        string sendingMessage;
        if(interactionCode == 2)
        {
            sendingMessage = interactionCode.ToString() + (selectedObjectNumber/100).ToString() + ((selectedObjectNumber/10)%10).ToString() + (selectedObjectNumber%10).ToString() + message;
        }
        else
        {
            sendingMessage = interactionCode.ToString() + selectedObjectNumber.ToString() + message;
        }
        if(sw != null)
        {
            sw.WriteLine(sendingMessage);
            sw.Flush();
        }
    }

    void Start()
    {
        InitSocket();
    }

    public void InternetQuit()
    {
        SocketQuit();
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
