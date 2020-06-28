using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginmanager : MonoBehaviour,Imanager
{
    public login _login;

    string res = "";

    // Start is called before the first frame update
    void Start()
    {
        shuju.instance.manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shuju.instance._loginbl&&!this._login.GetA()) {
            this._login.SetA(true);
        }
        else if (shuju.instance._loginbl&&this._login.GetA()) {
            this._login.SetA(false);
        }
    }

    public void Manager(string name,object need)
    {
        switch (name) {

            case "login":
                this.res = this._login.Get();
                shuju.instance.login = true;
                print("login");
                break;

            case "regis":
                this.res = this._login.Get();
                shuju.instance.regis = true;
                break;

            case "setlogin":
                this._login = (login)need;
                break;
        }
    }

    public object Get(string need) {
        object res = null;
        switch (need) {
            case "idnpassword":
                res = this.res;
                break;
        }
        return res;
    }
}
