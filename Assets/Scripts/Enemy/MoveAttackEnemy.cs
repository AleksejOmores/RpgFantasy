using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAttackEnemy : MonoBehaviour
{
    enum State
    {
        Roaming,
        Chase,
        Run
    }

    private State state;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVector;
    [SerializeField] private int speed;
    [SerializeField] private int chaseSpeed;
    [SerializeField] private int runSpeed;
    [SerializeField] private int detectionRadius;
    [SerializeField] private int attackRadius;
    [SerializeField] private int stopAttackRadius;
    public Transform player;
    private bool isAttacking = false;
    private Coroutine roamingCoroutine;
    private Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        GameObject character = GameObject.Find("Player");
        player = character.transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(Movement());  
    }

    private void Update()
    {
        SetDirectionValues();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            StartAttacking();
        }
        else if (distanceToPlayer <= 2)
        {
            StartChasing();
            StopAttacking();
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            StartRunning();
            StopAttacking();
        }
        else
        {
            StartRoaming();    
            anim.SetBool("isRun", false);
        }
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) { return; }
    }
    private void StartRoaming()
    {
        state = State.Roaming;
        if (roamingCoroutine == null) 
        {
            roamingCoroutine = StartCoroutine(Movement());
        }
    }
    private void StartChasing()
    {
        state = State.Chase;
        if (roamingCoroutine != null)
        {
            StopCoroutine(roamingCoroutine);
            roamingCoroutine = null;
            anim.SetBool("isRun", false);
            anim.SetBool("isMove", true);
        }
        ChasePlayer();
    }
    private void StartRunning()
    {
        state = State.Run;
        if (roamingCoroutine != null)
        {
            StopCoroutine(roamingCoroutine);
            roamingCoroutine = null;
        }
        RunPlayer();
    }
    private void RandomDirection()
    {
        moveVector.x = Random.Range(-1f, 1f);
        moveVector.y = Random.Range(-1f, 1f);
        moveVector = new Vector2(moveVector.x, moveVector.y).normalized;
    }

    IEnumerator Movement()
    {
        while (state == State.Roaming)
        {
            RandomDirection();
            rb.velocity = moveVector * speed;
            anim.SetBool("isMove", true);
            yield return new WaitForSeconds(1.5f);
        }
    }

    void SetDirectionValues()
    {
        anim.SetFloat("DirectionX", moveVector.x);
        anim.SetFloat("DirectionY", moveVector.y);
    }

    void ChasePlayer()
    {
        if (state == State.Chase)  
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            moveVector = directionToPlayer;
            rb.velocity = directionToPlayer * chaseSpeed;     
        }
    }
    void RunPlayer()
    {
        if (state == State.Run)
        {
            anim.SetBool("isRun", true);
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            moveVector = directionToPlayer;
            rb.velocity = directionToPlayer * runSpeed;
        }
    }
    void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
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
        if (isAttacking)  // Исправлено название флага
        {
            isAttacking = false;
            anim.SetBool("isAttack", false);
            CancelInvoke("AttackAnimation");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Water"))
        {
            StartRoaming();
        }
    }
}
