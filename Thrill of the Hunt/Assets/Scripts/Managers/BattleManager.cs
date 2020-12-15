using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;
    private List<GameObject> enemies;
    private List<GameObject> players;

    int state = 0;
    /**
     * states: 0 = neutral
     *         1 = player win
     *         2 = enemy win
     * **/
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
        {
            Destroy(instance);
            instance = this;
        }
        GetCharacters();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].gameObject.activeSelf)
                enemies.Remove(enemies[i]);
        }
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].gameObject.activeSelf)
                players.Remove(players[i]);
        }
        if (enemies.Count == 0)
            GameManagerScript.SetWinningState(1);
        if (players.Count == 0)
            GameManagerScript.SetWinningState(2);
    }

    void GetCharacters()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
    }
}
