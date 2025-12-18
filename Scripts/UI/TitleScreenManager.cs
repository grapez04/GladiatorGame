using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private Levels levels;

    [SerializeField] private Animator crossfade;

    [Space]
    [Header("Intro")]
    [SerializeField] private GameObject cutscenePrefab;
    [SerializeField] private Cutscene intro;

    private bool faded = false;

    private void Awake()
    {
        if(FindObjectsByType<Levels>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(levels);
        }
    }

    private void StartGame()
    {
        PlayerPrefs.DeleteAll();
        foreach (Levels _levels in FindObjectsByType<Levels>(FindObjectsSortMode.None))
        {
            _levels.currentLevel = 0;
        }

        StartCoroutine(LoadScene());
    }

    public void FadeToCutscene()
    {
        if (faded) return;

        faded = true;
        StartCoroutine(LoadCutscene());
    }

    private IEnumerator LoadCutscene()
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        StartIntro();
    }

    private IEnumerator LoadScene()
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("01Battle");
    }

    private void StartIntro()
    {
        GameObject instantiatedIntro = Instantiate(cutscenePrefab);
        CutsceneHandle handle = instantiatedIntro.GetComponentInChildren<CutsceneHandle>();
        handle.spriteHolder.sprite = intro.art;
        handle.TypeText(intro.monologue);
        handle.OnContinue += StartGame;
    }
}
