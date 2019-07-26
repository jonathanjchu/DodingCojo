using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{
    public Text hpText;
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float newSceneLoadDelay = 0.3f;


    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    private Animator animator;


    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        hp = GameManager.instance.playerHP; // for saving during level changes
        hpText.text = "HP: " + hp;

        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerHP = hp;
    }


    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.instance.playersTurn)
        {
            return;
        }

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }

    }

    private void CheckIfGameOver()
    {
        if (hp <= 0)
        {
            GameManager.instance.GameOver();
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
        hpText.text = "HP: " + hp;

        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }

        CheckIfGameOver();

        LevelManager.instance.playersTurn = false;
        //GameManager.instance.playersTurn = true;


    }

    protected override void OnCantMove<T>(T component)
    {
        //Wall hitWall = component as Wall;
        //hitWall.DamageWall(this.wallDamage);
        //animator.SetTrigger("playerChop");
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(LevelManager.instance.data.nextLevel);
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        hp -= loss;
        hpText.text = "HP: " + hp;

        CheckIfGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check what trigger objects the player has collided with
        if (other.tag == "Exit")
        {
            //Invoke("Restart", restartLevelDelay);
            enabled = false;
            Debug.Log("Reached exit, loading next scene");
            Invoke("LoadNextScene", newSceneLoadDelay);
            
        }
        else if (other.tag == "Food")
        {
            hp += pointsPerFood;
            other.gameObject.SetActive(false);
            hpText.text = $"+{pointsPerFood} HP: {hp}";
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
        }
        else if (other.tag == "Soda")
        {
            hp += pointsPerSoda;
            other.gameObject.SetActive(false);
            hpText.text = $"+{pointsPerSoda} HP: {hp}";
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
        }
    }


}
