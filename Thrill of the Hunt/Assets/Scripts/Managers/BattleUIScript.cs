using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleUIScript : MonoBehaviour
{
    Canvas m_canvas;
    [SerializeField]
    Image characterPortrait;
    [SerializeField]
    Text characterName;
    [SerializeField]
    GameObject skillSlots;
    [SerializeField]
    Text turnCounter;
    // Start is called before the first frame update
    void Awake()
    {
        m_canvas = GetComponent<Canvas>();
        if (!m_canvas) Debug.LogError("Canvas Expetect on BattleUIScript");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupUI(Stats stats)
    {
        characterPortrait.sprite = stats.GetSprite();
        characterName.text = stats.getCharacterName;
    }

    public void SetTurnUI(string text)
    {
        turnCounter.text = text;
    }

    public void SetupActions(SkillTreeScript skills)
    {
        for (int i = 0; i < skillSlots.transform.childCount; i++)
        {
            skillSlots.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < skills.skills.Count; i++)
        {
            //Setup on click event to run the skill functionality
            GameObject child = skillSlots.transform.GetChild(i).gameObject;
            Button buttonRef = child.GetComponent<Button>();
            buttonRef.onClick.RemoveAllListeners();
            buttonRef.onClick.AddListener(skills.skills[i].Execute);
            skillSlots.transform.GetChild(i).GetComponent<Image>().sprite = skills.skills[i].actionImage;
            if (skillSlots.transform.GetChild(i).GetComponent<Image>().sprite == null)
                skillSlots.transform.GetChild(i).gameObject.SetActive(false);
            else
                skillSlots.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
