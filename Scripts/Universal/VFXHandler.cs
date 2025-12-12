using System.Collections;
using UnityEngine;

public class VFXHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    private Material originalMat;
    [SerializeField] private Material onDamageMat;
    [SerializeField] private float onDamageVFXDuration = .15f;
    private Coroutine onDamageVFXCo;

    [Space]
    [SerializeField] private GameObject bloodSplat;

    private void Awake()
    {
        originalMat = sr.material;
    }

    public void PlayOnDamageVFX()
    {
        Instantiate(bloodSplat, transform.position, Quaternion.identity);

        if (onDamageVFXCo != null)
            StopCoroutine(OnDamageVFXCo());

        onDamageVFXCo = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo()
    {
        sr.material = onDamageMat;

        yield return new WaitForSeconds(onDamageVFXDuration);

        sr.material = originalMat;
    }
}
