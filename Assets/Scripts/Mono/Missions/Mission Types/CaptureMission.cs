using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureMission : Mission
{
    public CaptureMission(string description, bool isDone, int currentAmount, int requiredAmount)
    {
        this.description = description;
        this.isDone = isDone;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public void FlagCaptured()
    {
        currentAmount += 1;
        CheckProgress();
    }
}
