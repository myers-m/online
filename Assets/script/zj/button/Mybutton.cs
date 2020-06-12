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

        }
        else if (this.name == "join") {

        }
        else if (this.name == "quit") {
            Gamemanager.instance.exit();
        }
    }
}
