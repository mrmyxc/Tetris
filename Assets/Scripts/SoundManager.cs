using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;

    public AudioClip rotateSound;
    public AudioClip rowDelete;
    public AudioClip shapeMove;
    public AudioClip shapeStop;
    public AudioClip gameOver;

    private AudioSource audioSource;
    // Use this for initialization
    void Start ()
    {
		if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AudioSource source = GetComponent<AudioSource>();
        audioSource = source;
	}
	
    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
