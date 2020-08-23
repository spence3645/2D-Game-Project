using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public string description;
    public int currentAmount;
    public int requiredAmount;
    public bool isDone;

    public void CheckProgress()
    {
        if(currentAmount >= requiredAmount)
        {
            isDone = true;
        }
    }
}
