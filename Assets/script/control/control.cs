using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    public GameObject cmp;
    public GameObject cmq;
    public GameObject jumpp;

    RectTransform cmqRT;
    RectTransform cmpRT;
    RectTransform jumpRT;

    Vector2 size;
    Vector2 yd = new Vector2(0,0);

    public Vector2 direction = new Vector2(0,0);
    public Vector2 Cdirection = new Vector2(0,0);

    public bool jump = false;

    private void Start()
    {
        this.size = new Vector2(1.0f / 75.0f, 1.0f / 75.0f);
        this.cmqRT = this.cmq.GetComponent<Image>().rectTransform;
        this.cmpRT = this.cmp.GetComponent<Image>().rectTransform;
        this.jumpRT = this.jumpp.GetComponent<Image>().rectTransform;
    }

    Vector2 need;
    // Update is called once per frame
    void Update()
    {
        bool touchdir = false;
        for (int i=0;i<Input.touchCount;i++) {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[i].fingerId))
            {
                this.need = this.cmpRT.InverseTransformPoint(Input.touches[i].position);
                if (Math.Abs(Vector2.Distance(this.yd, this.need)) < 75)
                {
                    this.cmqRT.anchoredPosition = this.need;
                    this.need.Scale(this.size);
                    this.direction = this.need;
                    touchdir = true;
                }
                else
                {
                    this.need = this.jumpRT.InverseTransformPoint(Input.touches[i].position);
                    if (Math.Abs(Vector2.Distance(this.yd, this.need)) < 37.5)
                    {
                        this.jump = true;
                    }
                }
            }
            else {
                this.Cdirection = Input.touches[i].deltaPosition;
            }
        }

        if (!touchdir)
        {
            this.cmqRT.anchoredPosition = yd;
            this.direction.Set(0, 0);
        }
    }
}
