using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorSkills : SkillTreeScript
{
    // Start is called before the first frame update
    void Awake()
    {
        Setup();
        //numMoves = 1;
        //numActions = 0;
        ActionScript moveAction2 = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction2.action = new UnityEngine.Events.UnityEvent();
        moveAction2.action.AddListener(BasicAttackClick);
        moveAction2.actionImage = basicAttackImage;
        skills.Add(moveAction2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void BasicAttackClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, 5, Clicker.TargetType.Enemy, basicAttack);
    }
    protected int basicAttack(ClickerTile tile)
    {
        GameObject target = tile.gmc.currentCell.occupiedObject;
        target.GetComponent<Stats>().hurt(10, Stats.DamageType.True);
        numActions--;
        GameManagerScript.SubtractAction();
        return 0;
    }
}
