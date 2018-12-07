using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour {

    private Animator anim;
    private ActorController ac;

    public Vector3 a;

    void Awake() {
        anim = GetComponent<Animator>();
        ac = GetComponentInParent<ActorController>();
    }

    void OnAnimatorIK() {
        if (ac.leftIsShield)
        {
            if (anim.GetBool("defense") == false)
            {
                //print(123);
                Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += 0.75f * a;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
            }
        }

        
        
    }
}
