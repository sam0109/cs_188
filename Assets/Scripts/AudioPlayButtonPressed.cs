using UnityEngine;
using System.Collections;

public class AudioPlayButtonPressed : MonoBehaviour
{
    public AudioClip sound;

	public void playAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
