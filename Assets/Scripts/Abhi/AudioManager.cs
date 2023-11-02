using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    [SerializeField] private AudioClip menu1, menu2, menu3, launch1, launch2, hit1, hit2, hit3, score;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //TODO: Play BGM
    }

    public void PlayHitPaddleSound()
    {
        audioSource.PlayOneShot(Random.value < 0.5 ? hit1 : hit2);
    }

    public void PlayScoreSound()
    {
        audioSource.PlayOneShot(score);
    }

    public void PlayHitSidesSound()
    {
        audioSource.PlayOneShot(hit3);
    }

    public void PlayMenuSound()
    {
        float rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                audioSource.PlayOneShot(menu1);
                break;
            case 1:
                audioSource.PlayOneShot(menu2);
                break;
            case 2:
                audioSource.PlayOneShot(menu3);
                break;
        }
    }

    public void PlayLaunchSound()
    {
        audioSource.PlayOneShot(Random.value < 0.5 ? launch1 : launch2);
    }
}
