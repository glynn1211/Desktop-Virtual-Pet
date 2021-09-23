using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTimer
{
    StatusContainer status;
    float startTime;
    float currentTime;
    bool resolved;

    public StatusTimer(StatusContainer status, float startTime)
    {
        this.status = status;
        resolved = !status.Status;
        this.startTime = startTime;
        currentTime = startTime;
    }

    public void RunTimer()
    {
        if(status.Status == resolved)
        {
            return;
        }
        currentTime--;
        if(currentTime <= 0)
        {
            status.Status = !status.Status;
            currentTime = startTime;
        }
    }
}
