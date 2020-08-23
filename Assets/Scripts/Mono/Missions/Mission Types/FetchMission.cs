using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchMission : Mission
{
    public FetchMission(string description, bool isDone, int currentAmount, int requiredAmount)
    {
        this.description = description;
        this.isDone = isDone;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public void ItemPickedUp()
    {
        currentAmount += 1;
        CheckProgress();
    }
}
