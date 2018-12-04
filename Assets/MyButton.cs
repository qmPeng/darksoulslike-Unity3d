using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton {

    public bool isPressing = false;
    public bool onPressed = false;
    public bool onReleased = false;
    public bool IsExtending = false;
    public bool isDelaying = false;


    public float extendingDuration = 0.15f;
    public float delayingDuration = 0.15f;

    private bool curState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public void Tick(bool input){

        
        extTimer.Tick();
        delayTimer.Tick();

        curState = input;
        isPressing = curState;
        IsExtending = false;
        isDelaying = false;

        onPressed = false;
        onReleased = false;
        if(curState != lastState)
        {
            if(curState == true)
            {
                onPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                onReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }
        lastState = curState;

        if(extTimer.state == MyTimer.STATE.RUN)
        {
            IsExtending = true;
        }
        
        if(delayTimer.state == MyTimer.STATE.RUN)
        {
            isDelaying = true;
        }
    }

    private void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}
