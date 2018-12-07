using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManger : MonoBehaviour {

    public ActorController ac;
    public BattleManger bm;
    public WeaponManger wm;
    public StateManager sm;
    public DirectManager dm;
    public InteractionManager im;

	// Use this for initialization
	void Awake () {

        ac = GetComponent<ActorController>();

        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        
        bm = Bind<BattleManger>(sensor);
        wm = Bind<WeaponManger>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectManager>(gameObject);
        im = Bind<InteractionManager>(sensor);

        ac.OnAction += DoAction;
        
        //sm.Test();

        //bm = sensor.GetComponent<BattleManger>();
        //if(bm == null)
        //{
        //    bm = sensor.AddComponent<BattleManger>();
        //}
        //bm.am = this;

        //wm = model.GetComponent<WeaponManger>();
        //if(wm == null)
        //{
        //    wm = model.AddComponent<WeaponManger>();
        //}
        //wm.am = this;


        //sm = gameObject.GetComponent<StateManager>();
        //if (sm == null)
        //{
        //    sm = gameObject.AddComponent<StateManager>();
        //}
        //sm.am = this;
        //sm.Test();
    }

    public void DoAction()
    {
        if(im.overlapEcastms.Count != 0)
        {
            //print(im.overlapEcastms[0].eventName);

            if(im.overlapEcastms[0].eventName == "frontStab")
            {
                dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
            }
        }
    }
   

    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if(tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    //public void DoStab(ActorController targetAc, bool attackValid, bool counterValid)
    //{

    //}

    public void TryDoDamge(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        //if(sm.HP > 0)
        //{
        //    sm.AddHP(-5);
        //}
        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned();
            }
            
        }
        else if (sm.isCounterBackFailure)
        {
            if (attackValid)
            {
                HitOrDie(false);
            }
           
        }
        else if (sm.isImmortal)
        {
            //Do nothing
        }
        else if (sm.isDefense)
        {
            // attack should be blocked
            Blocked();
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(true);
            }
               
        }
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }
    public void Die()
    {
        ac.IssueTrigger("die");
        MonoBehaviour mb = ac.GetComponentInParent<MonoBehaviour>();
        //print(mb.name);
        GameObject.Find(mb.name).transform.GetChild(5).gameObject.SetActive(false);
        

        ac.pi.inputEnable = false;
        if (ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
            
        }
        ac.camcon.enabled = false;

    }
    public void HitOrDie(bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {
            //already dead
        }
        else
        {
            sm.AddHP(-5);
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                }
                //do some vfx 
            }

            else
            {
                Die();
                
            }
        }
    }
    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }

    public void TestEcho()
    {
        print("echo echo");
    }

    public void LockUnlockActorController(bool value)
    {
        ac.IssueBool("lock", value);
    }


}
