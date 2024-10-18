using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator anim;
    public GameObject enemy;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int Damage)
    {
        if (isDead) return;

        currentHealth -= Damage;

        Debug.Log(Damage);
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<MoveAttackEnemy>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("Destroed", 2f);
    }
    void Destroed()
    {
        Destroy(this.gameObject);
    }
}
