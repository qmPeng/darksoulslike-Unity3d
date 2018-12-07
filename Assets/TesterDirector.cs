using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TesterDirector : MonoBehaviour {

    public PlayableDirector pd;

    public Animator Attacker;
    public Animator Victim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach(var track in pd.playableAsset.outputs)
            {
                //print(track.streamName);
                if(track.streamName == "Animation Track")
                {
                    pd.SetGenericBinding(track.sourceObject, Attacker);
                }
                else if ( track.streamName == "Animation Track (1)")
                {
                    pd.SetGenericBinding(track.sourceObject, Victim);
                }
            }
            

            
            //pd.time = 0;
            //pd.Stop();
            //pd.Evaluate();
            pd.Play();
        }
	}
}
