using UnityEngine;
using System.Collections;

public class ShieldHandler : MonoBehaviour
{
    [Header("Time Slow Settings")]
    public int shieldCount;
    public float slowDuration = 1f;
    [SerializeField] private float slowTimeScale = 0.3f;

    private Coroutine slowRoutine;

    public void Shield()
    {
        if (slowRoutine != null)
            StopCoroutine(slowRoutine);

        slowRoutine = StartCoroutine(SlowTimeRoutine());
    }

    private IEnumerator SlowTimeRoutine()
    {
        ApplySlowTime();

        yield return new WaitForSecondsRealtime(slowDuration);

        RestoreTime();
        slowRoutine = null;
    }

    private void OnDisable()
    {
        // Scene reload / restart safety
        RestoreTime();

        if (slowRoutine != null)
        {
            StopCoroutine(slowRoutine);
            slowRoutine = null;
        }
    }

    private void ApplySlowTime()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void RestoreTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
