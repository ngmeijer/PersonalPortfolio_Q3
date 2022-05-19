using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> backgroundTracks;
    private AudioSource audioSource;
    private AudioClip lastClip;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(handleMusicPlaying());
    }

    private IEnumerator handleMusicPlaying()
    {
        if (backgroundTracks.Count <= 1)
        {
            Debug.Log($"Less than 2 background tracks available. Add more.");
            yield break;
        }
        audioSource.clip = randomBackgroundClip();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        StartCoroutine(handleMusicPlaying());
    }

    private AudioClip randomBackgroundClip()
    {
        int randomIndex = Random.Range(0, backgroundTracks.Count - 1);

        AudioClip clip = backgroundTracks[randomIndex];
        
        backgroundTracks.Add(lastClip);

        lastClip = clip;
        backgroundTracks.Remove(clip);
        
        return clip;
    }
}
