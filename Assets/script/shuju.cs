using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuju : MonoBehaviour
{
    public static shuju instance;

    public player player;
    public GameObject text;
    public GameObject connectui;

    public bool _loginbl = false;

    public List<other> other;
    public Imanager manager;
    public control control;

    public bool pause = true;

    public bool connect = true;

    public bool wait = false;

    public bool intable = false;
    public bool outtable = false;

    public bool begin = true;

    public bool createtable = false;
    public bool jointable = false;
    public bool login = false;
    public bool regis = false;
    public bool await = false;
    public bool doasync = false;

    public int tid = 0;
    public float FPS = 60;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    public int findid(int id) {
        int res = -1;
        //print(this.other.Count);
        for (int i=0;i<this.other.Count;i++)
        {
            //print(this.other.Count);
            if (this.other[i].id==id) {
                res = i;
                break;
            }
        }
        //print(res);
        return res;
    }
}
