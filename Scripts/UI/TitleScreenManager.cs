using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private Levels levels;

    [Space]
    [Header("Intro")]
    [SerializeField] private GameObject cutscenePrefab;
    [SerializeField] private Cutscene intro;

    private void Awake()
    {
        if(FindObjectsByType<Levels>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(levels);
        }
    }

    public void StartIntro()
    {
        GameObject instantiatedCutscene = Instantiate(cutscenePrefab);
        CutsceneHandle handle = instantiatedCutscene.GetComponentInChildren<CutsceneHandle>();
        handle.spriteHolder.sprite = intro.art;
        handle.TypeText(intro.monologue);
        handle.OnContinue += StartGame;
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
