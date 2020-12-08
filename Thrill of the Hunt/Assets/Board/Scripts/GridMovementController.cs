using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : MonoBehaviour
{
    BoardGenerator m_boardManagerRef;
    public BoardGenerator.Cell currentCell; //keeps a reference to the cur position on board
    // Start is called before the first frame update
    void Start()
    {
        m_boardManagerRef = GameObject.FindObjectOfType<BoardGenerator>();
        currentCell =  m_boardManagerRef.SnapObject(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_boardManagerRef.MoveToCell(new Vector2(currentCell.index.x - 1, currentCell.index.y), gameObject, this);
        }//Move Left
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_boardManagerRef.MoveToCell(new Vector2(currentCell.index.x + 1, currentCell.index.y), gameObject, this);
        }//Move Right
        else if (Input.GetKeyDown(KeyCode.W))
        {
            m_boardManagerRef.MoveToCell(new Vector2(currentCell.index.x, currentCell.index.y + 1), gameObject, this);
        }//Move Up
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_boardManagerRef.MoveToCell(new Vector2(currentCell.index.x, currentCell.index.y - 1), gameObject, this);
        }//Move Down
    }

    void Move(Vector2 dir, int numSquares)
    {
        //Ask the grid manager if the desired square is occupied (can move function)

        //Move the player there (over time/square by square/teleport?)
    }

    public void OnDie()
    {
        currentCell.occupiedObject = null;
    }
}
