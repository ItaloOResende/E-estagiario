using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonagemOffline : MonoBehaviour
{
    //variaveis da camera
    public GameObject camera;
    public Transform checaDistancia, checaCameraEsquerda, checaCameraDireita;
    public Rigidbody2D rigidbody2DCamera;
    //caracteristicas do personagem
    public SpriteRenderer sprite;
    //personagem andar
    public float x, y;
    public float Velocidade;
    public float Sensibilidade;
    //personagem pegar objetos
    public GameObject GavetaDeHD, GavetaDeRam, objCriado, HD, RAM;
    public List<GameObject> dispositivos;
    public static List<GameObject> dispositivosPub;
    public Transform Mao;
    public bool seguraObj;
    public static bool seguraObjPub;
    public List<bool> seguraDispositivo;
    public static List<bool> seguraDispositivoPub;
    //Bancada
    public GameObject[] bancada;
    public int qtd, qtdBancadas, qtdDispositivos;
    public static int qtdDispositivosPub;
    public bool[] bancadaOcupada;
    //Balaozin
    public GameObject gabinete, BalaozinCriado, BalaozinHD_RAM, BalaozinHD, BalaozinRAM;
    public List<GameObject> HDs, RAMs;
    public static List<GameObject> HDsPub, RAMsPub;
    public Transform apareceBalao;
    public int qtdEspecifica,qtdHds, HDNoGabinete, qtdRAMs, RAMNoGabinete;
    public static int qtdHdsPub, qtdRAMsPub;
    public bool ExpawnaBalao;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2DCamera = camera.GetComponent<Rigidbody2D>();
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
        MovimentoCamera();
        Gaveta();
        Bancada();
        SegurarObjeto();
        //ApareceBalaozin();
        //MontaComputador();
        dispositivosPub = dispositivos;
        seguraDispositivoPub = seguraDispositivo;
        qtdDispositivosPub = qtdDispositivos;
        HDsPub = HDs;
        qtdHdsPub = qtdHds;
        RAMsPub = RAMs;
        qtdRAMsPub = qtdRAMs;
        //seguraObj = Gabinetes.seguraObjPub;
        seguraObjPub = seguraObj;
    }
    void Movimento()
    {
        x = Input.GetAxis("Horizontal") * Velocidade * Time.deltaTime;
        y = Input.GetAxis("Vertical") * Velocidade * Time.deltaTime;
        transform.Translate(x, y, 0);

        if ((x > 0 && sprite.flipX == true) || (x < 0 && sprite.flipX == false))
        {
            sprite.flipX = !sprite.flipX;
        }
    }
    void MovimentoCamera()
    {
        //Horizontal
        x = (Input.GetAxis("Horizontal"));
        if (((Vector2.Distance(camera.transform.position, checaCameraEsquerda.transform.position) < 15) && (x < 0))
            || ((Vector2.Distance(camera.transform.position, checaCameraDireita.transform.position) < 15) && (x > 0))
            || (x == 0))
        {
            rigidbody2DCamera.velocity = new Vector2(0, 0);
            checaDistancia.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        if (((Vector2.Distance(transform.position, checaDistancia.transform.position) < 1) && (x < 0))
            || ((Vector2.Distance(transform.position, checaDistancia.transform.position) < 1) && (x > 0)))
        {
            rigidbody2DCamera.velocity = new Vector2(x * 10, rigidbody2DCamera.velocity.y);
            checaDistancia.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 10, rigidbody2DCamera.velocity.y);
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
                HDs.Add(objCriado);
                qtdHds++;
                seguraObj = true;
            }
            else
            {
                for (qtd = 0; qtd < qtdDispositivos; qtd++)
                {
                    for (qtdEspecifica = 0; qtdEspecifica < qtdHds; qtdEspecifica++)
                    { 
                        if ((Vector2.Distance(HDs[qtdEspecifica].transform.position, GavetaDeHD.transform.position) < 2) &&
                            (seguraDispositivo[qtd] == true))
                        {
                            Destroy(dispositivos[qtd]);
                            dispositivos.RemoveAt(qtd);
                            seguraDispositivo.RemoveAt(qtd);
                            qtdDispositivos--;
                            HDs.RemoveAt(qtdEspecifica);
                            qtdHds--;
                            seguraObj = false;
                        }
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
                RAMs.Add(objCriado);
                qtdRAMs++;
                seguraObj = true;

            }
            else
            {
                for (qtd = 0; qtd < qtdDispositivos; qtd++)
                {
                    for (qtdEspecifica = 0; qtdEspecifica < qtdRAMs; qtdEspecifica++)
                    {
                        if ((Vector2.Distance(RAMs[qtdEspecifica].transform.position, GavetaDeRam.transform.position) < 2) &&
                            (seguraDispositivo[qtd] == true))
                        {
                            Destroy(dispositivos[qtd]);
                            dispositivos.RemoveAt(qtd);
                            seguraDispositivo.RemoveAt(qtd);
                            qtdDispositivos--;  
                            RAMs.RemoveAt(qtdEspecifica);
                            qtdRAMs--;
                            seguraObj = false;
                        }
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
        for (qtdBancadas = 0; qtdBancadas < 4; qtdBancadas++)
        {
            if (Vector2.Distance(bancada[qtdBancadas].transform.position, gabinete.transform.position) < 2)
            {
                bancadaOcupada[qtdBancadas] = true;
            }
            for (qtd = 0; qtd < qtdDispositivos; qtd++)
            {
                if ((Vector2.Distance(bancada[qtdBancadas].transform.position, dispositivos[qtd].transform.position) < 1.5) &&
                    (Input.GetKeyDown("space")) && (bancadaOcupada[qtdBancadas] == false) && (seguraObj == true))
                {
                    bancadaOcupada[qtdBancadas] = true;
                    seguraDispositivo[qtd] = false;
                    seguraObj = false;
                }
                else
                if ((Vector2.Distance(transform.position, bancada[qtdBancadas].transform.position) < 1) &&
                    (Vector2.Distance(transform.position, dispositivos[qtd].transform.position) < 1) &&
                    (Input.GetKeyDown("space") && (bancadaOcupada[qtdBancadas] == true) && (seguraObj == false)))
                {
                    bancadaOcupada[qtdBancadas] = false;
                    seguraDispositivo[qtd] = true;
                    seguraObj = true;
                }
            }
        }
    }
}