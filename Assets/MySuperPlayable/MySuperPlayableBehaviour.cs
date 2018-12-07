using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public Camera myCamera;
    public float myFloat;

    PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        pd = (PlayableDirector)playable.GetGraph().GetResolver();
        //Debug.Log(pd);
        foreach(var track in pd.playableAsset.outputs)
        {
            //Debug.Log(track.streamName);
            if(track.streamName == "My Super Playable Track" || track.streamName == "My Super Playable Track (1)")
            {
                //Debug.Log(pd.GetGenericBinding(track.sourceObject));
                ActorManger am = (ActorManger)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(true);
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();
        foreach (var track in pd.playableAsset.outputs)
        {
            //Debug.Log(track.streamName);
            if (track.streamName == "My Super Playable Track" || track.streamName == "My Super Playable Track (1)")
            {
                //Debug.Log(pd.GetGenericBinding(track.sourceObject));
                ActorManger am = (ActorManger)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(false);
            }
        }
    }
}
