using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableTile : MonoBehaviour
{
    public Tilemap tilemap;        
    public TilemapCollider2D tilemapCollider; 
    public GameObject[] enemies;        

    void Start()
    {
        CheckEnemies();
    }

    void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            DisableTilemap();
        }
    }

    private void DisableTilemap()
    {
        if (tilemap != null)
        {
            tilemap.gameObject.SetActive(false); 
        }

        if (tilemapCollider != null)
        {
            tilemapCollider.enabled = false; 
        }

        Debug.Log("Все враги уничтожены. Tilemap отключен.");
    }
}
