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

    List<BoardGenerator.Cell> availableCells;
    public enum TargetType
    {
        Empty,
        Ally,
        Enemy
    }

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
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
    public void setupClickBoard(BoardGenerator.Cell start, int range, TargetType type, bool Attack, System.Func<ClickerTile, int> func)
    {
        _func = func;
        foreach (var item in ClickTiles)
        {
            item.gameObject.SetActive(false);
        }

        int cx = (int)start.index.x, cy = (int)start.index.y; //get center xy
        switch (type)
        {
            case TargetType.Empty:
                {
                    List<BoardGenerator.Cell> walkable = new List<BoardGenerator.Cell>();
                    for (int x = cx - range; x <= cx + range; x++) //from left to right
                    {
                        int val = cx - x;
                        val = Mathf.Abs(val);
                        val = range - val;  //closer to leftmost,rightmost, y range will be lowest
                        for (int y = cy - val; y <= cy + val; y++)
                        {
                            BoardGenerator.Cell temp = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
                            if (temp != null)
                            {
                                if (!temp.occupiedObject)
                                    walkable.Add(temp);
                            }
                        }
                    }
                    foreach (var cell in walkable)
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
                        GameManagerScript.getBoard().MoveToCell(cell.index, temptile.gameObject, gmc);
                        //set active
                        temptile.TurnOn(ClickerTile.TileType.Walkable);
                    }
                    break;
                }
            case TargetType.Ally:
            case TargetType.Enemy:
                {
                    List<BoardGenerator.Cell> withinRange = new List<BoardGenerator.Cell>();
                    for (int x = cx - range; x <= cx + range; x++) //from left to right
                    {
                        int val = cx - x;
                        val = Mathf.Abs(val);
                        val = range - val;  //closer to leftmost,rightmost, y range will be lowest
                        for (int y = cy - val; y <= cy + val; y++)
                        {
                            BoardGenerator.Cell temp = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
                            if (temp != null)
                            {
                                withinRange.Add(temp);
                            }
                        }
                    }
                    foreach (var cell in withinRange)
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
                        GameManagerScript.getBoard().MoveToCell(cell.index, temptile.gameObject, gmc);
                        //set active
                        if (!cell.occupiedObject)
                            temptile.TurnOn(ClickerTile.TileType.Range);
                        else
                        {
                            if (cell.occupiedObject.tag == "Ally")
                            {
                                if (type == TargetType.Ally)
                                    if (Attack)
                                        temptile.TurnOn(ClickerTile.TileType.Attackable);
                                    else
                                        temptile.TurnOn(ClickerTile.TileType.Healable);
                                else
                                    temptile.TurnOn(ClickerTile.TileType.Range);
                            }
                            else if (cell.occupiedObject.tag == "Enemy")
                            {
                                if (type == TargetType.Enemy)
                                    if (Attack)
                                        temptile.TurnOn(ClickerTile.TileType.Attackable);
                                    else
                                        temptile.TurnOn(ClickerTile.TileType.Healable);
                                else
                                    temptile.TurnOn(ClickerTile.TileType.Range);
                            }
                            else
                                temptile.TurnOn(ClickerTile.TileType.Range);
                        }
                    }
                    break;
                }
            default:
                break;
        }
        //Search(start, range, type);

        //foreach (var item in availableCells)
        //{
        //    ClickerTile temptile = null;
        //    //If have any available tile
        //    foreach (var tile in ClickTiles)
        //    {
        //        if (!tile.used)
        //        {
        //            //get tile ref
        //            temptile = tile;
        //            break;
        //        }
        //    }
        //    if (temptile == null)
        //    {
        //        //else instantiate a new one
        //        GameObject temp = Instantiate(dot, transform);
        //        temptile = temp.GetComponent<ClickerTile>();
        //        temptile.setClicker(this);
        //        ClickTiles.Add(temptile);
        //    }
        //    //set position
        //    GridMovementController gmc = temptile.GetComponent<GridMovementController>();
        //    GameManagerScript.getBoard().MoveToCell(item.index, temptile.gameObject, gmc);
        //    //set active
        //    temptile.gameObject.SetActive(true);
        //    temptile.used = true;
        //}
    }
    //void Search(BoardGenerator.Cell start, int range, TargetType type)
    //{
    //    int cx = (int)start.index.x, cy = (int)start.index.y; //get center xy

    //    for (int x = cx - range; x <= cx + range; x++) //from left to right
    //    {
    //        int val = cx - x;
    //        val = Mathf.Abs(val);
    //        val = range - val;  //closer to leftmost,rightmost, y range will be lowest
    //        for (int y = cy - val; y <= cy + val; y++)
    //        {
    //            BoardGenerator.Cell temp = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
    //            if (temp != null)
    //            {
    //                checking(temp, type);
    //            }
    //        }
    //    }
    //}

    //void checking(BoardGenerator.Cell cell, TargetType type)
    //{
    //    switch (type)
    //    {
    //        case TargetType.Empty:
    //            {
    //                if (!cell.occupiedObject)
    //                    availableCells.Add(cell);
    //                break;
    //            }
    //        case TargetType.Ally:
    //            {
    //                if (cell.occupiedObject)
    //                    if (cell.occupiedObject.tag == "Ally")
    //                        availableCells.Add(cell);
    //                break;
    //            }
    //        case TargetType.Enemy:
    //            {
    //                if (cell.occupiedObject)
    //                    if (cell.occupiedObject.tag == "Enemy")
    //                        availableCells.Add(cell);
    //                break;
    //            }
    //        default:
    //            break;
    //    }
    //}

    public void Clicked(ClickerTile tile)
    {
        if(tile.tileType!=ClickerTile.TileType.Range)
        {
            _func(tile);
            CloseTiles();
        }        
    }

    public void CloseTiles()
    {
        foreach (var item in ClickTiles)
        {
            item.TurnOff();
        }
    }
}
