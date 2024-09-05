using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [ Header("Velocidad y salto") ] 
        public float velMovement = 5f; //Velocidad Movimiento
        public float fuerzaJump = 7f; //Fuerza de Salto

    [ Header("RigidBody y Animator")]
        private Rigidbody2D rb; //RigiBodody de 
        private Animator animator; //Animator Animaciones del juego
   
    [ Header("Movimiento Player")]
        public float movimientoH; //Fuerza de movimento en el eje X a traves del un Input

    [ Header("Posicion del player")]  
        private Transform playerTransform; //Posicion, escala

    public bool enElSuelo = false; //Deteccion del suelo
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Debug de los componentes

        if ( rb == null)
        {
            Debug.Log("No se encontró el componente RigiBody2d en el objeto " + gameObject.name);
        }

        if ( animator == null)
        {
            Debug.Log("No se encontró el componente Animator en el objeto " + gameObject.name);
        }
    }

    
    void Update()
    {
        //Moviento Horizontal del Player
        movimientoH = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimientoH * velMovement, rb.velocity.y);
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoH));

        //Flip
        if (movimientoH > 0)
        {
            transform.localScale = new Vector3(6,6,6); //moviento hacia la derecha
        }
        else if (movimientoH < 0)
        {
             transform.localScale = new Vector3(-6,6,6); //movimiento hacia la derecha AQUI EL ESCALDO DEL PROFE FUE DE 1,1,1
        }

        //Salto 
        if(Input.GetButton("Jump") && enElSuelo)
         {
        animator.SetBool("Jump", true);
        rb.AddForce(new Vector2(0f, fuerzaJump), ForceMode2D.Impulse);
        enElSuelo = false;
         }

    }
    
     public void OnCollisionEnter2d(Collision2D collision)

     {
            if (collision.gameObject.CompareTag("Suelo"))

            {
                enElSuelo = true;
                Debug.Log("Estoy tocando el suelo");
            }
     }
}

