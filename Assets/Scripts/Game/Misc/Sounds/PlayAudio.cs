using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

    public AudioClip audio;
    public AudioClip[] weaponSounds;
    public AudioClip[] jumpSounds;
    public bool playOnStart;
    public bool loop;

    private AudioSource _source;

    void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();

        if (playOnStart)
        {
            _source.clip = audio;
            _source.loop = loop;
            _source.Play();
        }
    }

    public void PlayAudioFile(bool current, AudioClip _audio = null)
    {
        if (current == false)
        {
            _source.clip = _audio;
            _source.Play();
        }
        else
        {
            _source.Play();
        }
    }

    public void PlayRandomWeaponSound()
    {
        int i = Random.Range(0, weaponSounds.Length - 1);
        PlayAudioFile(false, weaponSounds[i]);
        print("LOO");
    }

    public void PlayRandomJump()
    {
        int i = Random.Range(0, jumpSounds.Length - 1);
        PlayAudioFile(false, jumpSounds[i]);
    }
}
