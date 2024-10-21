using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public int speed;
    private Rigidbody2D rb;
    [SerializeField] public Vector2 movementVector;
    private Vector3 difference;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetDirectionValues();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
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
}
