using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectManager : IActorManagerInterface {

    public PlayableDirector pd;

    [Header("=== Timeline assets ===")]
    public TimelineAsset frontStab;

    [Header("=== Asset Setting ===")]
    public ActorManger attacker;
    public ActorManger victim;

	// Use this for initialization
	void Start () {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        //pd.playableAsset = Instantiate(frontStab);

        //foreach(var track in pd.playableAsset.outputs)
        //{
        //    if(track.streamName == "My Super Playable Track")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, attacker);
        //    }
        //    else if(track.streamName == "My Super Playable Track (1)")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, victim);
        //    }
        //    else if (track.streamName == "Animation Track")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, attacker.ac.anim);
        //    }
        //    else if (track.streamName == "Animation Track (1)")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, victim.ac.anim);
        //    }
        //}
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKey(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player"))
  //      {
  //          pd.Play();
  //      }
	}

    public void PlayFrontStab(string timelineName, ActorManger attacker, ActorManger victim)
    {
        if(timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);

            foreach (var track in pd.playableAsset.outputs)
            {
                if (track.streamName == "My Super Playable Track")
                {
                    pd.SetGenericBinding(track.sourceObject, attacker);
                }
                else if (track.streamName == "My Super Playable Track (1)")
                {
                    pd.SetGenericBinding(track.sourceObject, victim);
                }
                else if (track.streamName == "Animation Track")
                {
                    pd.SetGenericBinding(track.sourceObject, attacker.ac.anim);
                }
                else if (track.streamName == "Animation Track (1)")
                {
                    pd.SetGenericBinding(track.sourceObject, victim.ac.anim);
                }
            }
            pd.Play();
        }
    }
}
