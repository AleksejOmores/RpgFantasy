using UnityEngine;

public class MoveAttackEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVector;
    [SerializeField] private int speed = 3;
    [SerializeField] private int chaseSpeed = 6;
    [SerializeField] private int detectionRadius = 5;
    [SerializeField] private int attackRadius = 1;
    [SerializeField] private int stopAttackRadius = 2;
    public Transform player;
    private bool isChasing = false;
    private bool isAttack = false;

    private void Start()
    {
        GameObject character = GameObject.Find("Player"); 
        player = character.transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        RandomDirection();
    }
    private void Update()
    {
        movement();
        SetDirectionValues();

        float distancePlayer = Vector2.Distance(transform.position, player.position);

        if (distancePlayer <= attackRadius)
            StartAttacking();
        else if (distancePlayer <= stopAttackRadius)
        {
            StopAttacking();
            ChasePlayer();
        }
        else if (distancePlayer <= detectionRadius)
        {
            isChasing = false;
            StopAttacking();
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                RandomDirection();
            }
            movement();
        }
    }
    void RandomDirection()
    {
        moveVector.x = Random.Range(-1f, 1f);
        moveVector.y = Random.Range(-1f, 1f);
        moveVector = new Vector2(moveVector.x, moveVector.y).normalized;
    }
    void movement()
    {
        rb.velocity = moveVector * speed;
        if (moveVector.x != 0 && moveVector.y != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);
    }
    void SetDirectionValues()
    {
        anim.SetFloat("DirectionX", moveVector.x);
        anim.SetFloat("DirectionY", moveVector.y);
    }
    void ChasePlayer()
    {
        Vector2 directionPlayer = (player.position - transform.position).normalized;
        moveVector = directionPlayer;
        rb.velocity = directionPlayer * chaseSpeed;
    }
    void StartAttacking()
    {
        if (!isAttack)
        {
            isAttack = true;
            anim.SetBool("isAttack", true);
            InvokeRepeating("AttackAnimation", 0f, 0.5f);
        }
    }
    void AttackAnimation()
    {
        anim.SetBool("isAttack", true);
    }
    void StopAttacking()
    {
        if (isAttack)
        {
            isAttack = false;
            anim.SetBool("isAttack", false);
            CancelInvoke("AttackAnimation");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            RandomDirection();
        }
    }
}
