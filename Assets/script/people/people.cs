using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people : MonoBehaviour,ipeople
{
    protected Vector3 vmove = new Vector3(0,0,0);

    protected float speed = 4;

    public Color color = new Color();

    // Update is called once per frame
    void Update()
    {
        if (!shuju.instance.pause)
        {
            this.control();
        }
    }

    protected virtual void control() {
        
    }

    

    public void init() {
        this.GetComponent<MeshRenderer>().material.color = this.color;
    }
}
