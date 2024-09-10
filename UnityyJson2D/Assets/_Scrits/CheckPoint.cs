using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public bool Activated = false;

    public static CheckPoint activeCheckpoint;
    
    private void OnTriggerEnter2d(Collider2D other)

    {
        if (other.CompareTag("Player") && !Activated)
        {
            Activated = true;
            //guardar la posicion del jugador


            activeCheckpoint = this;

            //guardar posicion del jugardor con playerprefs

            PlayerPrefs.SetFloat("PlayerPosX".other.transform.posicion.x);
            PlayerPrefs.SetFloat("PlayerPosY".other.transform.posicion.y); 

            //playerprefs solo guarda numeros

            //guardar loscambios en playerprefs

            PlayerPrefs.Save();

        }
    }
}
