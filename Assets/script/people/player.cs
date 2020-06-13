using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class player : people
{
    public GameObject camera;
    CharacterController m_ch;

    Vector3 m_eu;
    float m_y;

    float yspeed = 0;
    float gravity = 9.8f;

    public Vector3 oldposition = new Vector3(0,0,0);
    public Vector3 nowposition = new Vector3(0,0,0);
    public Quaternion rotation = new Quaternion();
    // Start is called before the first frame update
    void Start()
    {
        this.m_ch = this.gameObject.AddComponent<CharacterController>();
        this.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0.5f, 1f));
        this.init();
        this.oldposition = this.transform.position;
        this.nowposition = this.transform.position;
        this.rotation = this.transform.rotation;
        this.m_eu = this.transform.eulerAngles;
        shuju.instance.player = this;
        Tcp.instance.trans();
    }

    protected override void control()
    {
        this.vmove.Set(shuju.instance.control.direction.x, 0, shuju.instance.control.direction.y);
        this.vmove = this.transform.TransformDirection(this.vmove) * this.speed * Time.deltaTime;
        if (this.m_ch.isGrounded)
        {
            this.yspeed = 0;
            if (shuju.instance.control.jump) {
                this.yspeed = 10;
                shuju.instance.control.jump = false;
            }
        }
        else {
            this.yspeed -= this.gravity * Time.deltaTime;
            shuju.instance.control.jump = false;
        }
        this.vmove.y = this.yspeed * Time.deltaTime;
        this.oldposition = this.transform.position;
        this.move();
        this.nowposition = this.transform.position;

        this.m_y -= shuju.instance.control.Cdirection.y * 0.01f;
        this.m_y = (float)Math.Max(-Math.PI / 2, this.m_y);
        this.m_y = (float)Math.Min(Math.PI / 2, this.m_y);
        this.m_eu.y += shuju.instance.control.Cdirection.x;
        this.transform.eulerAngles = this.m_eu;
        this.camera.transform.position = this.transform.TransformPoint(new Vector3(0, (float)Math.Sin(this.m_y) * 0.5f + 1, -2));
        shuju.instance.control.Cdirection.Set(0, 0);
        this.camera.transform.LookAt(this.transform.position);
        this.rotation = this.transform.rotation;
    }

    public void move()
    {
        this.m_ch.Move(this.vmove);
    }
}
