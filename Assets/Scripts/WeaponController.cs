﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	private AudioSource audioSource;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		InvokeRepeating("Fire", delay, fireRate);
	}

	void Fire()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

		audioSource.Play();
	}
}
