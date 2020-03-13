using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instace;

    public GameObject Btn_MuteTrue;
    public GameObject Btn_MuteFalse;

    public int ismute;

    void Awake()
    {
        if (instace == null)
            instace = this;
        else 
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) 
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.GetComponent<AudioSource>();
            s.source.mute = true;
            PlayerPrefs.SetInt("ismute", 1);
            Btn_MuteTrue.SetActive(true);
            Btn_MuteFalse.SetActive(false);
        }
    }

    public void PlayPlaying(string sound)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.GetComponent<AudioSource>();
            s.source.mute = false;
            PlayerPrefs.SetInt("ismute", 0);
            Btn_MuteTrue.SetActive(false);
            Btn_MuteFalse.SetActive(true);
        }
    }
}
