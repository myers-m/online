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
        if (this.name == "create") {
            Gamemanager.instance.createtable();
        }
        else if (this.name == "join") {
            Gamemanager.instance.jointable();
        }
        else if (this.name == "quit") {
            Gamemanager.instance.exit();
        }
        else if (this.name == "out") {
            Gamemanager.instance.outtable();
        }
    }
}
