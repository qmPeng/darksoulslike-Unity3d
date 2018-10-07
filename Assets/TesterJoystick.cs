using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterJoystick : MonoBehaviour {

	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	void Update () {
        print(Input.GetAxis("padH"));
        //print("Jright:"+Input.GetAxis("Jright"));
        print("btn:" + Input.GetButtonDown("btnY"));
    }
}
