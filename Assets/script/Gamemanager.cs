using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public AsyncOperation pro;

    string waitname = "";

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = (int)shuju.instance.FPS;
    }

    void Update()
    {
        if ((shuju.instance.createtable || shuju.instance.jointable) && shuju.instance.doasync)
        {
            this.loadscene("table");
            shuju.instance.createtable = false;
            shuju.instance.jointable = false;
            shuju.instance.doasync = false;
        }
        else if (shuju.instance.outtable && !shuju.instance.intable) {
            shuju.instance.tid = 0;
            shuju.instance.other.Clear();
            shuju.instance.outtable = false;
            this.loadscene("newbegin");
            print("this");
        } 
    }

    public void loadscene(string name) {
        this.waitname = name;
        shuju.instance.wait = true;
        SceneManager.LoadScene("wait");
    }

    public void waitload()
    {
        this.pro = SceneManager.LoadSceneAsync(waitname);
    }

    public void exit() {
        Application.Quit();
    }

    public void createtable() {
        shuju.instance.createtable = true;
    }

    public void jointable() {
        shuju.instance.jointable = true;
    }

    public void outtable() {
        shuju.instance.outtable = true;
    }
}
