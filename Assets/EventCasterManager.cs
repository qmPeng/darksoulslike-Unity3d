using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManagerInterface {

    public string eventName;
    public bool active;

    void Start()
    {
        if(am == null)
        {
            am = GetComponentInParent<ActorManger>();
        }
    }

}
