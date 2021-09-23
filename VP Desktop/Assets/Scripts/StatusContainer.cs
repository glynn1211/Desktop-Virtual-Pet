using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusContainer
{
    public bool Status { get; set; }
    private bool startStatus;

    public StatusContainer(bool status)
    {
        Status = status;
        startStatus = status;
    }

    internal void Reset()
    {
        Status = startStatus;
    }
}
