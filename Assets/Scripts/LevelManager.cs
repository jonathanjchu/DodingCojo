using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float turnDelay = .05f;
    public float levelStartDelay = 2f;
    public BoardManager boardScript;
    public string sceneName;
    public static LevelManager instance = null;

    [HideInInspector]
    public bool playersTurn = true;

    [HideInInspector]
    public LevelData data;

    private Text levelText;
    private GameObject levelImage;
    private bool isSetup;
    private bool enemiesMoving;

    private List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log($"New Level Manager for scene {sceneName}");
        }
        else if (instance != this)
        {
            Debug.Log($"Destroying Level Manager for scene {sceneName}");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Returning Level Manager for {sceneName}");
        }

        data = Levels.levelMaps[sceneName];

        boardScript = GetComponent<BoardManager>();
        InitLevel();
    }

    private void InitLevel()
    {
        isSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = sceneName;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(data);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);

        isSetup = false;
    }


    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();

            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (playersTurn || enemiesMoving || isSetup)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }
}
