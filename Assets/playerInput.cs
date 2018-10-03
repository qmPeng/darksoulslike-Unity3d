using UnityEngine;
using System.Collections;

public class playerInput : MonoBehaviour {

    //variable
    public string keyUp = "w"; 
    public string keyDown = "s"; 
    public string keyLeft = "a";
    public string keyRight = "d"; 

    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public bool inputEnable = true; //flag

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        targetDup = (Input.GetKey(keyUp)?1.0f:0) - (Input.GetKey(keyDown)?1.0f:0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnable == false) {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, .1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, .1f);
        Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright));
        Dvec = Dright * transform.right + Dup * transform.forward;
    }
}
