using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderSkills : SkillTreeScript
{
    bool isUsed = false;
    private void Awake()
    {
        ActionScript moveAction = gameObject.AddComponent(typeof(ActionScript)) as ActionScript;
        moveAction.action = new UnityEngine.Events.UnityEvent();
        moveAction.action.AddListener(HolyCleave);
        moveAction.actionImage = specialMove1Image;
        skills.Add(moveAction);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HolyCleave()
    {
        //If not used
            //Get encounter SP count;
            //If more than 1, use this skill
        numActions++;
            //Used
    }
}
