using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initmanager : MonoBehaviour,Imanager
{
    bool connect = false;

    private void Start()
    {
        shuju.instance.manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Tcp.instance!=null&&shuju.instance!=null&&Gamemanager.instance!=null&&this.connect) {
            Gamemanager.instance.loadscene("newbegin");
        }
    }

    public object Get(string need)
    {
        throw new System.NotImplementedException();
    }

    public void Manager(string name, object need)
    {
        switch (name) {
            case "setconnect":
                this.connect = true;
                break;
        }
    }
}
