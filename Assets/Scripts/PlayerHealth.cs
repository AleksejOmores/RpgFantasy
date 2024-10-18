using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 5;
    public Slider healthSlider;
    public Animator anim;
    public GameObject player;

    private bool playerDead = false;

    private void Update()
    {
        if (playerHealth <= 0 && !playerDead)
        {
            PlayerDeath();
        }
    }
    public void PlayerHit()
    {
        if (!playerDead)
        {
            playerHealth--;
            healthSlider.value -= 20;

            if (playerHealth <=0)
            {
                PlayerDeath();
            }
        }
    }
    void PlayerDeath()
    {
        playerDead = true;
        anim.SetBool("isDead", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<MovePlayer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("ExitGame", 5f);
    }
    void ExitGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerHit();
        }
    }
}
