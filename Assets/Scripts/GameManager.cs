using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject levelManager;
    public int playerHP = 100;
    public string currentScene;

    public static GameManager instance = null;
       
    private int level = 1;

    // Awake is called at the beginning
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnLevelLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnLevelLoaded;
    //}

    //private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    this.level++;

    //    InitGame();
    //}

    private void OnLevelWasLoaded(int level)
    {
        this.level++;
        InitGame();
    }

    void InitGame()
    {
    }

    public void GameOver()
    {
        //levelText.text = "Game Over";
        //levelImage.SetActive(true);
        enabled = false;
    }



    // Update is called once per frame
    void Update()
    {

    
    }

}
