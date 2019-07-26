using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BattlePlayer : BattleUnit
{


    private float startX = -6;
    private float startY = -3.2f;
      

    // Start is called before the first frame update
    protected override void Awake()
    {
        statsText = GameObject.Find("PlayerStats").GetComponent<Text>();

        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            //Debug.Log("Attacking...");

            if (isMovingFoward)
            {
                transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;

                if (transform.position.x >= 0)
                {
                    isMovingFoward = false;
                    Attack();
                }
            }
            else
            {
                transform.position += new Vector3(-1 * moveSpeed, 0) * Time.deltaTime;

                if (transform.position.x < startX)
                {
                    isAttacking = false;
                    BattleManager.instance.isPlayerTurn = false;
                }
            }
        }
        else if (Input.GetButton("Fire1") && BattleManager.instance.isPlayerTurn)
        {
            //Debug.Log("Player attack");

            isAttacking = true;
            isMovingFoward = true;
        }

        statsText.text = $"HP: {hp}";
    }

    public override void Attack()
    {
        animator.SetTrigger("PlayerAttack");
        BattleManager.instance.AttackEnemy(Random.Range(16, 24));
        Debug.Log(BattleManager.instance);
        base.Attack();

    }

    protected override void MoveBack()
    {
        Move(startX, startY);

        BattleManager.instance.isPlayerTurn = false;
        isAttacking = false;
    }
}
