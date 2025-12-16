using UnityEngine;
using System.Collections;

public class ShieldHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;

    public bool isShielded { get; private set; } // only writable inside ShieldHandler

    [Header("Time Slow Settings")]
    public int shieldCount;
    private float slowDuration = 3f; // Shorten by age
    private float slowTimeScale = 0.3f;
    private float refillDuration = 9f; // Lengthen by age

    private Coroutine slowRoutine;
    private ShieldUI activeShield;

    [Header("Effects")]
    [SerializeField] private AudioClip tune;

    public void SetShieldCount(int _shieldCount)
    {
        shieldCount = _shieldCount;
    }

    public void Shield()
    {
        if (isShielded) return;

        activeShield = manager.playerUI.GetNextAvailableShield();
        if (activeShield == null) return;

        slowRoutine = StartCoroutine(SlowTimeRoutine());
    }

    private IEnumerator SlowTimeRoutine()
    {
        isShielded = true;
        ApplySlowTime();

        // FX
        manager.sFXHandler.PlayLoopSFX(tune);

        float t = 0f;

        while (t < slowDuration)
        {
            t += Time.unscaledDeltaTime;
            float fill = Mathf.Lerp(100f, 0f, t / slowDuration);
            activeShield.SetFill(fill);
            yield return null;
        }

        activeShield.SetFill(0f);
        activeShield.StartRefill(this, refillDuration);

        RestoreTime();
        isShielded = false;
        activeShield = null;
        slowRoutine = null;
    }

    private void ApplySlowTime()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void RestoreTime()
    {
        manager.sFXHandler.StopLastLoop();

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void OnDisable()
    {
        RestoreTime();
        isShielded = false;
    }
}
