using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
