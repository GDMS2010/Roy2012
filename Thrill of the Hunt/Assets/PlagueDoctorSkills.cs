using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorSkills : SkillTreeScript
{
    [SerializeField]
    Sprite moveActionImagePD;
    [SerializeField]
    Sprite healImage;

    Stats characterStats;

    //GridMovementController m_moveControl;
    // Start is called before the first frame update
    void Awake()
    {
        numMoves = 1;
        numActions = 0;
        maxActions = numActions;
        maxMoves = numMoves;
        m_moveControl = GetComponent<GridMovementController>();

        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction.action = new UnityEngine.Events.UnityEvent();
        moveAction.action.AddListener(Move);
        moveAction.actionImage = moveActionImagePD;
        skills.Add(moveAction);

        ActionScript healAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        healAction.action = new UnityEngine.Events.UnityEvent();
        healAction.action.AddListener(Heal);
        healAction.actionImage = healImage;
        skills.Add(healAction);
        
        characterStats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        if (numMoves > 0)
        {
            GameManagerScript.getBoard().MoveToCell(new Vector2(m_moveControl.currentCell.index.x - 1, m_moveControl.currentCell.index.y), gameObject, m_moveControl);
            numMoves--;
            GameManagerScript.SubtractAction();
        }
    }

    void Heal()
    {
        //only heals self for sprint 2, make it so it can heal on a target later
        characterStats.currHealth += 10;
        if (characterStats.currHealth > characterStats.maxHealth)
            characterStats.currHealth = characterStats.maxHealth;
    }
}
