using System;
using UnityEngine;

public class trapsAttack : MonoBehaviour
{
    private float playerDistance = 0.1f;
    private Animator anim;
    public float distance;
    public GameObject playerObj;
    public GameObject enemyObj;
    void Start()
    {
        try
        {
            anim = GetComponent<Animator>();
            GameObject player = GameObject.Find("Player");
            GameObject enemy = GameObject.Find("Enemy");
            playerObj = player.gameObject;
            enemyObj = enemy.gameObject;
        }
        catch (Exception ex)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isAttack", true);
            playerObj.GetComponent<PlayerHealth>().PlayerHit();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("isAttack", true);
            enemyObj.GetComponent<EnemyHealth>().TakeDamage(20);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
}
