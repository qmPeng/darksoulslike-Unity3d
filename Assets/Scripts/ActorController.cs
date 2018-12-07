using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {

    public GameObject model;
    //public GameObject[] objs;
    public CameraController camcon;
    public IUserInput pi;
    public float walkSpeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float JumpVelocity = 5.0f;
    public float RollVelocity = 10.0f;
    //public float JabMultipliyer = 3.0f;

    [Header("***** Friction Setting *****")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;
    
    public Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack;
    private bool lockPlanar = false;
    private bool trackDirection = false;
    private CapsuleCollider col;
    //private float lerpTarget;
    private Vector3 deltaPos;
    private Vector3 tempVec;

    public bool leftIsShield;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;

	// Use this for initialization
	void Awake () {

        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs) {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        //ScriptableObject scp = GetComponent<ScriptableObject>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {  //Time.deltaTime 1/60
        //print(pi.Dup);
        if (camcon.lockState == false)
        {
            float targetRunMulti = ((pi.run) ? runMultiplier : 1.0f);
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.5f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDVec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDVec.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDVec.x * ((pi.run) ? 2.0f : 1.0f));
        }
       
        //anim.SetBool("defense", pi.defense);

        if(pi.roll || rigid.velocity.magnitude > 7f)
        {
            //print("roll trigger");
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (pi.jump ) {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if ((pi.rb || pi.lb) && canAttack && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL"))) {
            if (pi.rb)
            {
                anim.SetBool("r0l1", false);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && !leftIsShield)
            {
                anim.SetBool("r0l1", true);
                anim.SetTrigger("attack");
            }
            
        }

        if ((pi.rt||pi.lt) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rt)
            {
                //do heavy attack
            }
            else 
            {
                if (!leftIsShield)
                {
                    //do heavy attack
                }
                else
                {
                    anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
                    anim.SetTrigger("counterBack");
                    
                }
            }
        }

        if (pi.action)
        {
            OnAction.Invoke();
        }

        if (leftIsShield)
        {
            
            if (CheckState("ground") || CheckState("blocked"))
            {
                anim.SetBool("defense", pi.defense);
                anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 1);
            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
            }
            
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
        }

        //if (CheckState("ground") && leftIsShield)
        //{
        //    anim.SetBool("defense", pi.defense);
        //    if (pi.defense)
        //    {
        //        anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);

        //    }
        //    else
        //    {
        //        anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        //    }

        //}
        //anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);

        if (pi.lockon)
        {
            camcon.LockUnlock();
        }
        
        //print(CheckState("idle", "Attack"));

    }

    void FixedUpdate () {
        rigid.position += deltaPos;
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;

        if(camcon.lockState == false)
        {
            if (pi.Dmag > 0.1f)
            {
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
                model.transform.forward = targetForward;
            }
            if (lockPlanar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            if(trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }
            
            if(lockPlanar == false)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
            
        }
        

        //if (rigid.velocity.magnitude > 1f)
        //{
        //    anim.SetTrigger("roll");
        //}
    }

    public bool CheckState(string stateName, string layerName = "Base Layer") {
        //int layerIndex = anim.GetLayerIndex(layerName);
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        //int layerIndex = anim.GetLayerIndex(layerName);
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }

    /// <summary>
    /// message processing block
    /// </summary>
    public void OnJumpEnter() {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, JumpVelocity, 0);
        trackDirection = true;
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
        trackDirection = false;
        model.SendMessage("WeaponDisable");
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
        thrustVec = new Vector3(Mathf.Lerp(0,10,0.1f), RollVelocity, 0);
        trackDirection = true;
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
        //lerpTarget = 1.0f;
        
    }
    public void OnAttack1hAUpdate() {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        //float currentWeight = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.1f);
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.3f));
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    public void OnUpdateRM(object _deltaPos) {
        if (CheckState("attack1hC")){
            deltaPos = 0.6f*deltaPos + 0.4f*(Vector3)_deltaPos;
        }
        //if (CheckState("roll")) {
        //    deltaPos = (Vector3)_deltaPos;
        //    //deltaPos = model.transform.forward * tempVec;
        //}
    }

    public void OnHitEnter()
    {
        model.SendMessage("WeaponDisable");
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnBlockedEnter()
    {
        pi.inputEnable = false;
    }

    public void OnDieEnter()
    {
        //GameObject.FindGameObjectWithTag("AI").SetActive(false);
        pi.inputEnable = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
        
    }

    public void OnStuunedEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBack()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnLockEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void IssueBool (string boolName, bool value)
    {
        anim.SetBool(boolName, value);
    }

}
