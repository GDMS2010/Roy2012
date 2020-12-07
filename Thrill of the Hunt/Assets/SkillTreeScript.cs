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
    [SerializeField]
    public List<ActionScript> skills;

    public int numActions = 1;
    public int numMoves = 1;
    public int maxActions = 1;
    public int maxMoves = 1;
    // Start is called before the first frame update
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
}
