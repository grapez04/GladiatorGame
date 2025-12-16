using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldUI : MonoBehaviour
{
    private static readonly int FillID = Shader.PropertyToID("_FillPercentage");
    [SerializeField] private Image shieldImage;

    private Material runtimeMat;
    private Coroutine refillRoutine;

    public float Fill { get; private set; } = 100f;

    public bool IsFull => Fill >= 100f;
    public bool IsEmpty => Fill <= 0f;

    private void Awake()
    {
        runtimeMat = Instantiate(shieldImage.material);
        shieldImage.material = runtimeMat;
        SetFill(100f);
    }

    public void SetFill(float value)
    {
        Fill = Mathf.Clamp(value, 0f, 100f);
        runtimeMat.SetFloat(FillID, Fill);
    }

    public void StartRefill(MonoBehaviour owner, float duration)
    {
        if (refillRoutine != null)
            owner.StopCoroutine(refillRoutine);

        refillRoutine = owner.StartCoroutine(RefillRoutine(duration));
    }

    private IEnumerator RefillRoutine(float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            SetFill(Mathf.Lerp(0f, 100f, t / duration));
            yield return null;
        }

        SetFill(100f);
        refillRoutine = null;
    }
}
