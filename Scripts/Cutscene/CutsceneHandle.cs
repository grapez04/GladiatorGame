using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class CutsceneHandle : MonoBehaviour
{
    public Image spriteHolder;
    [SerializeField] private TextMeshProUGUI textHolder;
    public TextMeshProUGUI continueText;
    [SerializeField] private Animator animator;

    public bool canContinue = false;
    public float typingSpeed = 0.04f;

    public event System.Action OnContinue;

    private Coroutine typingCoroutine;

    private string[] currentMonologue;
    private int currentLineIndex = 0;

    private void Start()
    {
        continueText.gameObject.SetActive(false);
    }

    public void SetProperties(Cutscene scene)
    {
        spriteHolder.sprite = scene.art;

        currentMonologue = scene.monologue;
        currentLineIndex = 0;

        // Animation
        if (scene.controller != null)
        {
            animator.runtimeAnimatorController = scene.controller;
            animator.enabled = true;
            animator.Rebind();
        }
        else
        {
            animator.runtimeAnimatorController = null;
            animator.enabled = false;
        }

        TypeCurrentLine();
    }

    private void TypeCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(
            TypeTextRoutine(currentMonologue[currentLineIndex])
        );
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

        // Typing finished
        canContinue = true;
        continueText.gameObject.SetActive(true);
    }

    public void Continue()
    {
        if (!canContinue) return;

        bool isBeforeLastLine = currentLineIndex == currentMonologue.Length - 2;

        if (isBeforeLastLine && animator.enabled)
        {
            animator.SetTrigger("Start");
        }

        // More lines left in this cutscene?
        if (currentLineIndex < currentMonologue.Length - 1)
        {
            currentLineIndex++;
            TypeCurrentLine();
        }
        else
        {
            // Cutscene finished
            Debug.Log("Cutscene finished");
            OnContinue?.Invoke();
        }
    }
}
