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
        moveAction2._action = new UnityEngine.Events.UnityEvent<GameObject>();
        moveAction2._action.AddListener(_BasicAttack);
        moveAction2.actionImage = basicAttackImage;
        moveAction2.damage = stats.getDamage;
        skills.Add(moveAction2);

        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction._action = new UnityEngine.Events.UnityEvent<GameObject>();
        moveAction._action.AddListener(_ClawSwipe);
        moveAction.actionImage = specialMove1Image;
        moveAction.damage = 2 + stats.getNimBleness;
        moveAction.MaxCooldown = 2;
        skills.Add(moveAction);
    }
    //void BasicAttackClick()
    //{
    //    Clicker clicker = FindObjectOfType<Clicker>();
    //    BoardGenerator.Cell cell = m_moveControl.currentCell;
    //    clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Ally, true, basicAttack);
    //}

    //protected int basicAttack(ClickerTile tile)
    //{
    //    GameObject target = tile.gmc.currentCell.occupiedObject;
    //    target.GetComponent<Stats>().hurt(skills[1].damage, Stats.DamageType.True);
    //    numActions--;
    //    GameManagerScript.SubtractAction();
    //    return 0;
    //}

    void _BasicAttack(GameObject target)
    {
        target.GetComponent<Stats>().hurt(skills[1].damage, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        if(numMoves>0)
        {
            numMoves--;
            GameManagerScript.SubtractAction();
        }
    }

    void _ClawSwipe(GameObject target)
    {
        Debug.Log(transform.name + " used ClawSwipe");
        target.GetComponent<Stats>().hurt(skills[2].damage, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        skills[2].remainCooldown = skills[2].MaxCooldown;
        if (numMoves > 0)
        {
            numMoves--;
            GameManagerScript.SubtractAction();
        }
    }
    

    //void ClawSwipeClick()
    //{
    //    if (skills[2].remainCooldown <= 0)
    //    {
    //        Clicker clicker = FindObjectOfType<Clicker>();
    //        BoardGenerator.Cell cell = m_moveControl.currentCell;
    //        clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Ally, true, ClawSwipe);
    //    }
    //}

    //protected int ClawSwipe(ClickerTile tile)
    //{
    //    GameObject target = tile.gmc.currentCell.occupiedObject;
    //    target.GetComponent<Stats>().hurt(skills[2].damage, Stats.DamageType.True);
    //    numActions--;
    //    GameManagerScript.SubtractAction();
    //    if (skills[2].MaxCooldown > 0) skills[2].remainCooldown = skills[2].MaxCooldown;
    //    return 0;
    //}
}
