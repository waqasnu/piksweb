﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class adsintegration : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("LaunchProjectile", 9.0f,61.0f);
	}

	// Update is called once per frame
	void Update () {

	}

	void LaunchProjectile () {

		Advertisement.Show ();

	}
}
