using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string audioName;
    public AudioClip click;
    public AudioClip back;
    public AudioClip over;
    public AudioClip load;
    public AudioClip wrongAnswer;
    public AudioClip correctAnswer;
    public AudioClip sameAgain;
    [HideInInspector]
    public AudioSource source;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.volume = volume;
    }


    public void playClick()
    {
        source.clip = click;
        source.Play();
    }

    public void playShuffle()
    {
          //  source.clip = shuffle;
          //  source.Play();
    }

    public void playBack()
    {
        source.clip = back;
        source.Play();
    }

    public void playCorrect()
    {
        source.clip = correctAnswer;
        source.Play();
    }

    public void playWrong()
    {
        source.clip = wrongAnswer;
        source.Play();
    }

    public void playSameCorrectAnswer()
    {
        source.clip = sameAgain;
        source.Play();
    }

    public void playMouseOver()
    {
        source.clip = over;
        source.Play();
    }
}
