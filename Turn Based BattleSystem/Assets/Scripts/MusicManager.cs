using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
        PlayMusic();
    }

    // Update is called once per frame
    void PlayMusic()
    {
        StartCoroutine(PlayMusicOrder());
    }
    IEnumerator PlayMusicOrder()
    {
        yield return null;

            for(int i = 0; i < audioClips.Length; i++)
            {
                Debug.Log(i);             
                    audioSource.clip = audioClips[i];
                    audioSource.Play();

            if (i == 1)
            {
                audioSource.loop = true;
            }
                while(audioSource.isPlaying)
                {
                    yield return null;
                }
            }
    }
}
