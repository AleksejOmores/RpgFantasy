using UnityEngine;

public class HitsAttackPlayer : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        GameObject sU = GameObject.Find("SlashUp");
        GameObject sD = GameObject.Find("SlashDown");
        GameObject sR = GameObject.Find("SlashRight");
        GameObject sL = GameObject.Find("SlashLeft");
        sD.gameObject.GetComponent<HitsAttackPlayer>().damage = sU.GetComponent<HitsAttackPlayer>().damage;
        sR.gameObject.GetComponent<HitsAttackPlayer>().damage = sU.GetComponent<HitsAttackPlayer>().damage;
        sL.gameObject.GetComponent<HitsAttackPlayer>().damage = sU.GetComponent<HitsAttackPlayer>().damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && Input.GetKey(KeyCode.Mouse0))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
