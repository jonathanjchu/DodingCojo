using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BattleUnit : MonoBehaviour
{
    public float moveTime = .03f;
    public float moveBackDelay = .5f;
    public float moveSpeed = 10;
    public int hp;

    public AudioClip attackSound;

    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    protected Animator animator;

    protected bool isMovingFoward;
    protected bool isAttacking;
    protected Text statsText;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        inverseMoveTime = 1 / moveTime;
    }

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Debug.Log($"Waking... {rb2D}");


        inverseMoveTime = 1 / moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        SoundManager.instance.PlaySingle(attackSound);
    }

    protected virtual void Move(float x, float y)
    {
        Vector2 start = transform.position;
        var end = new Vector2(x, y);


        StartCoroutine(SmoothMovement(end));

    }

    protected virtual void MoveBack()
    {

    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {

            Debug.Log(rb2D);

            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        isMovingFoward = false;
    }
}
