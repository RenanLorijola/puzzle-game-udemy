using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    Vector3 posicaoOriginal;

    void Start()
    {
        posicaoOriginal = gameObject.transform.position;
    }

    public void Drag()
    {
        print("Arrastando" + gameObject.name);
        gameObject.transform.position = Input.mousePosition;
    }

    public void moveBack()
    {
        gameObject.transform.position = posicaoOriginal;
    }

    public void snap(GameObject img, GameObject lm)
    {
        img.transform.position = lm.transform.position;
    }

    public void checkMatch()
    {
        GameObject img = gameObject;
        string tag = gameObject.tag;
        GameObject lm1 = GameObject.Find("LM" + tag);

        float distance = Vector3.Distance(lm1.transform.position, img.transform.position);
        print("Distancia " + distance);
        if (distance <= 50)
        {
            snap(img, lm1);
            GameObject.Find("managePuzzleGame").GetComponent<ManagePuzzleGame>().verificaVitoria();
        }
        else
        {
            moveBack();
        }
    }

    public void posicaoInicialPartes() {
        posicaoOriginal = transform.position;
    }


    public void Drop()
    {
        checkMatch();
    }
}
