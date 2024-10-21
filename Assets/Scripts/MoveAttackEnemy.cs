using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAttackEnemy : MonoBehaviour
{
    enum State
    {
        Roaming,
        Chase
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
    private bool isChasing = false;
    private bool isAttacking = false;
    private Coroutine roamingCoroutine;  // Храним ссылку на корутину

    private void Awake()
    {
        GameObject character = GameObject.Find("Player");
        player = character.transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamingCoroutine = StartCoroutine(Movement());  // Сохраняем ссылку на корутину
    }

    private void Update()
    {
        SetDirectionValues();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            StartAttacking();
        }
        else if (distanceToPlayer <= stopAttackRadius)
        {
            StopAttacking();
            if (state != State.Chase)
            {
                StartChasing();  // Запускаем преследование, если не в этом состоянии
            }
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            StopAttacking();
            if (state != State.Chase)
            {
                StartChasing();  // Переход к состоянию "Chase"
            }
        }
        else
        {
            if (state != State.Roaming)
            {
                StartRoaming();  // Возвращаемся к блужданию, если не в этом состоянии
            }
        }
    }

    private void StartRoaming()
    {
        state = State.Roaming;
        isChasing = false;
        if (roamingCoroutine == null)  // Проверяем, что корутина не запущена
        {
            roamingCoroutine = StartCoroutine(Movement());
        }
    }

    private void StartChasing()
    {
        state = State.Chase;
        isChasing = true;
        if (roamingCoroutine != null)  // Останавливаем корутину блуждания
        {
            StopCoroutine(roamingCoroutine);
            roamingCoroutine = null;
        }
        ChasePlayer();
    }

    private Vector2 RandomDirection()
    {
        moveVector.x = Random.Range(-1f, 1f);
        moveVector.y = Random.Range(-1f, 1f);
        moveVector = new Vector2(moveVector.x, moveVector.y).normalized;
        return moveVector;
    }

    private IEnumerator Movement()
    {
        while (state == State.Roaming)
        {
            Vector2 vector = RandomDirection();
            rb.velocity = vector * speed;
            anim.SetBool("isMove", true);
            yield return new WaitForSeconds(2f);
        }
        anim.SetBool("isMove", false);  // Останавливаем анимацию при завершении блуждания
    }

    void SetDirectionValues()
    {
        anim.SetFloat("DirectionX", moveVector.x);
        anim.SetFloat("DirectionY", moveVector.y);
    }

    private void ChasePlayer()
    {
        if (state == State.Chase)  // Проверяем, что находимся в состоянии "Chase"
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            moveVector = directionToPlayer;
            rb.velocity = directionToPlayer * chaseSpeed;
            anim.SetBool("isMove", true);  // Включаем анимацию движения при преследовании
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
        if (isAttacking)
        {
            isAttacking = false;
            anim.SetBool("isAttack", false);
            CancelInvoke("AttackAnimation");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Water"))
        {
            RandomDirection();
        }
    }
}
