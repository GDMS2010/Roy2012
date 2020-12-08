using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    TurnOrder turnOrder;
    BattleUIScript uiScript;
    Stats curCharacter;
    int turnCounter = 1; //defaults to turn 1 on start
    static BoardGenerator board;
    static int curCharActions = 0;
    static GameManagerScript instance;
    GameObject inventoryUICanvas;
    // Start is called before the first frame update
    void Awake()
    {
        turnOrder = FindObjectOfType<TurnOrder>();
        if (!turnOrder) Debug.LogError("Scene has a game manager script but no turn order");
        uiScript = FindObjectOfType<BattleUIScript>();
        if (!uiScript) Debug.LogError("Scene has a game manager script but no ui script");
        board = FindObjectOfType<BoardGenerator>();
        if (!board) Debug.LogError("Scene has a game manager script but no board");
        instance = this;
        inventoryUICanvas = GameObject.FindGameObjectWithTag("InventoryUICanvas");
        if (!inventoryUICanvas) Debug.LogError("Scene has a game manager but no inventory UI");
        else inventoryUICanvas.SetActive(false);//defaults inventory to closed
    }

    private void Start()
    {
        curCharacter = turnOrder.GetCurrent().GetComponent<Stats>();
        if (!curCharacter) Debug.LogError("Failed to get stats component from current head of turn from TurnOrder script");
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        curCharacter = turnOrder.GetCurrent().GetComponent<Stats>();
        if (!curCharacter) Debug.LogError("Failed to get stats component from current head of turn from TurnOrder script");
        uiScript.SetupUI(curCharacter);
        SkillTreeScript skills = curCharacter.GetComponent<SkillTreeScript>();
        curCharActions = skills.numActions + skills.numMoves;
        skills.StartTurn();
        uiScript.SetupActions(skills);
        uiScript.SetTurnUI("Turn " + turnCounter);
    }

    public void NextTurn()
    {
        turnCounter++;
        turnOrder.NextRound();
        UpdateUI();
    }

    public Stats getCurCharacter()
    {
        return curCharacter;
    }

    public static BoardGenerator getBoard()
    {
        return board;
    }

    public static void SubtractAction()
    {
        curCharActions--;
        if (curCharActions <= 0)
            instance.NextTurn();
    }

    public static void ToggleInventory()
    {
        instance.inventoryUICanvas.SetActive(!instance.inventoryUICanvas.activeSelf);
    }
    
}
