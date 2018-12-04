using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManger : MonoBehaviour {

    public ActorController ac;
    public BattleManger bm;
    public WeaponManger wm;

	// Use this for initialization
	void Awake () {

        ac = GetComponent<ActorController>();

        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        bm = sensor.GetComponent<BattleManger>();
        if(bm == null)
        {
            bm = sensor.AddComponent<BattleManger>();
        }
        bm.am = this;

        wm = model.GetComponent<WeaponManger>();
        if(wm == null)
        {
            wm = model.AddComponent<WeaponManger>();
        }
        wm.am = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoDamge()
    {
        ac.IssueTrigger("hit");
    }
}
