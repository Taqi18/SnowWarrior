
using UnityEngine;
using System;

public class AudioPlayer : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioPlayer instance;


    private void Start()
    {
        instance = this;
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        s.source.Play();

    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        s.source.Stop();
       
   
    }
}

