﻿using UnityEngine;
using System.Collections;

public class OnGroundSensor : MonoBehaviour
{

    public CapsuleCollider capcol;
    public float offset = 0.1f;


    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    // Use this for initialization
    void Awake()
    {
        radius = capcol.radius - 0.05f;
        //print(radius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        point1 = transform.position + transform.up * (radius - offset);
        point2 = transform.position + transform.up * (capcol.height - offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));
        if (outputCols.Length != 0)
        {
            //foreach (var col in outputCols)
            //{
            //    print("collision:" + col.name);
            //}
            SendMessageUpwards("IsGround");

        }
        else {
            SendMessageUpwards("IsNotGround");
        }
    }
}
