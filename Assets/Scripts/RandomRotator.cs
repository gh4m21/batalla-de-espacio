using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
	//Para establecer la máxima caída
	public float tumble;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		//Velocidad angular o de rotación = valor alaatorio en una esfera de radio 1 * la caida
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
