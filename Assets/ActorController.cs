using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {

    public GameObject model;
    public playerInput pi;
    public float walkSpeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float JumpVelocity = 5.0f;
    public float RollVelocity = 1.0f;
    public float JabVelocity = 3.0f;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockPlanar = false;

	// Use this for initialization
	void Awake () {

        pi = GetComponent<playerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {  //Time.deltaTime 1/60
        //print(pi.Dup);
        float targetRunMulti = ((pi.run) ? runMultiplier : 1.0f);
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.5f));

        if (pi.jump ) {
            anim.SetTrigger("jump");
        }
        

        if(pi.Dmag > 0.1f){
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            model.transform.forward = targetForward;
        }
        if (lockPlanar == false) {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
        }

        if (rigid.velocity.magnitude > 1f) {
            anim.SetTrigger("roll");
        }
        
        
	}

    void FixedUpdate () {
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    /// <summary>
    /// message processing block
    /// </summary>
    public void OnJumpEnter() {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, JumpVelocity, 0);
        //print("on jump!!!!!");
    }

    //public void OnJumpExit() {
    //    pi.inputEnable = true;
    //    lockPlanar = false;
    //    //print("jump exit!!!");
    //}

    public void IsGround() {
        //print("is on ground");
        anim.SetBool("isGround", true);
    }
    public void IsNotGround() {
        //print("is not on ground");
        anim.SetBool("isGround", false);
    }
    public void OnGroundEnter() {
        pi.inputEnable = true;
        lockPlanar = false;

    }
    public void OnFallEnter() {
        pi.inputEnable = false;
        lockPlanar = true;

    }
    public void OnRollEnter() {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, RollVelocity, 0);
    }
    public void OnJabEnter() {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = model.transform.forward * (-JabVelocity);
    }
}
