using Assets.Scripts.AbstractFactory;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject slashObject;
    public Tilemap tilemap;
    public GameObject unitPrefab;
    private void Start()
    {
        IUnitFactory unitFactory;
        switch (GameData.DifficultyLevel)
        {
            case 0:
                unitFactory = new EasyUnitFactory(unitPrefab, slashObject, tilemap, 50);
                break;
            case 1:
                unitFactory = new MediumUnitFactory(unitPrefab, slashObject, tilemap, 40);
                break;
            case 2:
                unitFactory = new HardUnitFactory(unitPrefab, slashObject, tilemap, 30);
                break;
            case 3:
                unitFactory = new VeryHardUnitFactory(unitPrefab, slashObject, tilemap, 20);
                break;
            default:
                unitFactory = null;
                break;
        }

        unitFactory?.SpawnUnits();
    }
}
