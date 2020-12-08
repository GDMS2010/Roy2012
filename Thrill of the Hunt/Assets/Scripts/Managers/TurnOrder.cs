using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrder : MonoBehaviour
{
    //keep all the character in the level
    public List<GameObject> testList;
    public List<GameObject> objQueue;
    public int index;

    //Turn Order Icon
    public List<Image> iconList;
    // Start is called before the first frame update
    void Start()
    {
        objQueue = new List<GameObject>();
        iconList = new List<Image>();
        setup(testList);
        UISetup();
        IconUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setup(List<GameObject> objList)
    {
        if (objList.Count == 0)
            Debug.LogError("Nothing in the List!");
        //Save all game objeet into same list
        //and get each Nimbleness
        var list = new List<KeyValuePair<GameObject, int>>();
        foreach (var item in objList)
        {
            int val = item.GetComponent<Stats>().initiative;
            list.Add(new KeyValuePair<GameObject, int>(item, val));
        }
        //Sort by its value
        list.Sort((x, y) => (y.Value.CompareTo(x.Value)));

        //add to Queue;
        foreach (var item in list)
        {
            objQueue.Add(item.Key);
        }
    }

    // call when character end round
    public void NextRound()
    {
        index = ++index >= objQueue.Count ? 0 : index;
        IconUpdate();
    }

    // call when any character die
    public void removeCharacter(GameObject obj)
    {
        if (!objQueue.Remove(obj))
            Debug.LogError("Cant find the object!");
        else
            IconUpdate();
    }

    //Initial set up when load scene
    void UISetup()
    {
        Transform t = transform.Find("Images");
        for (int i = 0; i < t.childCount; i++)
        {
            iconList.Add(t.GetChild(i).GetComponent<Image>());
        }
    }
    //Call when next round or any character die
    void IconUpdate()
    {
        int oq_count = objQueue.Count;
        for (int i = 0; i < oq_count; i++)
        {
            iconList[i].sprite = objQueue[i].GetComponent<Stats>().GetSprite();
        }

        for (int i = 0; i < iconList.Count; i++)
        {
            if(i>= objQueue.Count)
            {
                iconList[i].gameObject.SetActive(false);
            }
            else
            {
                iconList[i].sprite = objQueue[(i+index) % oq_count].GetComponent<Stats>().GetSprite();
            }
        }
    }

    public GameObject GetCurrent()
    {
        return objQueue[index];
    }

    //Debug
    public void Dead_test(int id)
    {
        int v = (id + index) % objQueue.Count;
        objQueue[v].GetComponent<Health>().hurt(100000);
    }
}
