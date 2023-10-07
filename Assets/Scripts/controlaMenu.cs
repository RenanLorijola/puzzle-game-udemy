using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlaMenu : MonoBehaviour
{
    
    public void AbrirGame(){
        SceneManager.LoadScene("Abertura");
    }

    public void AbrirCreditos(){
        SceneManager.LoadScene("Creditos");
    }

    public void sair(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
