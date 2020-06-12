using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuju : MonoBehaviour
{
    public static shuju instance;

    public GameObject player;
    public GameObject text;
    public GameObject connectui;

    public control control;

    public bool pause = true;

    public bool tcp = true;
    public bool connect = true;

    public bool intable = false;

    public bool change = false;
    public int life = 10;
    public int score = 0;

    public int maxscore = -1;

    public List<int> list=new List<int>();

    private void Awake()
    {
        instance = this;
    }
}
