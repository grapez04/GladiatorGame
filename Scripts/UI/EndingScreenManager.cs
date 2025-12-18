using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreenManager : MonoBehaviour
{
    [SerializeField] private Cutscene[] ending1;
    [SerializeField] private Cutscene[] ending2;
    [SerializeField] private Cutscene[] ending3;
    [Space]
    [Header("Ending")]
    [SerializeField] private GameObject cutscenePrefab;
    [SerializeField] private Cutscene[] chosenEnding;

    [Space]
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI finalAge;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI deathCount;

    [Space]
    [SerializeField] private Animator crossfade;

    private GameObject instantiationRef;
    private bool faded = false;

    private int currentCutsceneIndex = 0;

    private void Start()
    {
        SetEnding();
        PlayEnding();
        SetOverview();
    }

    private void SetEnding()
    {
        int age = GameManager.playerAge;
        finalAge.text = age.ToString();

        if (age < 50)
        {
            chosenEnding = ending1;
        }
        else if (age >= 50 && age < 70)
        {
            chosenEnding = ending2;
        }
        else if (age >= 70)
        {
            chosenEnding = ending3;
        }
    }

    private void PlayEnding()
    {
        currentCutsceneIndex = 0;
        PlayCurrentCutscene();
    }

    private void PlayCurrentCutscene()
    {
        // Clean up previous cutscene if needed
        if (instantiationRef != null)
        {
            Destroy(instantiationRef);
        }

        GameObject instantiatedEnding = Instantiate(cutscenePrefab);
        instantiationRef = instantiatedEnding;

        CutsceneHandle handle = instantiatedEnding.GetComponentInChildren<CutsceneHandle>();

        Cutscene cutscene = chosenEnding[currentCutsceneIndex];

        handle.spriteHolder.sprite = cutscene.art;
        handle.TypeText(cutscene.monologue);

        handle.OnContinue += GoToNextCutscene;
    }

    private void GoToNextCutscene()
    {
        currentCutsceneIndex++;

        if (currentCutsceneIndex >= chosenEnding.Length)
        {
            FadeOut(); // last cutscene finished
        }
        else
        {
            PlayCurrentCutscene(); // play next one
        }
    }


    private void SetOverview()
    {
        enemiesKilled.text = GameManager.enemiesKilled.ToString();
        deathCount.text = GameManager.playerDeathCount.ToString();
    }

    public void ToTitle()
    {
        GameManager.ResetGameState();
        SceneManager.LoadScene("00MainMenu");
    }

    private void FadeOut()
    {
        if (faded) return;

        faded = true;

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        CloseEnding();
    }

    private void CloseEnding()
    {
        Destroy(instantiationRef);
    }
}
