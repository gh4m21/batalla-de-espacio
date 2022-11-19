using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
    public GameObject explosionNave;
    private GameManager gameManager;
	public int scoreValue;

	// private GameController gameController;

	void Start()
	{
		//Busco el script de GameManager
        gameManager = FindObjectOfType<GameManager>();
	}

	//Al entrar en el objeto
    private void OnTriggerEnter(Collider other)
    {

        //Para que no se destruya con los límites
        if (other.name == "Limites" || other.CompareTag("Enemy")) return;

            //Explosión del disparo en su posición y rotación
            if (explosion != null) {
			    Instantiate(explosion, transform.position, transform.rotation);
		    }

            if (other.name == "Nave")
            {
                //Explosión de la nave en su posición y rotación
                Instantiate(explosionNave, other.transform.position, other.transform.rotation);

                //Mostrar mensaje de juego terminado
                gameManager.textoMensajes.enabled = true;

                gameManager.GameOver();
                
            }

            //Destruyo el disparo (con el que ha chocado)
            Destroy(other.gameObject);
            //Destruyo el asteroide
            Destroy(gameObject);

            //Actualizo el contador
            gameManager.actualizarContador();
        
    }
}
