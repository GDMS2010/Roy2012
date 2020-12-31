using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Leveling
{
    public int EXP;
    public int currLevel;
    public Action OnLevelUp;

    public int MAX_EXP;
    public int MAX_LEVEL = 50;


  public Leveling(int level, Action OnLevUp)
    {
        MAX_EXP = GetXPForLevel(MAX_LEVEL);

        currLevel = level;
        EXP = GetXPForLevel(level);
        OnLevelUp = OnLevUp;
    }

    public int GetXPForLevel(int level)
    {
        if(level > MAX_LEVEL)
        {
            return 0;
        }

        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Mathf.Floor(levelCycle + (300.0f * Mathf.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
        }
        if (secondPass > MAX_EXP && MAX_EXP != 0)
        {
            return MAX_EXP;
        }
        if(secondPass < 0)
        {
            return MAX_EXP;
        }
        return secondPass;
    }

    public int GetLevelForXP(int exp)
    {
        if(exp > MAX_EXP)
        {
            return MAX_EXP;
        }

        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle < MAX_LEVEL; levelCycle++)
        {
            firstPass += (int)Mathf.Floor(levelCycle + (300.0f * Mathf.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;

            if(secondPass > exp)
            {
                return levelCycle;
            }
        }
        if(exp > secondPass)
        {
            return MAX_LEVEL;
        }
           return 0;
    }

    public bool AddExp(int amount)
    {
        if (amount + EXP <0 || EXP > MAX_EXP)
        {
            if(EXP > MAX_EXP)
            {
                EXP = MAX_EXP;
                return false;
            }
        }
        int oldLevel = GetLevelForXP(EXP);
        EXP += amount;
        if (oldLevel < GetLevelForXP(EXP))
        {
            if (currLevel< GetLevelForXP(EXP))
            {
                currLevel = GetLevelForXP(EXP);
                if(OnLevelUp != null)
                {
                    OnLevelUp.Invoke();
                    return true;
                }
            }
        }
        return false;
    }
    public void IncreaseStats()
    {

    }
}
