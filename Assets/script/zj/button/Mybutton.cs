using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mybutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button bt = this.GetComponent<Button>();
        bt.onClick.AddListener(delegate() {
            this.onclick();
        });
    }

    private void onclick() {
        switch (this.name)
        {
            case "create":
                Gamemanager.instance.createtable();
                break;

            case "join":
                Gamemanager.instance.jointable();
                break;

            case "quit":
                Gamemanager.instance.exit();
                break;

            case "out":
                Gamemanager.instance.outtable();
                break;

            case "login":
                shuju.instance.manager.Manager("login",null);
                break;

            case "regis":
                shuju.instance.manager.Manager("regis",null);
                break;
        }
    }
}
