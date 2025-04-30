using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //AudioSource
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopingSfxSource;

    //Player SFX
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip collectAbility;
    [SerializeField] private AudioClip useAbility;

    //In Game SFX
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    [SerializeField] private AudioClip gravityChange;

    //Other SFX
    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip chooseLevel;

    private bool isEnd;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        isEnd = false;
        if (bgmSource != null)
        {
            bgmSource.Play();
        }
    }

    public void StopSFX(AudioClip clip)
    {
        isEnd = true;

        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
        if (loopingSfxSource.isPlaying)
        {
            loopingSfxSource.Stop();
        }

        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isEnd) return;
        sfxSource.PlayOneShot(clip);
    }

    public void StartLoopingSFX(AudioClip clip)
    {
        if (isEnd) return;
        if (loopingSfxSource.isPlaying) return;

        loopingSfxSource.clip = clip;
        loopingSfxSource.loop = true;
        loopingSfxSource.Play();
    }

    public void StopLoopingSFX(AudioClip clip)
    {
        if (loopingSfxSource.clip == clip)
        {
            loopingSfxSource.Stop();
            loopingSfxSource.clip = null;
        }
    }

    //Player SFX
    public void PlayJumpSFX()
    {
        PlaySFX(jump);
    }

    public void PlayRunSFX()
    {
        StartLoopingSFX(run);
    }

    public void StopRunSFX()
    {
        StopLoopingSFX(run);
    }

    public void PlayShootSFX()
    {
        StartLoopingSFX(shoot);
    }

    public void StopShootSFX()
    {
        StopLoopingSFX(shoot);
    }

    public void PlayHitSFX()
    {
        PlaySFX(hit);
    }

    public void PlayCollectAbilitySFX()
    {
        PlaySFX(collectAbility);
    }

    public void PlayUseAbilitySFX()
    {
        PlaySFX(useAbility);
    }

    //In Game SFX
    public void PlayWinSFX()
    {
        StopSFX(win);
    }
    public void PlayLoseSFX()
    {
        StopSFX(lose);
    }
    public void PlayGravityChangeSFX()
    {
        PlaySFX(gravityChange);
    }

    //Other SFX
    public void PlayPauseSFX()
    {
        PlaySFX(pause);
    }

    public void PlayChooseLevelSFX()
    {
        PlaySFX(chooseLevel);
    }
}
