using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput {

    [Header("***** Joystick Setting *****")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis3";
    public string axisJup = "axis6";
    public string btn0 = "btn0";
    public string btn1 = "btn1";
    public string btn2 = "btn2";
    public string btn3 = "btn3";
    public string btnLPad = "btn10";
    public string btnRPad = "btn11";
    public string btnLB = "btn4";
    public string btnLT = "btn6";
    public string btnRB = "btn5";
    public string btnRT = "btn7";

    public MyButton button0 = new MyButton();
    public MyButton button1 = new MyButton();
    public MyButton button2 = new MyButton();
    public MyButton button3 = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonRT = new MyButton();
    public MyButton buttonLPad = new MyButton();
    public MyButton buttonRPad = new MyButton();


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
    //   void Start () {

    //}

    // Update is called once per frame
    void Update () {

        button0.Tick(Input.GetButton(btn0));
        button1.Tick(Input.GetButton(btn1));
        button2.Tick(Input.GetButton(btn2));
        button3.Tick(Input.GetButton(btn3));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonRT.Tick(Input.GetButton(btnRT));
        buttonLPad.Tick(Input.GetButton(btnLPad));
        buttonRPad.Tick(Input.GetButton(btnRPad));

        //print(button0.onPressed);


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

        run = (button1.isPressing && !(button1.isDelaying))|| button1.IsExtending;
        defense = buttonLB.isPressing; 
        jump = button1.onPressed && button1.IsExtending;
        rb = buttonRB.onPressed;
        rt = buttonRT.onPressed;
        lb = buttonLB.onPressed;
        lt = buttonLT.onPressed;
        roll = button0.onPressed;
        lockon = buttonLPad.onPressed;
        action = button2.onPressed;

        //bool newJump = Input.GetButton(btn1);


        //if (newJump != lastJump && newJump == true)
        //{
        //    jump = true;
        //    print("jump trigger");
        //}
        //else
        //{
        //    jump = false;
        //}
        //lastJump = newJump;

        //bool newAttack = Input.GetButton(btn2);

        //if (newAttack != lastAttack && newAttack == true)
        //{
        //    attack = true;
        //    print("attack trigger");
        //}
        //else
        //{
        //    attack = false;
        //}
        //lastAttack = newAttack;
    }

    //private Vector2 SquareToCircle(Vector2 input)
    //{
    //    Vector2 output = Vector2.zero;

    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);

    //    return output;
    //}
}
