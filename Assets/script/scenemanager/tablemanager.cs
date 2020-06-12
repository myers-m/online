using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablemanager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        shuju.instance.control = GameObject.FindGameObjectWithTag("control").GetComponent<control>();
        shuju.instance.pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
