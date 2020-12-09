using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorSkills : SkillTreeScript
{
    [SerializeField]
    Sprite moveActionImagePD;
    [SerializeField]
    Sprite healImage;

    //GridMovementController m_moveControl;
    // Start is called before the first frame update
    void Awake()
    {
        Setup();
        numMoves = 1;
        numActions = 0;
        maxActions = numActions;
        maxMoves = numMoves;

        ActionScript AttackAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        AttackAction.action = new UnityEngine.Events.UnityEvent();
        AttackAction.action.AddListener(BasicAttackClick);
        AttackAction.actionImage = basicAttackImage;
        skills.Add(AttackAction);

        ActionScript healAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        healAction.action = new UnityEngine.Events.UnityEvent();
        healAction.action.AddListener(HealClick);
        healAction.actionImage = healImage;
        skills.Add(healAction);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

    void HealClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, stats.getAttackRange, Clicker.TargetType.Ally, false, Heal);
    }
    protected int Heal(ClickerTile tile)
    {
        int amount = 10; // TODO change to stats;
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().heal(amount);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }
}
