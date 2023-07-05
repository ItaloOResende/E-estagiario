using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pegarObj : MonoBehaviour
{
    public GameObject jogador, Mao, imagemInteragir;
    public bool seguraObj;
    public int x, y;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        Mao = GameObject.FindGameObjectWithTag("mao");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objDesabilitado = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject objeto in objDesabilitado)
        {
            if (objeto.CompareTag("pegarObj"))
            {
                imagemInteragir = objeto;
                break;
            }
        }
        soltarObjeto();
        if (seguraObj == true)
        {
            transform.position = Mao.transform.position + new Vector3(x * 20 * Time.deltaTime, y * Time.deltaTime, 0);
        }
    }

    //Função que verifica se o mouse está no objeto e mostra as imagens se o jogador estiver perto dele
    void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, jogador.transform.position) < 3)
        {
            imagemInteragir.SetActive(true);
        }
    }

     //Função que verifica se o mouse n está mais no objeto
     void OnMouseExit()
     {
        imagemInteragir.SetActive(false);
     }

    //Função pra pegar e soltar o objeto
    void soltarObjeto()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (seguraObj == false)
            {
                if ((Vector3.Distance(transform.position, jogador.transform.position) <= 3) && (imagemInteragir == true))
                {
                    seguraObj = true;
                    imagemInteragir.SetActive(false);
                }
            }
            else
            {
                seguraObj = false;
            }
        }
    }
}