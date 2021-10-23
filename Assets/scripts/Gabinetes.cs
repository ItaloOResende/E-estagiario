using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gabinetes : MonoBehaviour
{
    //Variaveis de outros scripts
    public int qtdDispositivos;
    public List<GameObject> dispositivos;
    public bool seguraObj;
    public List<bool> seguraDispositivo;
    public List<GameObject> HDs, RAMs;
    public int qtdHds, qtdRAMs;
    //
    public int qtd;
    //variaveis a mais
    public GameObject Jogador;
    //Balaozin
    public GameObject BalaozinCriado, BalaozinHD_RAM, BalaozinHD, BalaozinRAM;
    public Transform apareceBalao;
    public int qtdEspecifica, HDNoGabinete, RAMNoGabinete;
    public bool ExpawnaBalao;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        qtdDispositivos = PersonagemOffline.qtdDispositivosPub;
        dispositivos = PersonagemOffline.dispositivosPub;
        seguraObj = PersonagemOffline.seguraObjPub;
        seguraDispositivo = PersonagemOffline.seguraDispositivoPub;
        qtdHds = PersonagemOffline.qtdHdsPub;
        HDs = PersonagemOffline.HDsPub;
        qtdRAMs = PersonagemOffline.qtdRAMsPub;
        RAMs = PersonagemOffline.RAMsPub;
        ApareceBalaozin();
        MontaComputador();
    }
    void ApareceBalaozin()
    {
        //balaozin de HD e RAM
        if ((Vector2.Distance(transform.position, Jogador.transform.position) < 2) && (ExpawnaBalao == false) &&
            (HDNoGabinete < 1) && (RAMNoGabinete < 1))
        {
            BalaozinCriado = Instantiate(BalaozinHD_RAM, new Vector2(apareceBalao.position.x, apareceBalao.position.y), apareceBalao.rotation);
            ExpawnaBalao = true;
        }
        //transição do balaozin HD e RAM pro balaozin de HD ou pro balaozin de RAM
        if ((Vector2.Distance(transform.position, Jogador.transform.position) < 2) && (ExpawnaBalao == true) &&
            (HDNoGabinete >= 1) || (RAMNoGabinete >= 1))
        {
            Destroy(BalaozinCriado);
            ExpawnaBalao = false;
        }
        //balaozin de HD
        if ((Vector2.Distance(transform.position, Jogador.transform.position) < 2) && (ExpawnaBalao == false) &&
            (HDNoGabinete < 1) && (RAMNoGabinete >= 1))
        {
            BalaozinCriado = Instantiate(BalaozinHD, new Vector2(apareceBalao.position.x, apareceBalao.position.y), apareceBalao.rotation);
            ExpawnaBalao = true;
        }
        //balaozin de RAM
        if ((Vector2.Distance(transform.position, Jogador.transform.position) < 2) && (ExpawnaBalao == false) &&
            (HDNoGabinete >= 1) && (RAMNoGabinete < 1))
        {
            BalaozinCriado = Instantiate(BalaozinRAM, new Vector2(apareceBalao.position.x, apareceBalao.position.y), apareceBalao.rotation);
            ExpawnaBalao = true;
        }
        //nenhum balaozin
        if ((Vector2.Distance(transform.position, Jogador.transform.position) >= 2) || ((HDNoGabinete >= 1) && (RAMNoGabinete >= 1)))
        {
            Destroy(BalaozinCriado);
            ExpawnaBalao = false;
        }
    }
    void MontaComputador()
    {
        for (qtd = 0; qtd < qtdDispositivos; qtd++)
        {
            for (qtdEspecifica = 0; qtdEspecifica < qtdHds; qtdEspecifica++)
            {
                if ((Vector2.Distance(transform.position, HDs[qtdEspecifica].transform.position) < 1.5) &&
                    (Input.GetKeyDown("space")) && (seguraObj == true) && (HDNoGabinete < 1) && (seguraDispositivo[qtd] == true))
                {
                    Jogador.GetComponent<PersonagemOffline>().seguraObj = false;
                    Jogador.GetComponent<PersonagemOffline>().seguraDispositivo[qtd] = false;
                    HDNoGabinete++;
                }
            }
            for (qtdEspecifica = 0; qtdEspecifica < qtdRAMs; qtdEspecifica++)
            {
                if ((Vector2.Distance(transform.position, RAMs[qtdEspecifica].transform.position) < 1.5) &&
                    (Input.GetKeyDown("space")) && (seguraObj == true) && (RAMNoGabinete < 1) && (seguraDispositivo[qtd] == true))
                {
                    Jogador.GetComponent<PersonagemOffline>().seguraObj = false;
                    Jogador.GetComponent<PersonagemOffline>().seguraDispositivo[qtd] = false;
                    RAMNoGabinete++;
                }
            }
        }
    }
}