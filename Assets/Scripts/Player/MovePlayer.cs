using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public static MovePlayer Instance;
    private Animator animator;
    [SerializeField] public int speed;
    private Rigidbody2D rb;
    [SerializeField] public Vector2 movementVector;
    private Vector3 difference;
    public SpriteRenderer player;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetDirectionValues();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerFacingDirection();
            animator.SetTrigger("Attack");

            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.z = 0;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void SetDirectionValues()
    {
        animator.SetFloat("DirectionX", movementVector.x);
        animator.SetFloat("DirectionY", movementVector.y);
    }

    public void Movement()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        movementVector.Normalize();
        if (movementVector != Vector2.zero)
        {
            animator.SetBool("move", true);

             rb.MovePosition(rb.position + movementVector * (speed * Time.fixedDeltaTime));
        }
        else
        {
            animator.SetBool("move", false);

            rb.velocity = Vector2.zero;
        }
        if (movementVector != Vector2.zero && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8;
            animator.SetBool("move", false);
            animator.SetBool("isShift", true);
            rb.MovePosition(rb.position + movementVector * (speed * Time.fixedDeltaTime));
        }
        else
        {
            speed = 4;

            animator.SetBool("isShift", false);
        }
    }

    private void PlayerFacingDirection()
    {
        Vector3 vector = Input.mousePosition;
        Vector3 playerScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (vector.x < playerScreen.x)
            player.flipX = true;
        else
            player.flipX = false;

    }
}
