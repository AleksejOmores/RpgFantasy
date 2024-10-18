using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AbstractFactory
{
    public class HardUnitFactory : MonoBehaviour, IUnitFactory
    {
        private GameObject unitPrefab;
        private GameObject slash;
        private Tilemap tilemap;
        private int damagePlayer;
        public HardUnitFactory(GameObject prefab, GameObject slash, Tilemap tilemap, int damagePlayer)
        {
            unitPrefab = prefab;
            this.tilemap = tilemap;
            this.slash = slash;
            this.damagePlayer = damagePlayer;
        }
        public void SpawnUnits()
        {
            int numberOfUnits = Random.Range(7, 10);  // Случайное количество юнитов от 3 до 5
            for (int i = 0; i < numberOfUnits; i++)
            {
                SpawnRandomUnit();
            }
            Debug.Log(numberOfUnits);
        }
        private void SpawnRandomUnit()
        {
            Vector3Int randomTilePosition = new Vector3Int(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            if (tilemap.HasTile(randomTilePosition))
            {
                Vector3 worldPosition = tilemap.GetCellCenterWorld(randomTilePosition);
                Instantiate(unitPrefab, worldPosition, Quaternion.identity);
                slash.GetComponent<HitsAttackPlayer>().damage = damagePlayer;
            }
            else
            {
                SpawnRandomUnit();
            }
        }
    }
}
