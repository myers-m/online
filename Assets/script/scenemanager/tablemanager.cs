using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class tablemanager : MonoBehaviour
{
    public GameObject other;
    public List<int> id = new List<int>();
    public List<Color> color = new List<Color>();

    public List<int> rid = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        shuju.instance.control = GameObject.FindGameObjectWithTag("control").GetComponent<control>();
        shuju.instance.table = this;
        new Thread(new ThreadStart(locktcp)).Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.id.Count > 0)
        {
            while (this.id.Count > 0)
            {
                print("create");
                this.createother(id[0], color[0]);
                this.id.RemoveAt(0);
                this.color.RemoveAt(0);
            }
        }
        else if (this.rid.Count > 0)
        {
            while (this.rid.Count > 0)
            {
                int id = this.rid[0];
                int oid = shuju.instance.findid(id);
                //print(shuju.instance.other.Count);
                //print(id);
                //print(shuju.instance.findid(id));
                //print(this.rid.Count);
                if (oid!=-1)
                {
                    print("killbg");
                    print(shuju.instance.other.Count);
                    shuju.instance.other[oid].destory=true;
                    shuju.instance.other.RemoveAt(oid);
                    print(shuju.instance.other.Count);
                    print("kill");
                }
                this.rid.RemoveAt(0);
            }
        }
        else if(Tcp.instance.waitbl){
            Tcp.instance.waitbl = false;
        }
    }

    void locktcp() {
        while (shuju.instance.player==null) {
            //print("null");
        }
        shuju.instance.intable = true;
        shuju.instance.pause = false;
    }

    public void createother(int id,Color color) {
        GameObject gob = GameObject.Instantiate(other);
        gob.GetComponent<other>().id = id;
        gob.GetComponent<other>().color = color;
    }
}
