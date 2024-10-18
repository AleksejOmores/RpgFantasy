using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AbstractFactory
{
    public class EasyUnitFactory : MonoBehaviour, IUnitFactory
    {
        private GameObject unitPrefab;
        private GameObject slashObject;
        private Tilemap tilemap;
        private int damagePlayer;
        public EasyUnitFactory(GameObject prefab, GameObject slashObject, Tilemap tilemap, int damagePlayer)
        {
            unitPrefab = prefab;
            this.tilemap = tilemap;
            this.slashObject = slashObject;
            this.damagePlayer = damagePlayer;
        }
        public void SpawnUnits()
        {
            int numberOfUnits = Random.Range(1, 4);  // Случайное количество юнитов от 3 до 5
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
                slashObject.GetComponent<HitsAttackPlayer>().damage = damagePlayer;
            }
            else
            {
                SpawnRandomUnit();
            }
        }
    }
}
