using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource startLevelMusic;
    public AudioSource backGroundMusic;
    public AudioSource carSound;
    public AudioSource deadMusic;
    public AudioSource catSound;
    public AudioSource winSound;

    private bool isPlayingBackGround = false;
    // Start is called before the first frame update
    void Start()
    {
        startLevelMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

        //Reproducir musica de fondo
        if (!startLevelMusic.isPlaying && !isPlayingBackGround)
        {
            backGroundMusic.Play();
            isPlayingBackGround = true;
        }   
    }

    //Reproductor de sonido de carro
    public void PlayCarSound ()
        {
            if (carSound != null)
            {
                carSound.Play();
            }
        }

    public void PlayDeadMusic ()
        {
            if (deadMusic != null)
            {
                deadMusic.Play();
            }
        }
    public void PlayWinSound ()
        {
            if (winSound != null)
            {
                winSound.Play();
            }
        }

    public void PlayCatSound ()
        {
            if (catSound != null)
            {
                catSound.Play();
            }
        }          
}
