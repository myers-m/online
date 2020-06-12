using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class connect : MonoBehaviour
{
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void settext(string text) {
        this.text.GetComponent<Text>().text = text;
    }
}
