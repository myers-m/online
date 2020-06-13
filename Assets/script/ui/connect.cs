using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class connect : MonoBehaviour
{
    public GameObject text;

    public void settext(string text) {
        this.text.GetComponent<Text>().text = text;
    }
}
