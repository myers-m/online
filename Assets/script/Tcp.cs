using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Tcp : MonoBehaviour
{
    public static Tcp instance;
    Transform m_transform;

    IPAddress ip;
    IPEndPoint ipEnd;
    Thread connectThread;
    Socket serverSocket;

    byte[] recvData = new byte[1024];
    int recvLen;
    string recvStr;

    float time = 0;

    bool run = true;
    public bool waitbl=false;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(shuju.instance.connectui);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.InitSocket();
    }

    public void trans() {
        this.m_transform = shuju.instance.player.transform;
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
        this.ip = IPAddress.Parse("49.232.170.103");
        //this.ip = IPAddress.Parse("127.0.0.1");
        this.ipEnd = new IPEndPoint(ip, 8080);
        this.connectThread = new Thread(new ThreadStart(SocketReceive));
        this.connectThread.Start();
    }

    void SocketReceive()
    {
        if (this.SocketConnet())
        {
            this.set("connect");
            while (this.run)
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

        int time=System.DateTime.Now.DayOfYear*1000000+System.DateTime.Now.Hour*10000+System.DateTime.Now.Minute*100+System.DateTime.Now.Second+1;
        this.serverSocket.Send(Encoding.ASCII.GetBytes("" + time + "-0"));
        //输出初次连接收到的字符串
        this.recvLen = this.serverSocket.Receive(this.recvData);
        this.recvStr = Encoding.ASCII.GetString(this.recvData, 0, this.recvLen);
        if (recvStr == "connect success")
        {
            shuju.instance.manager.Manager("setconnect", null);
            return true;
        }
        else {
            return false;
        }
    }

    void SocketQuit()
    {
        this.serverSocket.Send(Encoding.ASCII.GetBytes("quit" + "-0"));
        this.run = false;
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
        //print(this.recvStr);
    }

    string[] need0;
    string[] need;
    void doing(){
        if (shuju.instance.await) {
            if (!shuju.instance.intable) {
                if (shuju.instance.login) {
                    if (this.recvStr.Equals("login success"))
                    {
                        shuju.instance._loginbl = true;
                    }
                    shuju.instance.login = false;
                }
                else if (shuju.instance.regis) {
                    if (this.recvStr.Equals("regis success"))
                    {
                        print("this");
                        shuju.instance._loginbl = true;
                    }
                    shuju.instance.regis = false;
                }
                else if (shuju.instance.createtable) {
                    if (this.recvStr.Contains("create table success")) {
                        shuju.instance.tid = int.Parse(this.recvStr.Replace("create table success", ""));
                        shuju.instance.doasync = true;
                        print("create table success");
                    }
                    else{
                        print("create table lose");
                        shuju.instance.createtable = false;
                    }
                }
                else if (shuju.instance.jointable) {
                    if (this.recvStr.Contains("join success")) {
                        shuju.instance.tid = int.Parse(this.recvStr.Replace("join success", ""));
                        shuju.instance.doasync = true;
                    }
                    else{
                        print("join lose");
                        shuju.instance.jointable = false;
                    }
                }
            }
            shuju.instance.await = false;
        }

        if (shuju.instance.outtable && !shuju.instance.wait && shuju.instance.intable)
        {
            this.set("out table");
            shuju.instance.wait = true;
        }
        else if (shuju.instance.outtable && shuju.instance.wait)
        {
            if (this.recvStr.Equals("out table success"))
            {
                shuju.instance.intable = false;
                shuju.instance.wait = false;
            }
            else
            {
                shuju.instance.outtable = false;
                shuju.instance.wait = false;
            }
            //print(this.recvStr);
            this.set("connect");
        }
        else if (shuju.instance.intable && shuju.instance.wait)
        {
            shuju.instance.wait = false;
            this.set("intable" + "," + shuju.instance.player.color.r + "|" + shuju.instance.player.color.g + "|" + shuju.instance.player.color.b + "|" + shuju.instance.player.color.a);
        }
        else if (shuju.instance.intable && !shuju.instance.wait)
        {
            this.need0 = this.recvStr.Split('%');
            if (!this.need0[1].Equals(""))
            {
                for (int i = 1; i < need0.Length; i++)
                {
                    if (need0[i].Contains("out"))
                    {
                        string res = this.need0[i].Replace("out", "");
                        shuju.instance.manager.Manager("addrid", int.Parse(res));
                        this.waitbl = true;
                    }
                    else if (need0[i].Contains("in"))
                    {
                        string res = this.need0[i].Replace("in", "");
                        string[] res1 = res.Split(',');
                        int id = int.Parse(res1[0]);
                        string[] res2 = res1[1].Split('|');
                        print(res);
                        shuju.instance.manager.Manager("addcolor", new Color(float.Parse(res2[0]), float.Parse(res2[1]), float.Parse(res2[2]), float.Parse(res2[3])));
                        shuju.instance.manager.Manager("addid", id);
                        this.waitbl = true;
                    }
                }
                if (this.waitbl)
                {
                    this.wait();
                }
            }

            if (!this.need0[0].Equals(""))
            {
                this.need = this.need0[0].Split('_');
                for (int i = 0; i < need.Length; i++)
                {
                    string[] need1 = need[i].Split('|');
                    int id = int.Parse(need1[0]);
                    Vector3 oldposition = new Vector3(float.Parse(need1[1]), float.Parse(need1[2]), float.Parse(need1[3]));
                    Vector3 position = new Vector3(float.Parse(need1[4]), float.Parse(need1[5]), float.Parse(need1[6]));
                    Quaternion rotation = new Quaternion(float.Parse(need1[7]), float.Parse(need1[8]), float.Parse(need1[9]), float.Parse(need1[10]));
                    //print(shuju.instance.other.Count);
                    //print(shuju.instance.findid(id));
                    int needid = -1;
                    if ((needid = shuju.instance.findid(id)) != -1)
                    {
                        shuju.instance.other[needid].setvalue(oldposition, position, rotation);
                    }
                }
            }

            string value = "" + shuju.instance.player.oldposition.x + "|" + shuju.instance.player.oldposition.y + "|" + shuju.instance.player.oldposition.z +
                "|" + shuju.instance.player.nowposition.x + "|" + shuju.instance.player.nowposition.y + "|" + shuju.instance.player.nowposition.z
                + "|" + shuju.instance.player.rotation.x + "|" + shuju.instance.player.rotation.y + "|" + shuju.instance.player.rotation.z + "|" + shuju.instance.player.rotation.w;
            this.set(value);
        }
        else
        {
            if (shuju.instance.login)
            {
                shuju.instance.await = true;
                this.set("login"+ (string)shuju.instance.manager.Get("idnpassword"));
            }
            else if (shuju.instance.regis)
            {
                shuju.instance.await = true;
                this.set("regis"+(string)shuju.instance.manager.Get("idnpassword"));
            }
            else if (shuju.instance.createtable && !shuju.instance.await &&!shuju.instance.doasync)
            {
                shuju.instance.await = true;
                this.set("create table");
            }
            else if (shuju.instance.jointable && !shuju.instance.await &&!shuju.instance.doasync)
            {
                shuju.instance.await = true;
                this.set("join table");
            }
            else
            {
                this.set("connect");
            }
        }
    }

    void wait() {
        while (this.waitbl) { 
        
        }
    }

    void set(string need) {
        print(need);
        this.serverSocket.Send(Encoding.ASCII.GetBytes(need + "-0"));
    }
}
