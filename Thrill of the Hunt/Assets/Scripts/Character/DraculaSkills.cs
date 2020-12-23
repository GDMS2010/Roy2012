using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaSkills : SkillTreeScript
{
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

    void _BasicAttack(GameObject target)
    {
        target.GetComponent<Stats>().hurt(skills[1].damage, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        if (numMoves > 0)
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

    protected int ClawSwipe(ClickerTile tile)
    {
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().hurt(2 + stats.getNimBleness, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }
}
