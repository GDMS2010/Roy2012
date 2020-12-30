using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatSheet : MonoBehaviour
{
    public Stats[] charaters;
    public int index;
    public GameObject Name;
    public GameObject Level;
    public GameObject Defense;
    public GameObject Nimbleness;
    public GameObject Brawn;
    public GameObject Brain;
    public GameObject Vigor;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;

        UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            charaters[index].leveling.AddExp(charaters[index].leveling.GetXPForLevel(charaters[index].leveling.currLevel)) ;
        }
    }

    public void CycleForward()
    {
        if (index >= charaters.Length)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        UpdateUI();
    }

    public void CycleBackwards()
    {
        if (index >= charaters.Length -1)
        {
            index = charaters.Length - 1;
        }
        else
        {
            index--;
        }

        UpdateUI();
    }

    public void IncreaseDefense()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].defense++;
            charaters[index].statpoint--;
        }
    }

    public void IncreaseNimbleness()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].nimbleness++;
            charaters[index].statpoint--;
        }
    }

    public void IncreaseBrawn()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].brawn++;
            charaters[index].statpoint--;
        }
    }

    public void IncreaseBrain()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].brain++;
            charaters[index].statpoint--;
        }
    }

    public void IncreaseVigor()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].vigor++;
            charaters[index].statpoint--;
        }
    }
    private void UpdateUI()
    {
        Name.GetComponent<TextMeshPro>().text = charaters[index].getCharacterName;
        Level.GetComponent<TextMeshPro>().text = charaters[index].lv.ToString();
        Defense.GetComponent<TextMeshPro>().text = charaters[index].defense.ToString();
        Nimbleness.GetComponent<TextMeshPro>().text = charaters[index].nimbleness.ToString();
        Brawn.GetComponent<TextMeshPro>().text = charaters[index].brawn.ToString();
        Brain.GetComponent<TextMeshPro>().text = charaters[index].brain.ToString();
        Vigor.GetComponent<TextMeshPro>().text = charaters[index].vigor.ToString();
    }
}
