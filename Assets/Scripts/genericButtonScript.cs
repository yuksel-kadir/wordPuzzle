using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string audioName;
    public AudioClip click;
    [HideInInspector]
    public AudioSource source;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    void Awake()
    {
       source =  gameObject.GetComponent<AudioSource>();
       source.clip = click;
        source.volume = volume;
    }


    public void playSound()
    {
        source.Play();
    }
    // Update is called once per frame
    
}
