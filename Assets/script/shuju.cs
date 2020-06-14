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

    public List<other> other;
    public tablemanager table;
    public control control;

    public bool pause = true;

    public bool connect = true;
    public int createtable = -1;

    public bool wait = false;

    public bool intable = false;
    public bool outtable = false;

    public bool begin = true;

    public int tid = 0;

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
