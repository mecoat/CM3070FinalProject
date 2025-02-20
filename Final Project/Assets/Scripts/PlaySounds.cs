using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    //https://freesound.org/people/maxgreat/sounds/421958/ Accecssed 20/2/25
    //game's loop by maxgreat -- https://freesound.org/s/421958/ -- License: Creative Commons 0
    public AudioClip gameSound;
    
    //https://freesound.org/people/plasterbrain/sounds/397353/ Accessed 20/2/25
    //Tada Fanfare G by plasterbrain -- https://freesound.org/s/397353/ -- License: Creative Commons 0
    public AudioClip matchSound;

    //https://freesound.org/people/BloodPixelHero/sounds/572936/ Accessed 20/2/25
    //Error by BloodPixelHero -- https://freesound.org/s/572936/ -- License: Creative Commons 0
    public AudioClip loseSound;

    AudioSource sound;

    public static PlaySounds SoundInstance;


    private void Awake()
    {
        if (SoundInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        SoundInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMatchSound()
    {
        sound.PlayOneShot(matchSound);
    }

    public void playLoseSound()
    {
        sound.PlayOneShot(loseSound);
    }

    //private void playGameSound()
    //{
      //  sound.PlayOneShot(gameSound);
    //}
}
