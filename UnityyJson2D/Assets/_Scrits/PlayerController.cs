using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [ Header("Velocidad y salto") ] 
        public float velMovement = 5f; //Velocidad Movimiento
        public float fuerzaJumo = 7f; //Fuerza de Salto

    [ Header("RigidBody y Animator")]
        private Rigidbody2D rb; //RigiBodody de 
        private Animator animator;
   
    [ Header("Movimiento Player")]
        public float movimientoH;

    [ Header("Posicion del player")]  
    private Transform playerTransform;


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
             transform.localScale = new Vector3(-6,6,6);
        }

    }
}
