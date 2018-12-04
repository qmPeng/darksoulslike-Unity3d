using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManger : MonoBehaviour {

    private Collider weaponCol;
    public ActorManger am;

    public GameObject whl;
    public GameObject whr;

    void Start()
    {
        weaponCol = whr.GetComponentInChildren<Collider>();
    }

	public void WeaponEnable()
    {
        weaponCol.enabled = true;
        //print("WeaponEnable");
    }
    public void WeaponDisable()
    {
        weaponCol.enabled = false;
        //print("WeaponDisable");
    }
}
