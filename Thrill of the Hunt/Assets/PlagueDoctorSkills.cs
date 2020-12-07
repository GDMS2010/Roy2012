using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorSkills : SkillTreeScript
{
    [SerializeField]
    Sprite moveActionImage;

    GridMovementController m_moveControl;
    // Start is called before the first frame update
    void Awake()
    {
        numMoves = 1;
        numActions = 0;
        m_moveControl = GetComponent<GridMovementController>();
        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction.action = new UnityEngine.Events.UnityEvent();
        moveAction.action.AddListener(Move);
        moveAction.actionImage = moveActionImage;
        skills.Add(moveAction);
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
}
