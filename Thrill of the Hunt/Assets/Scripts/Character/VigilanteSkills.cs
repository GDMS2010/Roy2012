﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigilanteSkills : SkillTreeScript
{
    bool isUsed = false;
    private void Awake()
    {
        Setup();

        ActionScript moveAction2 = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction2.action = new UnityEngine.Events.UnityEvent();
        moveAction2.action.AddListener(BasicAttackClick);
        moveAction2.actionImage = basicAttackImage;
        skills.Add(moveAction2);

        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction.action = new UnityEngine.Events.UnityEvent();
        moveAction.action.AddListener(BullsEyeClick);
        moveAction.actionImage = specialMove1Image;
        skills.Add(moveAction);
    }
    // Start is called before the first frame update

    void BasicAttackClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Enemy, true, basicAttack);
    }

    protected int basicAttack(ClickerTile tile)
    {
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().hurt(stats.getDamage, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }

    void BullsEyeClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Enemy, true, BullsEye);
    }

    protected int BullsEye(ClickerTile tile)
    {
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().hurt(stats.getDamage * 2, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }


}
