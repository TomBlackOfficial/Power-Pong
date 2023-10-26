using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    [SerializeField] private AudioClip hit1, hit2, hit3, score;  

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
}
