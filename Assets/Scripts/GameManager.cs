using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] asteroide; //objeto a instanciar
    public Vector3 posicion; //posición (límites) en la que instanciar
    public int numeroDeAsteroides; //número de objetos en cada ola
    public float esperaInicial;
    public float esperaEntreAsteroides;
    public float esperaEntreOlas;
    public Text textoContador; //caja de texto para el contador
    public int contador; //Entero para contar los puntos
    public Text textoMensajes; //caja de texto para el mensaje de juego terminado
	private bool gameOver;
	private bool restart;
    public GameObject menuContainer;
    public GameObject menuGameOver;
    

    void Start () {

        StartCoroutine(crearAsteroides());

        //Inicializo el contador
        textoContador.text = "Puntuación: 0";

        //Oculto el mensaje de juego terminado
        textoMensajes.enabled = false;

        menuContainer.SetActive(false);
        menuGameOver.SetActive(false);

	}

    IEnumerator crearAsteroides(){

        //Espera inicial
        yield return new WaitForSeconds(esperaInicial);

        while (true){

            for (int i = 0; i < numeroDeAsteroides; i++)
            {
                if(gameOver){
                    break;
                }
                GameObject asteroid = asteroide[Random.Range(0, asteroide.Length)];
                
                //Posición aleatoria entre los límites (positivo y negativo) que establezcamos
                Vector3 posicionAsteroide = new Vector3(Random.Range(-posicion.x, posicion.x), posicion.y, posicion.z);
                //Rotación
                Quaternion rotacionAsteroide = Quaternion.identity;
                //Instancio el asteroide
                Instantiate(asteroid, posicionAsteroide, rotacionAsteroide);
                //Espera entre asteroides
                yield return new WaitForSeconds(esperaEntreAsteroides);
            }

            if(gameOver){
                menuGameOver.SetActive(true);
                yield return new WaitForSeconds(5f);
                Time.timeScale = 0;
                break;
            }

            //Espera entre olas
            yield return new WaitForSeconds(esperaEntreOlas);

            numeroDeAsteroides+=5;
        }
           
    }

    //Actualizo el contador (desde DestruirPorContacto)
    public void actualizarContador(){

        contador += 10;
        textoContador.text = "Puntuación: " + contador;

    }

    // Update is called once per frame
    void Update()
    {
        //ir al menu en el juego con la tecla M
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(menuGameOver.activeSelf==false){
                menuContainer.SetActive(true);
                Time.timeScale = 0;
            }
        }

        // Continuar el juego con P
        if (Input.GetKeyDown(KeyCode.P))
        {
            menuContainer.SetActive(false);
            Time.timeScale = 1;
        }

        // Quitar el juego con Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        // ir al Menu Principal con N
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Menu");
        }

        // Reiniciar el juego con R
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Principal");
            Time.timeScale = 1;
        }
    }

    public void GameOver()
	{
		gameOver = true;
	}
}
