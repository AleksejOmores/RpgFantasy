using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class SceneStartScene : MonoBehaviour
{
    [SerializeField] public Button Start;
    [SerializeField] public Button Com1;
    [SerializeField] public Button Com2;
    [SerializeField] public Button Com3;
    [SerializeField] public Button Com4;

    public void StartButton()
    {
        Start.gameObject.SetActive(false);
        Com1.gameObject.SetActive(true);
        Com2.gameObject.SetActive(true);
        Com3.gameObject.SetActive(true);
        Com4.gameObject.SetActive(true);
    }
    public void ComSceneGame()
    {
        SceneManager.LoadScene("SceneGame");
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
