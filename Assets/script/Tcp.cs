using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Tcp : MonoBehaviour
{
    public static Tcp instance;


    IPAddress ip;
    IPEndPoint ipEnd;
    Thread connectThread;
    Socket serverSocket;

    byte[] recvData = new byte[1024];
    int recvLen;
    string recvStr;

    float time = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.InitSocket();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (this.time >= 5 && shuju.instance.connect)
        {
            shuju.instance.connectui.SetActive(true);
            shuju.instance.connectui.GetComponent<connect>().settext("get out now!");
            shuju.instance.connect = false;
        }
        else {
            this.time += Time.deltaTime;
        }
    }

    private void OnApplicationQuit()
    {
        print("quit********************");
        this.SocketQuit();
    }

    void InitSocket() {
        this.ip = IPAddress.Parse("127.0.0.1");
        this.ipEnd = new IPEndPoint(ip, 8080);
        this.connectThread = new Thread(new ThreadStart(SocketReceive));
        this.connectThread.Start();
    }

    void SocketReceive()
    {
        if (this.SocketConnet())
        {
            this.set("connect");
            while (true)
            {
                this.get();
                if (recvLen == 0)
                {
                    continue;
                }
                this.time = 0;
                this.doing();
            }
        }
        else {
            this.time = 5;
        }
    }

    bool SocketConnet()
    {
        if (this.serverSocket != null)
        {
            this.serverSocket.Close();
        }
        //定义套接字类型,必须在子线程中定义
        this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //连接
        this.serverSocket.Connect(ipEnd);

        int time=System.DateTime.Now.DayOfYear*1000000+System.DateTime.Now.Hour*10000+System.DateTime.Now.Minute*100+System.DateTime.Now.Second;
        this.serverSocket.Send(Encoding.ASCII.GetBytes("" + time + "-0"));
        //输出初次连接收到的字符串
        this.recvLen = this.serverSocket.Receive(this.recvData);
        this.recvStr = Encoding.ASCII.GetString(this.recvData, 0, this.recvLen);
        if (recvStr == "connect success")
        {
            return true;
        }
        else {
            return false;
        }
    }

    void SocketQuit()
    {
        this.serverSocket.Send(Encoding.ASCII.GetBytes("quit" + "-0"));
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭服务器
        if (serverSocket != null)
            serverSocket.Close();
    }

    void get()
    {
        this.recvData = new byte[1024];
        this.recvLen = serverSocket.Receive(recvData);
        this.recvStr = Encoding.ASCII.GetString(this.recvData, 0, this.recvLen);
    }

    void doing(){
        if (shuju.instance.intable) {

        }
        else {
            this.set("connect");
        }
    }

    void set(string need) {
        this.serverSocket.Send(Encoding.ASCII.GetBytes(need + "-0"));
    }
}
