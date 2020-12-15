using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIMaster : MonoBehaviour
{
    List<GameObject> playerCharList;
    GameObject target = null;
    GridMovementController gmc;
    BoardGenerator bg;
    Stats stats;
    SkillTreeScript skillTree;
    bool myTurn;
    string s;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        playerCharList = new List<GameObject>();
        skillTree = GetComponent<SkillTreeScript>();
        GameObject[] p = GameObject.FindGameObjectsWithTag("Ally");
        float distance = float.MaxValue;
        foreach (var item in p)
        {
            playerCharList.Add(item);
            float tempDis = Vector3.Distance(item.transform.position, transform.position);
            if (tempDis < distance)
            {
                distance = tempDis;
                target = item;
            }
        }
        gmc = GetComponent<GridMovementController>();
        bg = FindObjectOfType<BoardGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn)
        {
            Action();
        }
    }


    //FindCloestTarget and replace current target
    void FindCloestTarget()
    {
        float distance = float.MaxValue;
        foreach (var item in playerCharList)
        {
            float tempDis = Vector3.Distance(item.transform.position, transform.position);
            if (tempDis < distance)
            {
                distance = tempDis;
                target = item;
            }
        }
    }

    public void NextTurn()
    {
        myTurn = true;
        skillTree.reduceCooldown();
    }

    void attack()
    {
        int mostdmg = int.MinValue, index = -1;
        // Find most damage skill first;
        for (int i = 0; i < skillTree.skills.Count; i++)
        {
            if (skillTree.skills[i].damage > mostdmg)
            {
                mostdmg = skillTree.skills[i].damage;
                index = i;
            }
        }
        skillTree.skills[index]._Execute(target);
        s += "\n" + transform.name + " dealt " + mostdmg + " damage";
    }

    void Walk(int steps)
    {
        // Priority, equal to attack range > within attack range > away from attack range
        BoardGenerator.Cell _cell = gmc.currentCell;
        // Find all walkable tiles, aka destination       
        List<BoardGenerator.Cell> walkableCells = new List<BoardGenerator.Cell>();
        int[] dis; //distance away from target
        int range = steps;
        int cx = (int)_cell.index.x, cy = (int)_cell.index.y;
        for (int x = cx - range; x <= cx + range; x++)
        {
            int val = cx - x;
            val = Mathf.Abs(val);
            val = range - val;
            for (int y = cy - val; y <= cy + val; y++)
            {
                BoardGenerator.Cell temp = GameManagerScript.getBoard().isValidCell(new Vector2(x, y));
                if (temp != null)
                {
                    if (!temp.occupiedObject)
                        walkableCells.Add(temp);
                }
            }
        }
        Vector2 targetCellIndex = target.GetComponent<GridMovementController>().currentCell.index;
        List<BoardGenerator.Cell> bestoptions = new List<BoardGenerator.Cell>();
        dis = new int[walkableCells.Count];
        for (int i = 0; i < walkableCells.Count; i++)
        {
            dis[i] = bg.getCellWalkDistance(walkableCells[i].index, targetCellIndex);
            if (dis[i] == stats.getAttackRange)
                bestoptions.Add(walkableCells[i]);
        }
        // For each des
        if (bestoptions.Count > 0)
        {
            int index = Random.Range(0, bestoptions.Count);
            skillTree.Move(bestoptions[index]);
        }
        else
        {
            for (int i = 0; i < dis.Length; i++)
            {
                if (dis[i] <= stats.getAttackRange)
                {
                    bestoptions.Add(walkableCells[i]);
                }
            }
            if (bestoptions.Count > 0)
            {
                int index = Random.Range(0, bestoptions.Count);
                skillTree.Move(bestoptions[index]);
            }
            else
            {
                int min = int.MaxValue;
                for (int i = 0; i < dis.Length; i++)
                {
                    if (dis[i] < min)
                        min = dis[i];
                }
                for (int i = 0; i < dis.Length; i++)
                {
                    if (dis[i] == min)
                        bestoptions.Add(walkableCells[i]);
                }
                int index = Random.Range(0, bestoptions.Count);
                skillTree.Move(bestoptions[index]);
                
            }
        }
        s += "\n" + transform.name + " walked ";
    }

    void Action()
    {
        Text text = GameObject.Find("EnemyText").GetComponent<Text>();
        s = text.text + "\n";
        if (target == null)
            FindCloestTarget();
        if (target == null)
        {
            Debug.Log("No target found");
            return;
        }
        s += "\n" + transform.name + " targeting " + target.name;
        BoardGenerator.Cell _cell = gmc.currentCell;
        int dis = bg.getCellWalkDistance(_cell.index, target.GetComponent<GridMovementController>().currentCell.index);
        s += "\n" + transform.name + " Walk Distance away from " + target.name + " : " + dis;
        // check self walk distance + attack range
        int range = stats.getMoveSpeed + stats.getAttackRange;
        // if enough range
        if (range >= dis)
        {
            // if attack range too short, walk
            if (stats.getAttackRange < dis)
            {
                Walk(stats.getMoveSpeed);
            }
            // then attack
            // if within attack range, attack with most damage
            attack();
            myTurn = false;
        }
        // if distance too far, walk close to target
        else
        {
            Walk(stats.getMoveSpeed);
            Walk(stats.getMoveSpeed);
            myTurn = false;
        }
        text.text = s;
    }
}
