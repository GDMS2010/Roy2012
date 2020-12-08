using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovementController))]
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

    protected GridMovementController m_moveControl;
    protected Stats stats;

    private void Awake()
    {
        Setup();
        
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
    void MoveClick()
    {
        Clicker clicker = FindObjectOfType<Clicker>();
        BoardGenerator.Cell cell = m_moveControl.currentCell;
        clicker.setupClickBoard(cell, stats.getSpeed, Clicker.TargetType.Empty, Move);
    }
    int Move(ClickerTile tile)
    {
        if (numMoves > 0)
        {
            GameManagerScript.getBoard().MoveToCell(tile.gmc.currentCell.index, gameObject, m_moveControl);
            numMoves--;
            GameManagerScript.SubtractAction();
        }

        else if (numActions>0)
        {
            GameManagerScript.getBoard().MoveToCell(tile.gmc.currentCell.index, gameObject, m_moveControl);
            numActions--;
            GameManagerScript.SubtractAction();
        }
        return 0;
    }

    protected void Setup()
    {
        m_moveControl = GetComponent<GridMovementController>();
        stats = GetComponent<Stats>();
        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction.action = new UnityEngine.Events.UnityEvent();
        moveAction.action.AddListener(MoveClick);
        moveAction.actionImage = moveActionImage;
        skills.Add(moveAction);
    }
}
