using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovimentoPlayer : NetworkBehaviour
{
    //caracteristicas do personagem
    public SpriteRenderer sprite;
    //personagem andar
    public float x, y;
    public float Velocidade;
    public float Sensibilidade;
    //personagem pegar objetos
    public GameObject GavetaDeHD, GavetaDeRam, objCriado, HD,RAM;
    public List<GameObject> dispositivos;
    public Transform Mao;
    public bool seguraObj;
    public List<bool> seguraDispositivo;
    //Bancada
    public GameObject[] bancada;
    public int qtd, qtdBancadas, qtdDispositivos;
    public bool[] bancadaOcupada;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GavetaDeHD = GameObject.FindWithTag("GavetaDeHD");
        GavetaDeRam = GameObject.FindWithTag("GavetaDeRam");
        bancada = GameObject.FindGameObjectsWithTag("bancada");
        bancadaOcupada = new bool[4];
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Gaveta();
        Bancada();
        SegurarObjeto();
    }
    void Movimento()
    {
        if (!isLocalPlayer)
        {
            //CameraObj.SetActive(false);
            return;
        }
        //rigidbody2DPersonagem.AddForce(new Vector2(0f, ForcaDePulo));
        x = Input.GetAxis("Horizontal") * Velocidade * Time.deltaTime;
        y = Input.GetAxis("Vertical") * Velocidade * Time.deltaTime;
        transform.Translate(x, y, 0);

        if ((x > 0 && sprite.flipX == true) || (x < 0 && sprite.flipX == false))
        {
            sprite.flipX = !sprite.flipX;
        }
    }
    void Gaveta()
    {
        if (Input.GetKeyDown("space") && (Vector2.Distance(transform.position, GavetaDeHD.transform.position) < 1))
        {
            if (seguraObj == false)
            {
                objCriado = Instantiate(HD, new Vector2(Mao.position.x, Mao.position.y), Mao.rotation);
                dispositivos.Add(objCriado);
                seguraDispositivo.Add(true);
                qtdDispositivos++;
                seguraObj = true;
            }
            else
            {
                for (qtd = 0; qtd < qtdDispositivos; qtd++)
                {
                    if (seguraDispositivo[qtd] == true)
                    {
                        Destroy(dispositivos[qtd]);
                        dispositivos.RemoveAt(qtd);
                        seguraDispositivo.RemoveAt(qtd);
                        qtdDispositivos--;
                        seguraObj = false;
                    }
                }
            }
        }
        if (Input.GetKeyDown("space") && (Vector2.Distance(transform.position, GavetaDeRam.transform.position) < 1))
        {
            if (seguraObj == false)
            {
                objCriado = Instantiate(RAM, new Vector2(Mao.position.x, Mao.position.y), Mao.rotation);
                dispositivos.Add(objCriado);
                seguraDispositivo.Add(true);
                qtdDispositivos++;
                seguraObj = true;
            }
            else
            {
                for (qtd = 0; qtd < qtdDispositivos; qtd++)
                {
                    if (seguraDispositivo[qtd] == true)
                    {
                        Destroy(dispositivos[qtd]);
                        dispositivos.RemoveAt(qtd);
                        seguraDispositivo.RemoveAt(qtd);
                        qtdDispositivos--;
                        seguraObj = false;
                    }
                }
            }
        }
    }
    void SegurarObjeto()
    {
        if (dispositivos.Count > 0)
        {
            for (qtd = 0; qtd < qtdDispositivos; qtd++)
            {
                if (seguraDispositivo[qtd] == true)
                {
                    dispositivos[qtd].transform.position = Mao.position + new Vector3(x * 20 * Time.deltaTime, y * Time.deltaTime, 0);
                }
            }
        }
    }
    void Bancada()
    {
        if (dispositivos.Count > 0)
        {
            for (qtdBancadas = 0; qtdBancadas < 4; qtdBancadas++)
            {
                for (qtd = 0; qtd < qtdDispositivos; qtd++)
                {
                    //Debug.Log("for true 2");
                    if ((Vector2.Distance(bancada[qtdBancadas].transform.position, dispositivos[qtd].transform.position) < 1.5) &&
                        (Input.GetKeyDown("space") && (bancadaOcupada[qtdBancadas] == false) && (seguraObj == true)))
                    {
                        Debug.Log("colocou obj na bancada");
                        bancadaOcupada[qtdBancadas] = true;
                        seguraDispositivo[qtd] = false;
                        seguraObj = false;
                    }
                    else
                    if ((Vector2.Distance(transform.position, bancada[qtdBancadas].transform.position) < 1) &&
                        (Vector2.Distance(transform.position, dispositivos[qtd].transform.position) < 1) &&  
                        (Input.GetKeyDown("space") && (bancadaOcupada[qtdBancadas] == true) && (seguraObj == false)))
                    {
                        Debug.Log("tirou obj da bancada");
                        bancadaOcupada[qtdBancadas] = false;
                        seguraDispositivo[qtd] = true;
                        seguraObj = true;
                    }  
                }

            }
            
        }
    }
}