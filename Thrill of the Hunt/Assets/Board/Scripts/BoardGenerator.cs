using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    public class Cell
    {
        public float startX, startY, endX, endY, width, height;
        public Vector2 index;
        public GameObject occupiedObject;
        public Cell(float x, float y, float eX, float eY)
        {
            startX = x;
            startY = y;
            endX = eX;
            endY = eY;
            width = endX - startX;
            height = endY - startY;
            Vector3 boundsX;
        }
    }

    [SerializeField]
    [Tooltip("Number of cuts on that axis")]
    private int horizontal, vertical;

    //Cached measurament variables
    private Renderer m_renderer;
    [SerializeField]
    private float width, height;

    private List<Cell> cells;
    Cell[,] cells_2d;
    int[] test;

    //Debug Variables
    private bool drawing = true;

    void Awake()
    {
        cells = new List<Cell>();
        cells_2d = new Cell[horizontal, vertical];
        m_renderer = GetComponent<Renderer>();
        if (!m_renderer)
            Debug.LogError("Renderer expected in component with BoardGenerator.cs");
        else
        {
            width = m_renderer.bounds.max.x - m_renderer.bounds.min.x;
            height = m_renderer.bounds.max.z - m_renderer.bounds.min.z; //height is extracted from z since we don't account for 3D height
        }
        Split();
    }

    void Update()
    {

    }

    void Split()
    {
        float stepX = width / horizontal;
        float stepY = height / vertical;
        for (int i = 0; i < horizontal; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                Cell c = new Cell(stepX * i, stepY * j, stepX * i + stepX, stepY * j + stepY);
                c.index = new Vector2(i, j);
                cells.Add(c);
                cells_2d[i, j] = c;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!drawing || cells == null)
            return;
        // A sphere that fully encloses the bounding box.
        for (int i = 0; i < cells.Count; i++)
        {
            Gizmos.color = Color.red;
            Vector3 lineX1 = new Vector3(transform.position.x + cells[i].startX - width / 2, transform.position.y, transform.position.z + cells[i].startY - height / 2);
            Vector3 lineX2 = new Vector3(transform.position.x + cells[i].endX - width / 2, transform.position.y, transform.position.z + cells[i].startY - height / 2);
            Gizmos.DrawLine(lineX1, lineX2);
            Vector3 lineY1 = new Vector3(transform.position.x + cells[i].startX - width / 2, transform.position.y, transform.position.z + cells[i].startY - height / 2);
            Vector3 lineY2 = new Vector3(transform.position.x + cells[i].startX - width / 2, transform.position.y, transform.position.z + cells[i].endY - height / 2);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(lineY1, lineY2);
        }
    }//Draws grid to show board division

    public void MoveToCell(Vector2 cellIndex, GameObject obj, GridMovementController moveController)
    {
        Cell target = isValidCell((int)cellIndex.x, (int)cellIndex.y);
        bool isTile = obj.GetComponent<ClickerTile>();
        if (target != null)
        {
            if (!isTile) moveController.currentCell.occupiedObject = null;
            obj.transform.position = new Vector3(transform.position.x + target.startX + (target.width / 2) - width / 2,
                                    obj.transform.position.y,
                                    transform.position.z + target.startY + (target.height / 2) - height / 2);//position = half the cell width + parent padding (transform.x - this width / 2)
            moveController.currentCell = target;
            if (!isTile) target.occupiedObject = obj;
        }
    }

    public Cell SnapObject(Transform objTransform)
    {
        bool isTile = objTransform.GetComponent<ClickerTile>();
        //go through all the cells and find where object is. this can be optimized later
        for (int i = 0; i < cells.Count; i++)
        {
            if (CheckBounds(objTransform.position, cells[i]))
            {
                objTransform.position = new Vector3(transform.position.x + cells[i].startX + (cells[i].width / 2) - width / 2,
                                                    objTransform.position.y,
                                                    transform.position.z + cells[i].startY + (cells[i].height / 2) - height / 2);//position = half the cell width + parent padding (transform.x - this width / 2)

                if (!isTile) cells[i].occupiedObject = objTransform.gameObject;
                return cells[i];
            }
        }
        return null;
    }

    public Cell isValidCell(Vector2 index)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].index == index)
                return cells[i];
        }
        return null;
    }

    public Cell isValidCell(int x, int y)
    {
        if (x < 0 || x >= horizontal || y < 0 || y >= vertical) return null;
        return cells_2d[x, y];
    }

    bool CheckBounds(Vector3 position, Cell cell)
    {
        float startX = transform.position.x + cell.startX - width / 2;
        float startY = transform.position.z + cell.startY - height / 2;
        float endX = transform.position.x + cell.endX - width / 2;
        float endY = transform.position.z + cell.endY - height / 2;
        //Point 1 x0, y0
        Vector2 bottomLeft = new Vector2((transform.position.x + cell.startX - width / 2), transform.position.z + cell.startY - height / 2);
        //Point 2 x1, y0
        Vector2 bottomRight = new Vector2((transform.position.x + cell.endX - width / 2), transform.position.z + cell.startY - height / 2);
        //Point 3 x0, y1
        Vector2 topLeft = new Vector2((transform.position.x + cell.startX - width / 2), transform.position.z + cell.endY - height / 2);
        //Point 4 x1, y1
        Vector2 topRight = new Vector2((transform.position.x + cell.endX - width / 2), transform.position.z + cell.endY - height / 2);

        if (position.x >= startX && position.x <= endX)
        {
            if (position.z >= startY && position.z <= endY)
            {
                Debug.Log("Object inside cell " + cell.startX + " " + cell.endX);
                Debug.Log("Object inside cell " + cell.startY + " " + cell.endY);
                return true;
            }
        }//if obj position is inside the X bounds
        return false;
    }
    private void OnDestroy()
    {
        drawing = false;
    }
    public bool getCellIndex(Vector3 position, ref Vector2 cellIndex)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (CheckBounds(position, cells[i]))
            {
                cellIndex = cells[i].index;
                return true;
            }
        }
        return false;
    }

    // Take gameobject index
    public int getCellWalkDistance(Vector2 indexA, Vector2 indexB)
    {
        Cell start = isValidCell(indexA), end = isValidCell(indexB);
        if (start == null || end == null || indexA == indexB) //if not vaild index, or same index, just quit
            return -1;
        List<Cell> discovered = new List<Cell>();
        Queue<Cell> q = new Queue<Cell>();
        Queue<int> d = new Queue<int>();
        q.Enqueue(start);
        d.Enqueue(0);
        while (q.Count > 0)
        {
            Cell curr = q.Dequeue();
            int dis = d.Dequeue();
            if (curr == end)
                return dis;
            discovered.Add(curr);
            Cell temp;
            temp = isValidCell((int)curr.index.x - 1, (int)curr.index.y); //left
            if (temp != null && !discovered.Contains(temp)) { q.Enqueue(temp); d.Enqueue(dis + 1); }
            temp = isValidCell((int)curr.index.x + 1, (int)curr.index.y); //right
            if (temp != null && !discovered.Contains(temp)) { q.Enqueue(temp); d.Enqueue(dis + 1); }
            temp = isValidCell((int)curr.index.x, (int)curr.index.y + 1); //up
            if (temp != null && !discovered.Contains(temp)) { q.Enqueue(temp); d.Enqueue(dis + 1); }
            temp = isValidCell((int)curr.index.x, (int)curr.index.y - 1); //down
            if (temp != null && !discovered.Contains(temp)) { q.Enqueue(temp); d.Enqueue(dis + 1); }
        }


        return -1;
    }
    // Take gameobject position
    public int getCellWalkDistance(Vector3 start, Vector3 end)
    {
        Vector2 _s = new Vector2(), _e = new Vector2();
        if (!getCellIndex(start, ref _s) || !getCellIndex(end, ref _e))
            return -1;
        return getCellWalkDistance(_s, _e);
    }
}
