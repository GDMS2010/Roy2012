using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    TurnOrder turnOrder;
    BattleUIScript uiScript;
    Clicker clicker;
    Stats curCharacter;
    int turnCounter = 1; //defaults to turn 1 on start
    static BoardGenerator board;
    static int curCharActions = 0;
    static GameManagerScript instance;
    GameObject inventoryUICanvas;
    GameObject endScreenUICanvas;
    EndUIController endUIController;

    BattleManager m_battleManager;
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
        //Inventory Variables
        inventoryUICanvas = GameObject.FindGameObjectWithTag("InventoryUICanvas");
        if (!inventoryUICanvas) Debug.LogError("Scene has a game manager but no inventory UI");
        else inventoryUICanvas.SetActive(false);//defaults inventory to closed
        //End Screen Variables
        endScreenUICanvas = GameObject.FindGameObjectWithTag("EndScreenUI");
        if (!endScreenUICanvas) Debug.LogError("Scene has a game manager but no end screen UI");
        endUIController = FindObjectOfType<EndUIController>();
        if (!endUIController) Debug.LogError("Scene has no end UI Controller");
        else endUIController.gameObject.SetActive(false);
        clicker = FindObjectOfType<Clicker>();
        m_battleManager = gameObject.AddComponent<BattleManager>();
        if (!m_battleManager) Debug.LogError("Battle Manager Creation Failed");
    }

    private void Start()
    {
        clicker = FindObjectOfType<Clicker>();
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
        skills.StartTurn();
        curCharActions = skills.numActions + skills.numMoves;
        uiScript.SetupActions(skills);
        uiScript.SetTurnUI("Turn " + turnCounter);
        if(!clicker) clicker = FindObjectOfType<Clicker>();
        clicker.CloseTiles();
        if(curCharacter.tag == "Enemy")
        {
            curCharacter.gameObject.GetComponent<EnemyAIMaster>().NextTurn();
        }
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

    public TurnOrder GetTurnOrder() { return turnOrder; }

    public static void SetWinningState(int state)
    {
        instance.endUIController.gameObject.SetActive(true);
        instance.endUIController.ChangeState(state);
        
    }
    
}
