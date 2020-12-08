using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireThrallSkills : SkillTreeScript
{
    // Melee attack
    // Deal 2 + nimbleness Damage
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
        moveAction.action.AddListener(ClawSwipeClick);
        moveAction.actionImage = specialMove1Image;
        skills.Add(moveAction);
    }
    void BasicAttackClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Ally, basicAttack);
    }
    protected int basicAttack(ClickerTile tile)
    {
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().hurt(stats.getDamage, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }

    void ClawSwipeClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Ally, ClawSwipe);
    }

    protected int ClawSwipe(ClickerTile tile)
    {
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().hurt(2 + stats.getNimBleness, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }
}
