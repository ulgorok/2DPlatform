using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    [SerializeField] public AudioClip stomp;
    [SerializeField] public AudioClip pistol;
    [SerializeField] public AudioClip death;
    [SerializeField] public AudioClip dash;
    [SerializeField] public AudioClip chest;
    [SerializeField] public AudioClip dagger;
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip katana;
    [SerializeField] public AudioClip heavydeath;
    [SerializeField] public AudioClip lightdeath;
    [SerializeField] public AudioClip shotgun;
    [SerializeField] public AudioClip reload;
    [SerializeField] public AudioClip shield;
    [SerializeField] public AudioClip respawn;
    [SerializeField] public AudioClip weaponswitch;
    [SerializeField] public AudioClip button;

    private void Start()
    {
        if (musicSource != null)
        {
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("musicSource atanmad�!");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("PlaySFX'e g�nderilen AudioClip null!");
            return;
        }

        if (SFXSource == null)
        {
            Debug.LogError("SFXSource atanmad�!");
            return;
        }

        SFXSource.PlayOneShot(clip);
    }
}
