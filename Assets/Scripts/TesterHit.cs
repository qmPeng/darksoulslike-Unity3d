using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterHit : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J)){
            anim.SetTrigger("hit");
        }
	}
}
