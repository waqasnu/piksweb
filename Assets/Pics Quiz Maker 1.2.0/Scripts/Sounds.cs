﻿/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script controla los botones de sonido del menu y guarda los sonidos del juego.
/// 
///----------- ENGLISH -----------
/// 
/// This script controls the sound buttons of the menu and save the game sounds.
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour 
{
	public AudioClip buttonClic;
	public AudioClip buyHelp;
	public AudioClip errorWord;
	public AudioClip winQuiz;
	public AudioClip backgroundSound;
	GameObject soundsButton;
	GameObject musicButton;

	void Awake () 
	{

		if (!PlayerPrefs.HasKey("sounds")) 
		{

			PlayerPrefs.SetInt("sounds", 1);

		}
		
		
		if (!PlayerPrefs.HasKey("music")) 
		{

			PlayerPrefs.SetInt("music", 1);

		}

		refreshMusic();
		refreshSounds();

	}

	void refreshMusic ()
	{

		
		soundsButton = GameObject.Find ("MENU").transform.FindChild("soundsButton").gameObject;
		musicButton = GameObject.Find ("MENU").transform.FindChild("musicButton").gameObject;

		if (PlayerPrefs.GetInt("music") == 1) 
		{
			
			GetComponent<AudioSource>().clip = backgroundSound;

			if (GetComponent<AudioSource>().enabled)
			{

				GetComponent<AudioSource>().Play();

			}


			musicButton.GetComponent<SpriteRenderer>().color = new Vector4 (musicButton.GetComponent<SpriteRenderer>().color.r, musicButton.GetComponent<SpriteRenderer>().color.g, musicButton.GetComponent<SpriteRenderer>().color.b, 1f);
			
		}
		else 
		{

			GetComponent<AudioSource>().clip = null;
			musicButton.GetComponent<SpriteRenderer>().color = new Vector4 (musicButton.GetComponent<SpriteRenderer>().color.r, musicButton.GetComponent<SpriteRenderer>().color.g, musicButton.GetComponent<SpriteRenderer>().color.b, 0.5f);

		}

	}


	void refreshSounds ()
	{

		
		soundsButton = GameObject.Find ("MENU").transform.FindChild("soundsButton").gameObject;
		musicButton = GameObject.Find ("MENU").transform.FindChild("musicButton").gameObject;

		
		if (PlayerPrefs.GetInt("sounds") == 1)
		{
			
			soundsButton.GetComponent<SpriteRenderer>().color = new Vector4 (soundsButton.GetComponent<SpriteRenderer>().color.r, soundsButton.GetComponent<SpriteRenderer>().color.g, soundsButton.GetComponent<SpriteRenderer>().color.b, 1f);
			
		} 
		else 
		{

			soundsButton.GetComponent<SpriteRenderer>().color = new Vector4 (soundsButton.GetComponent<SpriteRenderer>().color.r, soundsButton.GetComponent<SpriteRenderer>().color.g, soundsButton.GetComponent<SpriteRenderer>().color.b, 0.5f);
			
		}
		
	}


	void Update () 
	{

		ButtonClickDetectionToPlaySound ();

	}

	void ButtonClickDetectionToPlaySound () 
	{

		if (Input.GetMouseButtonUp (0)) 
		{
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			
			if (hit.collider != null) 
			{
				
				GameObject objectHit = hit.collider.gameObject;
				
				if(objectHit.name == "musicButton") 
				{
					
					if (PlayerPrefs.GetInt("music") == 1) 
					{
						
						PlayerPrefs.SetInt("music", 0);
						refreshMusic();
						
					}
					else 
					{
						
						PlayerPrefs.SetInt("music", 1);
						refreshMusic();
						
					}
					
				} 
				else if(objectHit.name == "soundsButton")
				{
					
					if (PlayerPrefs.GetInt("sounds") == 1) 
					{
						
						PlayerPrefs.SetInt("sounds", 0);
						refreshSounds();
						
					} 
					else 
					{
						
						PlayerPrefs.SetInt("sounds", 1);
						refreshSounds();
						
					}
					
				}
				
			}
			
		}


	}

}
