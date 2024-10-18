using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public int speed = 10;
    private Rigidbody2D rb;
    [SerializeField] public Vector2 movementVector;
    private Vector3 difference;
    public GameObject endLevel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
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
        SetDerictionValues();
    }
    public void SetDerictionValues()
    {
        animator.SetFloat("DirectionX", movementVector.x);
        animator.SetFloat("DirectionY", movementVector.y);
    }
    public void Movement()
    {
        animator.SetBool("move", true);
        movementVector.x = Input.GetAxis ("Horizontal");
        movementVector.y = Input.GetAxis ("Vertical");
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);

        if (movementVector.x == 0 && movementVector.y == 0)
        {
            animator.SetBool("move", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().position = Vector2.zero;
            this.gameObject.GetComponent<MovePlayer>().enabled = false;
            endLevel.gameObject.SetActive(true);
        }
    }
}
