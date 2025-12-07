using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private Levels levels;

    private void Awake()
    {
        if(FindObjectsByType<Levels>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(levels);
        }
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        foreach (Levels _levels in FindObjectsByType<Levels>(FindObjectsSortMode.None))
        {
            _levels.currentLevel = 0;
        }
        SceneManager.LoadScene("01Battle");
    }
}
