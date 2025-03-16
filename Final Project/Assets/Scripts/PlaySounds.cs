using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    //Game sound is :
    //https://freesound.org/people/G-14/sounds/646460/ Accessed 15/3/25
    //Medieval March(mp3) by G-14 -- https://freesound.org/s/646460/ -- License: Creative Commons 0

    
    //https://freesound.org/people/plasterbrain/sounds/397353/ Accessed 20/2/25
    //Tada Fanfare G by plasterbrain -- https://freesound.org/s/397353/ -- License: Creative Commons 0
    public AudioClip matchSound;

    //https://freesound.org/people/Akrythael/sounds/334918/ Accessed 15/3/25
    //Discordant chord by Akrythael -- https://freesound.org/s/334918/ -- License: Attribution NonCommercial 3.0
    public AudioClip loseSound;

    //holder for the desired Audiosource in the scene
    private AudioSource sound;

    //create a soundInstance
    public static PlaySounds SoundInstance;

    //runs when created
    private void Awake()
    {
        //if there is a;ready a soundInstance...
        if (SoundInstance != null)
        {
            //destroy this object (prevents duplicate copies)
            Destroy(gameObject);
            //stop this function
            return;
        }

        //Assign this object to the soundInstance
        SoundInstance = this;
        //prevent this object from being destroyed when new scenes are being loaded
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the audiosource 
        sound = GetComponent<AudioSource>();
    }

    //function to play the Match sound
    public void playMatchSound()
    {
        //play once the declared sound
        sound.PlayOneShot(matchSound);
    }

    //function to play the lose sound
    public void playLoseSound()
    {
        //play once the desied sound
        sound.PlayOneShot(loseSound);
    }
}
