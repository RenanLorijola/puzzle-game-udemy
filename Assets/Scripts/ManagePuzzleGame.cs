using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagePuzzleGame : MonoBehaviour
{
    public Image parte;
    public Image localMarcado;
    float lmLargura, lmAltura;

    float timer;

    float timerVitoria;

    bool partesEmbaralhadas;

    bool vitoria = false;

    bool tocouVitoria = false;

    int nivel = 0;

    void criarLocaisMarcados()
    {
        lmLargura = 100; lmAltura = 100;
        float numLinhas = 5; float numColunas = 5;
        float linha, coluna;
        for (int i = 0; i < 25; i++)
        {
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoDireito").transform.position;
            linha = i % 5;
            coluna = i / 5;
            Vector3 lmPosicao = new Vector3(posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
                posicaoCentro.y - lmAltura * (coluna - numColunas / 2), 
                posicaoCentro.z);
            Image lm = (Image)(Instantiate(localMarcado, lmPosicao, Quaternion.identity));
            lm.tag = "" + (i + 1);
            lm.name = "LM" + (i + 1);
            lm.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    public void criarPartes()
    {
        lmLargura = 100; lmAltura = 100;
        float numLinhas, numColunas;
        numLinhas = numColunas = 5;
        float linha, coluna;
        for (int i = 0; i < 25; i++)
        {
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;
            linha = i % 5;
            coluna = i / 5;
            Vector3 lmPosicao = new Vector3(posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
               posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
               posicaoCentro.z);
            Image lm = (Image)(Instantiate(parte, lmPosicao, Quaternion.identity));
            lm.tag = "" + (i + 1);
            lm.name = "Parte" + (i + 1);
            lm.transform.SetParent(GameObject.Find("Canvas").transform);
            Sprite[] todasSprites = Resources.LoadAll<Sprite>("puzzle" + (nivel % 2 + 1));
            Sprite s1 = todasSprites[i];
            lm.GetComponent<Image>().sprite = s1;
        }
    }

    void embaralhaPartes() {
        int[] novoArray = new int[25];
        for (int i = 0; i < 25; i++)
        {
            novoArray[i] = i;
        }
        int tmp;
        for (int t = 0; t < 25; t++)
        {
            tmp = novoArray[t];
            int r = Random.Range(t, 10);
            novoArray[t] = novoArray[r];
            novoArray[r] = tmp;
        }
        float linha, coluna, numLinhas, numColunas;
        numLinhas = numColunas = 5;
        for (int i = 0; i < 25; i++){
            linha = (novoArray[i]) % 5;
            coluna = (novoArray[i]) / 5;
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;
            var g = GameObject.Find("Parte" + (i + 1));
            Vector3 novaPosicao = new Vector3(posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
               posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
               posicaoCentro.z);
            g.transform.position = novaPosicao;
            g.GetComponent<DragAndDrop>().posicaoInicialPartes();
        }
    }

    void falaInicial() {
        GameObject.Find("totemInicio").GetComponent<tocadorInicio>().playInicio();
    }
    
    void falaPlay() {
        GameObject.Find("totemPlay").GetComponent<tocadorPlay>().playPlay();
    }

    void somVitoria() {
        GameObject.Find("totemVitoria").GetComponent<tocadorVitoria>().playVitoria();
    }

    public void verificaVitoria(){
        var tpmVitoria = true;
        for (int i = 0; i < 25; i++){
            var parte = GameObject.Find("Parte" + (i + 1));
            var lm = GameObject.Find("LM" + (i + 1));
            tpmVitoria = tpmVitoria && (parte.transform.position == lm.transform.position);
        }
        vitoria = tpmVitoria;
    }

    void proximoNivel(){
        nivel++;
        partesEmbaralhadas = false;
        vitoria = false;
        tocouVitoria = false;
        timer = 0;
        for (int i = 0; i < 25; i++){
            Destroy(GameObject.Find("Parte" + (i + 1)));
        }
        falaInicial();
        criarPartes();
    }

    public void sair()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void voltarAoMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }



    void Start()
    {
        criarLocaisMarcados();
        criarPartes();
        falaInicial();
    }

    void Update()
    {
        var TEMPO_INICIAL = 4;
        timer += Time.deltaTime;
        if(timer >= TEMPO_INICIAL && !partesEmbaralhadas){
            embaralhaPartes();
            falaPlay();
            partesEmbaralhadas = true;
        }
        if(vitoria && !tocouVitoria){
            somVitoria();
            tocouVitoria = true;
            timerVitoria = timer;
        }
        var tempoTimer = timer - TEMPO_INICIAL > 0 ? timer - TEMPO_INICIAL : 0;
        if(!vitoria){
            GameObject.Find("tempo").GetComponent<TMPro.TextMeshProUGUI>().text = "Tempo: " + (tempoTimer).ToString("F2").Replace(".", ":");
        }
        if(vitoria && timer - timerVitoria > 3){
            proximoNivel();
        }
    }
}
