using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemy : BattleUnit
{
    private float startX;
    private float startY;


    private Transform target;
    private GameObject targetPlayer;

    // Start is called before the first frame update
    protected override void Awake()
    {
        if (BattleManager.instance != null)
            BattleManager.instance.AddEnemytoList(this);
        else
            Debug.Log("Battle Manager is NULL");

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").transform;

        startX = transform.position.x;
        startY = transform.position.y;
        isAttacking = false;

        statsText = GameObject.Find("EnemyStats").GetComponent<Text>();

        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            //Debug.Log("Enemy attacking...");

            if (isMovingFoward)
            {
                transform.position += new Vector3(-1 * moveSpeed, 0) * Time.deltaTime;

                if (transform.position.x <= 0)
                {
                    isMovingFoward = false;
                    Attack();
                }
            }
            else
            {
                transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;

                if (transform.position.x > startX)
                {
                    isAttacking = false;
                    BattleManager.instance.isPlayerTurn = true;
                }
            }
        }
        else if (!BattleManager.instance.isPlayerTurn)
        {
            isAttacking = true;
            isMovingFoward = true;

            //BattleManager.instance.isPlayerTurn = true;
        }

        statsText.text += $"\n{hp}";
    }

    public override void Attack()
    {

        animator.SetTrigger("enemy1Attack");

        BattleManager.instance.AttackPlayer(Random.Range(8, 12));

        Debug.Log(BattleManager.instance);
        base.Attack();

    }

}
