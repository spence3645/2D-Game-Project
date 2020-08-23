using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMission : Mission
{

    int enemyType;

    public KillMission(int enemyType, string description, bool isDone, int currentAmount, int requiredAmount)
    {
        this.enemyType = enemyType;
        this.description = description;
        this.isDone = isDone;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public void EnemyKilled(IEnemy enemy)
    {
        if(enemy.ID == enemyType)
        {
            currentAmount += 1;
            CheckProgress();
        }
    }
}
