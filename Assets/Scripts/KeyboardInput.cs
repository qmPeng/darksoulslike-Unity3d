using UnityEngine;
using System.Collections;

public class KeyboardInput : IUserInput {

    //variable
    [Header("***** Key setting *****")]
    public string keyUp = "w"; 
    public string keyDown = "s"; 
    public string keyLeft = "a";
    public string keyRight = "d"; 

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;

    //[Header("***** Output signal *****")]
    //public float Dup;
    //public float Dright;
    //public float Dmag;
    //public Vector3 Dvec;
    //public float Jup;
    //public float Jright;


    ////1. pressing signal
    //public bool run;

    ////2.trigger once signal
    //public bool jump;
    //public bool lastJump;
    //public bool attack;
    //public bool lastAttack;

    ////3.double trigger

    //[Header("***** others *****")]

    //public bool inputEnable = true; //flag

    //private float targetDup;
    //private float targetDright;
    //private float velocityDup;
    //private float velocityDright;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        //print(Jup);
        //Jup = Input.GetAxis("Mouse Y");
        //Jright = Input.GetAxis("Mouse X");


        targetDup = (Input.GetKey(keyUp)?1.0f:0) - (Input.GetKey(keyDown)?1.0f:0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnable == false) {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, .1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, .1f);

        Vector2 tempDaxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDaxis.x;
        float Dup2 = tempDaxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);

        if (newJump != lastJump && newJump == true)
        {
            jump = true;
            print("jump trigger");
        }
        else {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetKey(keyC);

        if (newAttack != lastAttack && newAttack == true){
            rb = true;
            print("attack trigger");
        }
        else
        {
            rb = false;
        }
        lastAttack = newAttack;

    }
    //private Vector2 SquareToCircle(Vector2 input) {
    //    Vector2 output = Vector2.zero;

    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);

    //    return output;
    //}
}
