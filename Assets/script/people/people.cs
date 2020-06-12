using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people : MonoBehaviour,ipeople
{
    protected Vector3 vmove = new Vector3(0,0,0);

    protected CharacterController m_ch;
    

    protected float speed = 4;

    // Start is called before the first frame update
    protected void Start()
    {
        this.m_ch = this.gameObject.AddComponent<CharacterController>();
    }

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

    public void move()
    {
        this.m_ch.Move(this.vmove);
    }
}
