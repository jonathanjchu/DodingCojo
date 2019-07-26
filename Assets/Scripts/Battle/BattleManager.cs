using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    public bool isPlayerTurn = true;

    public GameObject player;
    public GameObject[] enemies;

    public static BattleManager instance = null;

    private Text gotext;
    private GameObject goimage;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        isPlayerTurn = true;

        gotext = GameObject.Find("GameOverText").GetComponent<Text>();
        goimage = GameObject.Find("GameOverImage");
        goimage.SetActive(false);
    }

    void Awake()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        var statsText = GameObject.Find("EnemyStats").GetComponent<Text>();
        //if (statsText.text.Length > 30)
        //    statsText.text = "Enemy HP:";
    }

    public void AddEnemytoList(BattleEnemy enemy)
    {
        //enemies.Add(enemy);
    }

    public void AttackEnemy(int dmg)
    {
        if (enemies.Length > 0)
        {
            var benemy = enemies.Last().GetComponent<BattleEnemy>();
            if (benemy != null)
                benemy.hp -= dmg;
            else
                Debug.LogError("Enemy object is null");

            if (benemy.hp <= 0)
            {
                
                enemies = enemies.Take(enemies.Length - 1).ToArray();
                Debug.Log("Removing enemy");
            }
        }
    }

    public void AttackPlayer(int dmg)
    {
        var bplayer = player.GetComponent<BattlePlayer>();
        if (bplayer != null)
            bplayer.hp -= dmg;
        else
            Debug.LogError("Player object is null");

        if (bplayer.hp <= 0)
        {
            // game over
            GameOver();
            
        }
    }

    void GameOver()
    {
        gotext.text = "Game Over";
        goimage.SetActive(true);

        Invoke("Quit", 2f);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
