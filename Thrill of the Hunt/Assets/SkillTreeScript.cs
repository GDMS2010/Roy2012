using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeScript : MonoBehaviour
{
    public enum SkillType
    {
        Move,
        Other
    };


    public int numActions = 1;
    public int numMoves = 1;
    public int maxActions = 1;
    public int maxMoves = 1;

    [SerializeField]
    public List<ActionScript> skills;

    [Header("UI")]
    [SerializeField] protected Sprite moveActionImage;
    [SerializeField] protected Sprite basicAttackImage;
    [SerializeField] protected Sprite specialMove1Image;

    GridMovementController m_moveControl;
    BoardGenerator.Cell cell;

    private void Awake()
    {
        m_moveControl = GetComponent<GridMovementController>();
        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction.action = new UnityEngine.Events.UnityEvent();
        moveAction.action.AddListener(Move);
        moveAction.actionImage = moveActionImage;
        skills.Add(moveAction);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTurn()
    {
        numActions = maxActions;
        numMoves = maxMoves;
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

    void Move(Vector2 pos)
    {
        if (numMoves > 0)
        {
            GameManagerScript.getBoard().MoveToCell(pos, gameObject, m_moveControl);
            numMoves--;
            GameManagerScript.SubtractAction();
        }
    }

    void basicAttack()
    {

    }
}
