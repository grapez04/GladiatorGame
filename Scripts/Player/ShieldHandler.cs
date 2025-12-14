using UnityEngine;
using System.Collections;

public class ShieldHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;

    public bool isShielded { get; private set; } // only writable inside ShieldHandler

    [Header("Time Slow Settings")]
    public int shieldCount;
    public float slowDuration = 2f;
    [SerializeField] private float slowTimeScale = 0.3f;

    private Coroutine slowRoutine;

    public void SetShieldCount(int _shieldCount)
    {
        shieldCount = _shieldCount;
    }

    public void Shield()
    {
        if (shieldCount <= 0 || isShielded)
            return;

        shieldCount--;

        // Update stats + UI
        manager.stats.shield = shieldCount;
        manager.playerUI.HandleShields(manager.stats);

        isShielded = true;
        slowRoutine = StartCoroutine(SlowTimeRoutine());
    }

    private IEnumerator SlowTimeRoutine()
    {
        ApplySlowTime();

        yield return new WaitForSecondsRealtime(slowDuration);

        RestoreTime();
        isShielded = false;
        slowRoutine = null;
    }

    private void OnDisable()
    {
        RestoreTime();

        if (slowRoutine != null)
        {
            StopCoroutine(slowRoutine);
            slowRoutine = null;
        }

        isShielded = false;
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
