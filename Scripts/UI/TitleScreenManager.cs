using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private Levels levels;

    private void Awake()
    {
        DontDestroyOnLoad(levels);
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        levels.currentLevel = 0;
        SceneManager.LoadScene("01Battle");
    }
}
