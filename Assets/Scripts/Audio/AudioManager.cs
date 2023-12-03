using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Gino { get; private set; }
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    private void Awake() {
        if(Gino != null) {
            Destroy(gameObject);
        }

        Gino = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        PlayMusic("Start");
    }

    public static void PlaySFX(string name) {
        Sound sound = Array.Find(Gino.sfxSounds, x => x.name == name);

        if(sound == null) {
            Debug.Log("SFX not Found");
        } else {
            Gino.sfxSource.clip = sound.clip;
            Gino.sfxSource.Play();
        }
    }

    public static void PlayMusic(string name) {
        Sound sound = Array.Find(Gino.musicSounds, x => x.name == name);

        if (sound == null) {
            Debug.Log("Music not Found");
        } else {
            Gino.musicSource.clip = sound.clip;
            Gino.musicSource.Play();
        }
    }
}
