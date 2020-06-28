using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    public GameObject _login;
    public GameObject _uid;
    public GameObject _password;


    // Start is called before the first frame update
    void Start()
    {
        shuju.instance.manager.Manager("setlogin",this);
        this._login.SetActive(false);
    }

    float speed = 0;
    void Update()
    {
        speed += 10f * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", speed);
    }

    public void SetA(bool need) {
        this._login.SetActive(need);
    }

    public bool GetA() {
        return this._login.active;
    }

    public string Get() {
        return this._uid.GetComponent<Text>().text + "|" + this._password.GetComponent<InputField>().text;
    }
}
