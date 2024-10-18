using UnityEngine;

namespace Assets.Scripts.AbstractFactory
{
    public class DifficultyController : MonoBehaviour
    {
        private IUnitFactory unitFactory;
        public void OnEasyButtonPressed()
        {
            GameData.DifficultyLevel = 0;
        }

        public void OnMediumButtonPressed()
        {
            GameData.DifficultyLevel = 1;
        }

        public void OnHardButtonPressed()
        {
            GameData.DifficultyLevel = 2;
        }

        public void OnVeryHardButtonPressed()
        {
            GameData.DifficultyLevel = 3;
        }
    }
}
