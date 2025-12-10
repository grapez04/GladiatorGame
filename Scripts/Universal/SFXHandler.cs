using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    
    public AudioClip slashClip;

    public void PlaySFX(AudioClip clip)
    {
        source.PlayOneShot(slashClip);
    }
}
