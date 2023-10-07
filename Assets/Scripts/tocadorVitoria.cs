using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocadorVitoria : MonoBehaviour
{
    AudioSource audioVitoria;

    public void playVitoria(){
        audioVitoria = GetComponent<AudioSource>();
        audioVitoria.Play();
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
