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
    [SerializeField] private Cutscene[] intro;

    private bool faded = false;

    private int currentIntroIndex = 0;
    private GameObject instantiationRef;

    private void Awake()
    {
        if(FindObjectsByType<Levels>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(levels);
        }
    }

    private void StartGame()
    {
        GameManager.ResetGameState();
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
        currentIntroIndex = 0;
        PlayCurrentIntroFrame();
    }

    private void PlayCurrentIntroFrame()
    {
        // Clean up previous frame
        if (instantiationRef != null)
        {
            Destroy(instantiationRef);
        }

        instantiationRef = Instantiate(cutscenePrefab);

        CutsceneHandle handle = instantiationRef.GetComponentInChildren<CutsceneHandle>();

        Cutscene cutscene = intro[currentIntroIndex];

        handle.spriteHolder.sprite = cutscene.art;
        handle.TypeText(cutscene.monologue);

        handle.OnContinue += GoToNextIntroFrame;
    }

    private void GoToNextIntroFrame()
    {
        currentIntroIndex++;

        if (currentIntroIndex >= intro.Length)
        {
            StartGame(); // finished intro
        }
        else
        {
            PlayCurrentIntroFrame(); // next frame
        }
    }
}
