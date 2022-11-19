using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosController : MonoBehaviour
{
	//Declarlo la variable de tipo RigidBody que luego asociaremos a nuestro objeto
	private Rigidbody rb;

	//Declaro la variable pública velocidad para poder modificarla desde la Inspector window
	[Range(10,30)]
	public float velocidad = 20;

	void Start()
	{
		//Capturo el rigidbody del jugador al iniciar el juego
		rb = GetComponent<Rigidbody>();

		//Aplico movimiento en dirección z positiva (con su velocidad)
		rb.velocity = transform.forward * velocidad;
	}

}
