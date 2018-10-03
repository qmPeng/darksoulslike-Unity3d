using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {

    public GameObject model;
    public playerInput pi;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Vector3 movingVec;

	// Use this for initialization
	void Awake () {

        pi = GetComponent<playerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {  //Time.deltaTime 1/60
        //print(pi.Dup);
        anim.SetFloat("forward", pi.Dmag);
        if(pi.Dmag > 0.1f){
            model.transform.forward = pi.Dvec;
        }
        movingVec = pi.Dmag * model.transform.forward;
        
	}

    void FixedUpdate () {
        rigid.position += movingVec * Time.fixedDeltaTime;
       
    }
}
