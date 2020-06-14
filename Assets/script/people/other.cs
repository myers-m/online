using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class other : people
{
    public Vector3 oldposition = new Vector3(0,0,0);
    public Vector3 position = new Vector3(0,0,0);
    public Quaternion rotation = new Quaternion();

    public int id = 0;

    public bool destory = false;

    private void Start()
    {
        this.init();
        shuju.instance.other.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.destory) {
            GameObject.Destroy(this.gameObject);
        }
        this.transform.position = oldposition;
        this.transform.rotation = this.rotation;
        this.move();
    }

    public void setvalue(Vector3 oldposition,Vector3 position,Quaternion rotation) {
        this.oldposition = oldposition;
        this.position = position;
        this.rotation = rotation;
        this.vmove = position - oldposition;
    }

    void move() {
        this.transform.position += vmove;
    }
}
