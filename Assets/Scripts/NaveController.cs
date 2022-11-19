using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : MonoBehaviour
{
	//Declarlo la variable de tipo RigidBody que luego asociaremos a nuestro Jugador
	private Rigidbody rb;
	
	//Declaro la variable pública velocidad para poder modificarla desde la Inspector window
	[Range(5,15)]
	public float velocidad = 10;

	//Límites del juego
	float xMin = -5.8f;
	float xMax = 5.8f; 
	float zMin = -3.7f;
	float zMax = 13.5f;

	//Declaro la variable pública giro para poder modificarla desde la Inspector window
	[Range(-1,-10)]
	public float giro = -4;

	//Declaro la variable de tipo GameObject que luego asociaremos a nuestro prefab Disparos
	public GameObject disparo;

	//Declaro la variable de tipo Transform para la posición del disparador
	public Transform disparador;

	//Declaro la variable de tipo float velocidadDisparo para la velocidad con la que puedo generar disparos
	[Range(0,1)]
	public float velocidadDisparo = 0.25f; //4 por segundo

	//Tiempo que tiene que transcurrir hasta el próximo disparo
	private float proximoDisparo;

	//Audio Source
    public AudioSource audioSource;

	void Start()
	{
		//Capturo el rigidbody del jugador al iniciar el juego
		rb = GetComponent<Rigidbody>();

		//Capturo el audio source de la nave al iniciar el juego
        audioSource = GetComponent<AudioSource>();
	}

	//Esto lo ejecuta antes de actualizar el último frame
	void Update () {


		if (Input.GetButton("Fire1") && Time.time > proximoDisparo){

			//Incremento el valor de proximo disparo
			proximoDisparo = Time.time + velocidadDisparo;

			//Instancio un nuevo disparo en esa posición y con esa rotación
			Instantiate(disparo, disparador.position, disparador.rotation);

			//Sonido de disparo
            audioSource.Play();			
		}

	}

	// Porque voy a utilizar la física para moverlo
	void FixedUpdate () {

		//Capturo el movimiento en horizontal y vertical de nuestro teclado
		float movimientoH = Input.GetAxis("Horizontal");
		float movimientoV = Input.GetAxis("Vertical");	

		//Genero el vector de movimiento asociado, teniendo en cuenta la velocidad
		Vector3 movimiento = new Vector3(movimientoH * velocidad, 0.0f, movimientoV * velocidad);

		//Aplico ese movimiento al RigidBody del jugador
		rb.AddForce(movimiento);

		//Le quito la velocity (x o z) si llega a los límites
        if (rb.position.x < xMin || rb.position.x > xMax)
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
        }
        if (rb.position.z < zMin || rb.position.z > zMax)
        {
            rb.velocity = new Vector3(rb.position.x, 0, 0);
        }
		
		//Restrinjo la posición de la nave a los límites del juego
		rb.position = new Vector3 
        (
            Mathf.Clamp (rb.position.x, xMin, xMax), 
            0.0f, 
            Mathf.Clamp (rb.position.z, zMin, zMax)
        );

		//Rotación de la nave en Z en función de la velocidad en X
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * giro);
	}
}
