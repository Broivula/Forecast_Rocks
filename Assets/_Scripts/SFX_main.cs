using UnityEngine;
using System.Collections;

public class SFX_main : MonoBehaviour
{


    public AudioClip[] sfx_collection;
    AudioSource sfxSource;

    void Awake()
    {
        sfxSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySFX(int clipID, float volume)
    {
        sfxSource.pitch = 1.0f;
        sfxSource.clip = sfx_collection[clipID];
        float randomNumber;
        randomNumber = Random.Range(-0.15f, 0.3f);
        sfxSource.pitch = sfxSource.pitch + randomNumber;
        sfxSource.PlayOneShot(sfx_collection[clipID], volume);
      
       // AudioSource.PlayClipAtPoint(sfx_collection[clipID], transform.position, 0.25f);
    }
}
