using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {

    public GameObject model;
    public playerInput pi;
    public float walkSpeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float JumpVelocity = 5.0f;
    public float RollVelocity = 1.0f;
    //public float JabMultipliyer = 3.0f;

    [Header("***** Friction Setting *****")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;
    
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack;
    private bool lockPlanar = false;
    private CapsuleCollider col;
    private float lerpTarget;

	// Use this for initialization
	void Awake () {

        pi = GetComponent<playerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {  //Time.deltaTime 1/60
        //print(pi.Dup);
        float targetRunMulti = ((pi.run) ? runMultiplier : 1.0f);
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.5f));

        if (pi.jump ) {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if (pi.attack && canAttack ) {
            anim.SetTrigger("attack");
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
        //print(CheckState("idle", "Attack"));

    }

    void FixedUpdate () {
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    private bool CheckState(string stateName, string layerName = "Base Layer") {
        //int layerIndex = anim.GetLayerIndex(layerName);
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
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
        canAttack = true;
        col.material = frictionOne;
    }
    public void OnGroundExit() {
        col.material = frictionZero;
    }
    public void OnFallEnter() {
        pi.inputEnable = false;
        lockPlanar = true;
        canAttack = false;

    }
    public void OnRollEnter() {
        pi.inputEnable = false;
        lockPlanar = true;
        canAttack = false;
        thrustVec = new Vector3(0, RollVelocity, 0);
    }
    public void OnJabEnter() {
        pi.inputEnable = false;
        //lockPlanar = true;
        
    }
    public void OnJabUpdate() {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnAttack1hAEnter(){
        pi.inputEnable = false;
        //lockPlanar = true;
        //print(anim.GetLayerIndex("attack"));
        lerpTarget = 1.0f;
        
    }
    public void OnAttack1hAUpdate() {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        //float currentWeight = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.3f));
    }
    public void OnAttackIdleEnter() {
        pi.inputEnable = true;
        //lockPlanar = false;
        lerpTarget = 0;
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 0);
    }
    public void OnAttackIdleUpdate() {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.1f));
    }
}
