using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wait : MonoBehaviour
{
    public GameObject waitvalue;
    int value = 0;

    private void Start()
    {
        //print("this");
        Gamemanager.instance.waitload();
    }

    // Update is called once per frame
    void Update()
    {
        if (value < (int)(Gamemanager.instance.pro.progress * 100))
        {
            value++;
            waitvalue.GetComponent<Text>().text = "进度:" + value + "%";
            Gamemanager.instance.pro.allowSceneActivation = false;
        }
        else
        {
            Gamemanager.instance.pro.allowSceneActivation = true;
        }
    }
}
