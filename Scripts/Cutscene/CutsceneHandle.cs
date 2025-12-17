using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneHandle : MonoBehaviour
{
    public Image spriteHolder;
    [SerializeField] private TextMeshProUGUI textHolder;
    public TextMeshProUGUI continueText;

    public bool canContinue = false;
    public float continueDelay = 2f;
    public float typingSpeed = 0.04f;

    public event System.Action OnContinue;

    Coroutine typingCoroutine;

    private void Start()
    {
        continueText.gameObject.SetActive(false);
        StartCoroutine(ShowContinueAfterDelay());
    }

    public void TypeText(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeTextRoutine(text));
    }

    private IEnumerator TypeTextRoutine(string text)
    {
        textHolder.text = "";
        canContinue = false;
        continueText.gameObject.SetActive(false);

        foreach (char c in text)
        {
            textHolder.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        StartCoroutine(ShowContinueAfterDelay());
    }

    private IEnumerator ShowContinueAfterDelay()
    {
        yield return new WaitForSeconds(continueDelay);
        continueText.gameObject.SetActive(true);
        canContinue = true;
    }

    public void Continue()
    {
        if (!canContinue) return;

        Debug.Log("Continued");
        OnContinue?.Invoke();
    }
}
