using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public GameObject dot;
    Camera cam;
    System.Func<ClickerTile, int> _func; //Gameobject to, int just ignore it
    GameObject target;
    List<ClickerTile> ClickTiles;
    int[] checkIndex = {-1,0, //left
                        1,0,  //right
                        0,-1, //up
                        0,1 }; //down

    List<BoardGenerator.Cell> checkedCells;
    List<BoardGenerator.Cell> availableCells;
    public enum TargetType
    {
        Empty,
        Ally,
        Enemy
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        checkedCells = new List<BoardGenerator.Cell>();
        availableCells = new List<BoardGenerator.Cell>();
        ClickTiles = new List<ClickerTile>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    Physics.Raycast(ray, out hit, 50f);
        //    Debug.DrawLine(ray.origin, hit.point);
        //    if (Physics.Raycast(ray, out hit, 50f))
        //    {
        //        Debug.DrawLine(ray.origin, hit.point);
        //        Vector3 position = hit.point;
        //        Vector2 index = new Vector2();
        //        Debug.Log(
        //        GameManagerScript.getBoard().getCellIndex(position, ref index)
        //        );
        //        Debug.Log(index);
        //    }
        //}

        //_func(target);
    }
    public void setupClickBoard(BoardGenerator.Cell start, int range, TargetType type, System.Func<ClickerTile, int> func)
    {
        _func = func;
        checkedCells.Clear();
        availableCells.Clear();
        setupRecursive(start, range, type);
        //BFS(start, range, type);
        //Search(start, range, type);
        foreach (var item in ClickTiles)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in availableCells)
        {
            ClickerTile temptile = null;
            //If have any available tile
            foreach (var tile in ClickTiles)
            {
                if (!tile.used)
                {
                    //get tile ref
                    temptile = tile;
                    break;
                }
            }
            if (temptile == null)
            {
                //else instantiate a new one
                GameObject temp = Instantiate(dot, transform);
                temptile = temp.GetComponent<ClickerTile>();
                temptile.setClicker(this);
                ClickTiles.Add(temptile);
            }
            //set position
            GridMovementController gmc = temptile.GetComponent<GridMovementController>();
            GameManagerScript.getBoard().MoveToCell(item.index, temptile.gameObject, gmc);
            //set active
            temptile.gameObject.SetActive(true);
            temptile.used = true;
        }
    }

    void setupRecursive(BoardGenerator.Cell start, int range, TargetType type)
    {
        //if negative range, just leave
        if (range < 0) return;
        //If checked, just leave
        if (checkedCells.Contains(start)) return;
        //Add this cell as checked
        checkedCells.Add(start);
        //Check the cell
        checking(start, type);
        //Foreach neibour, call this function
        for (int i = 0; i < 8; i += 2)
        {
            int x = (int)start.index.x + checkIndex[i];
            int y = (int)start.index.y + checkIndex[i + 1];
            BoardGenerator.Cell temp = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
            if (temp != null)
                setupRecursive(temp, range - 1, type);
        }
    }

    void BFS(BoardGenerator.Cell start, int range, TargetType type)
    {
        Queue<BoardGenerator.Cell> queue = new Queue<BoardGenerator.Cell>();

        queue.Enqueue(start);
        checkedCells.Add(start);
        while (queue.Count > 0)
        {
            BoardGenerator.Cell temp = queue.Dequeue();
            checking(temp, type);

            for (int i = 0; i < 8; i += 2)
            {
                int x = (int)start.index.x + checkIndex[i];
                int y = (int)start.index.y + checkIndex[i + 1];
                BoardGenerator.Cell temp2 = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
                if (temp2 != null)
                {
                    if (!checkedCells.Contains(temp2))
                    {
                        checkedCells.Add(temp2);
                        queue.Enqueue(temp2);
                    }
                }
            }
        }
    }

    void Search(BoardGenerator.Cell start, int range, TargetType type)
    {
        int center = (int)start.index.x + (int)start.index.y;
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                if (Mathf.Abs(x-y)<range)
                {
                    BoardGenerator.Cell temp = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
                    if (temp != null)
                    {
                        int sum = x + y;
                        if (Mathf.Abs(center - sum) <= range)
                        {
                                checking(temp, type);
                        }
                    }
                }
            }
        }
    }

    void checking(BoardGenerator.Cell cell, TargetType type)
    {
        switch (type)
        {
            case TargetType.Empty:
                {
                    if (!cell.occupiedObject)
                        availableCells.Add(cell);
                    break;
                }
            case TargetType.Ally:
                {
                    if (cell.occupiedObject)
                        if (cell.occupiedObject.tag == "Ally")
                            availableCells.Add(cell);
                    break;
                }
            case TargetType.Enemy:
                {
                    if (cell.occupiedObject)
                        if (cell.occupiedObject.tag == "Enemy")
                            availableCells.Add(cell);
                    break;
                }
            default:
                break;
        }
    }

    public void Clicked(ClickerTile tile)
    {
        _func(tile);
        foreach (var item in ClickTiles)
        {
            item.used = false;
            item.gameObject.SetActive(false);
        }
    }
}
