using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class BattleManger : IActorManagerInterface
{

    //public ActorManger am;
    private CapsuleCollider defcol;

    void Start()
    {
        defcol = GetComponent<CapsuleCollider>();
        defcol.center = Vector3.up * 1.0f;
        defcol.height = 2.0f;
        defcol.radius = 0.5f;
        defcol.isTrigger = true;
    }

    void OnTriggerEnter(Collider col)
    {
        //print(col.name);

        WeaponController targetWc = col.GetComponentInParent<WeaponController>();
        if(targetWc == null)
        {
            //ActorController targetAc = col.GetComponentInParent<ActorController>();
            return;
        }

        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - receiver.transform.position;

        float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir);
        float countAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        float countAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);//should be closed to 180 degree

        bool attackVaild = (attackingAngle1 < 45);
        bool counterValid = (countAngle1 < 30 && Mathf.Abs(countAngle2 - 180) < 30);
        

        if (col.tag == "Weapon")
        {
            if (attackingAngle1 <= 45)
            {
                am.TryDoDamge(targetWc, attackVaild, counterValid);
            }
           
        }
        if (col.name == "caster")
        {
            if (attackingAngle1 <= 45)
            {
                //am.DoStab(targetAc, attackVaild, counterValid);
            }
        }
    }

}
