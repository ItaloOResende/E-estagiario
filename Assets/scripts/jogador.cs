using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour
{
    //Variaveis da movimentação do jogador
    CharacterController controller;
    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;
    float forwardSpeed = 6f;
    float strafeSpeed = 6f;
    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    //Variaveis para interagir com objetos
    public int qtd, qtdDispositivos, qtdDispositivosAnterior, qtdDispositivosAgora;
    public GameObject[] dispositivos;
    public List<bool> seguraDispositivo;
    public float x, y;
    public GameObject Mao;

    void Start()
    {
        qtdDispositivosAnterior = dispositivos.Length;

        controller = GetComponent<CharacterController>();
        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;
    }

    void Update()
    {
        movimentacao();
        ExpawnObjeto();
        SegurarObjeto();
    }
    //Função de movimentação do jogador
    void movimentacao()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        // force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded)
        {
            vertical = Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);
    }

    //Função que expawna objetos (ela é chamda na função OnMouseDown();)
    void ExpawnObjeto()
    {
        dispositivos = GameObject.FindGameObjectsWithTag("ObjetosSeguraveis");
        qtdDispositivosAgora = dispositivos.Length;
        soltarObjeto();
        if (qtdDispositivosAgora > qtdDispositivosAnterior)
        {
            seguraDispositivo.Add(true);
            qtdDispositivos++;
            qtdDispositivosAnterior++;
        }
    }
        
    //Função que permite segurar os objetos proximos ao personagem
    void SegurarObjeto()
    {
        if (dispositivos.Length > 0)
        {
            for (qtd = 0; qtd < qtdDispositivos; qtd++)
            {
                if (seguraDispositivo[qtd] == true)
                {
                    dispositivos[qtd].transform.position = Mao.transform.position + new Vector3(x * 20 * Time.deltaTime, y * 10 * Time.deltaTime, 1);
                }
            }
          }
    }

    //Função que faz o jogador soltar o objeto (ela é chamada na função ExpawnaObjeto)
    void soltarObjeto()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (qtd = 0; qtd < qtdDispositivos; qtd++)
            {
                seguraDispositivo[qtd] = false;
            }
        }
    }
}