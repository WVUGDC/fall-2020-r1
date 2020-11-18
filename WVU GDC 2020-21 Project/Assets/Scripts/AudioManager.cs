using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    //public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        
        //makes sure there is only one instance of this gameObject
                                                               /*
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //ensures the game object persists between scenes
        DontDestroyOnLoad(gameObject);                      */

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //can use this for continual sounds, music etc.
    void Start()
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
