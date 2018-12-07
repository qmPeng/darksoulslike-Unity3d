using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("***** Output signal *****")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    //1. pressing signal
    public bool run;
    public bool defense;

    //2.trigger once signal
    public bool jump;
    protected bool lastJump;
    public bool action;
    //public bool attack;
    protected bool lastAttack;
    public bool roll;
    public bool lockon;
    public bool lt;
    public bool lb;
    public bool rt;
    public bool rb;


    //3.double trigger

    [Header("***** others *****")]

    public bool inputEnable = true; //flag

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);

        return output;
    }

    protected void UpdateDmagDVec(float Dup2, float Dright2)
    {
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
    }
}
