using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniShuffleButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource source;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void playSound()
    {
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
