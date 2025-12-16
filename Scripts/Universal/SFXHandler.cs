using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    public AudioClip slashClip;

    [HideInInspector] public AudioSource lastSource;

    public void PlaySFX(AudioClip clip)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.playOnAwake = false;
        newSource.loop = false;
        newSource.spatialBlend = 0f; // 0 = 2D, set to 1 for 3D if needed

        newSource.Play();

        Destroy(newSource, clip.length);
    }

    public void PlayLoopSFX(AudioClip clip)
    {
        if (lastSource != null)
            Destroy(lastSource);

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = true;
        newSource.playOnAwake = false;
        newSource.spatialBlend = 0f;

        newSource.Play();
        lastSource = newSource;
    }

    public void StopLastLoop()
    {
        if (lastSource == null) return;

        lastSource.Stop();
        Destroy(lastSource);
        lastSource = null;
    }
}
