using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour {

    [Header("***** Joystick Setting *****")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis3";
    public string axisJup = "axis6";
    public string btn0 = "btn0";
    public string btn1 = "btn1";
    public string btn2 = "btn2";
    public string btn3 = "btn3";


    [Header("***** Output signal *****")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    //1. pressing signal
    public bool run;

    //2.trigger once signal
    public bool jump;
    public bool lastJump;
    public bool attack;
    public bool lastAttack;

    //3.double trigger

    [Header("***** others *****")]

    public bool inputEnable = true; //flag

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    // Use this for initialization
 //   void Start () {
		
	//}
	
	// Update is called once per frame
	void Update () {

        Jup = -1 * Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);

        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX);

        if (inputEnable == false)
        {
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

        run = Input.GetButton(btn0);

        bool newJump = Input.GetButton(btn1);

        if (newJump != lastJump && newJump == true)
        {
            jump = true;
            print("jump trigger");
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetButton(btn2);

        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true;
            print("attack trigger");
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);

        return output;
    }
}
