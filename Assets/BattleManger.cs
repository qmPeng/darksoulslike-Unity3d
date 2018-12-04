using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class BattleManger : MonoBehaviour {

    public ActorManger am;
    private CapsuleCollider defcol;

    void Start()
    {
        defcol = GetComponent<CapsuleCollider>();
        defcol.center = Vector3.up * 1.0f;
        defcol.height = 2.0f;
        defcol.radius = 0.25f;
        defcol.isTrigger = true;
    }

    void OnTriggerEnter(Collider col)
    {
        //print(col.name);
        if(col.tag == "Weapon")
        {
            am.DoDamge();
        }
    }

}
