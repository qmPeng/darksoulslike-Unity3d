using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManger : IActorManagerInterface
{

    private Collider weaponColL;
    private Collider weaponColR;
    //public ActorManger am;

    public GameObject whl;
    public GameObject whr;

    public WeaponController wcL;
    public WeaponController wcR;

    void Start()
    {
        //weaponCol = whr.GetComponentInChildren<Collider>();
        ////print(transform.DeepFind("arm left shoulder 1").name);
        //print(transform.DeepFind("weaponHandleL"));
        whl = transform.DeepFind("weaponHandleL").gameObject;
        whr = transform.DeepFind("weaponHandleR").gameObject;

        wcL = BindWeaponController(whl);
        wcR = BindWeaponController(whr);

        weaponColL = whl.GetComponentInChildren<Collider>();
        weaponColR = whr.GetComponentInChildren<Collider>();

    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if(tempWc == null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;
        return tempWc;
    }

	public void WeaponEnable()
    {
        if(am.ac.CheckStateTag("attackL")){
            weaponColL.enabled = true;
        }else{
            weaponColR.enabled = true;
        }
        
        
        //print("WeaponEnable");
    }
    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        weaponColL.enabled = false;
        //print("WeaponDisable");
    }

    public void counterBackEnable()
    {
        am.SetIsCounterBack(true);
    }
    public void counterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
}
