/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script controla el funcionamiento de los botones incluyendo las ayudas, controla el funcionamiento del
/// juego escogiendo la palabra respectiva del nivel y la imagen.
/// 
///----------- ENGLISH -----------
/// 
/// This script controls the buttons, including the helps. Controls the functioning of
/// the game choosing the corresponding word and respective image.
/// 
/// </summary>

using UnityEngine;
using System.Collections;
public class Game_Controller : MonoBehaviour 
{

	public string word = "Don't change";
	string playerAnswer = ""; 
	string[] separedWord;
	Vector2 position = new Vector2(0, 0);
	public GameObject buttonPrefab;
	public GameObject buttonAnswerPrefab;
	string[] allLetters = new string[16];
	string[] alphabet = new string[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
	int Xposition;
	bool slashFounded = false;
	int positionSlash = 0;
	GameObject[] instanciedButtons;
	GameObject[] instanciedAnswerFields;
	bool win = false;
	GameObject buttonNext;
	int lettersPressed = 0;
	bool lookingPicture = false;
	string[] playerAnswerArray;
	string buttonClicked;
	public GameObject AreYouSureWindow;
	Word_Database wordDB;

	GameObject coinsPlayerGame;
	GameObject coinsPlayerMenu;

	bool addCoinsLog = false;
	bool quitCoinsLog = false;
	float timerAddCoinsLog = 0f;
	float timerQuitCoinsLog = 0f;

	GameObject addCoinsLogObj;
	GameObject quitCoinsLogObj;

	GameObject cameraObj;

	//CONVERT SPRITE TO TEXTURE FUNCTION
	public Texture2D textureFromSprite(Sprite sprite)
	{
		if(sprite == null) 
		{
			Debug.LogError ("WORD WITHOUT IMAGE: Word #"+PlayerPrefs.GetInt ("levelWord"));
		}


		if(sprite.rect.width != sprite.texture.width) 
		{
			Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
			Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
			                                             (int)sprite.textureRect.y, 
			                                             (int)sprite.textureRect.width, 
			                                             (int)sprite.textureRect.height );
			newText.SetPixels(newColors);
			newText.Apply();
			return newText;
			
		} else
			return sprite.texture;
		
	}

	public void Start () 
	{

		findObjectsOfVariables ();

		cameraObj.GetComponent<Animation>().Play("startScene");

		setImageAndWord ();

		if (word.Length > 16) 
		{

			allLetters = new string[24];

		}

		separedWord = new string[word.Length];

		if (word.Length <= 16) 
		{
			instanciedButtons = new GameObject[16];
		}
		else 
		{
			instanciedButtons = new GameObject[24];
		}

		instanciedAnswerFields = new GameObject[word.Length];

		if (word.Length > 23) 
		{

			Debug.LogError ("ERROR IN WORD: #"+(PlayerPrefs.GetInt ("levelWord")+1)+", THE WORD CAN'T CONTAIN MORE THAN 22 LETTERS");

		} 
		else 
		{

			SepareWord ();
			veryLongWordCheck ();
			AnswerFields ();
		}

		RefreshBoardCoins();


	}

	void findObjectsOfVariables () 
	{

		cameraObj = GameObject.Find("Main Camera").gameObject;
		addCoinsLogObj = GameObject.Find ("GAME").transform.FindChild("addCoinsLog").gameObject;
		quitCoinsLogObj = GameObject.Find ("GAME").transform.FindChild("quitCoinsLog").gameObject;
		coinsPlayerGame = GameObject.Find ("GAME").transform.FindChild("CoinsInGame").transform.FindChild("TotalCoinsPlayer").gameObject;
		coinsPlayerMenu = GameObject.Find ("MENU").transform.FindChild("CoinsInGame-Menu").transform.FindChild("TotalCoinsPlayerMenu").gameObject;
		buttonNext = GameObject.Find("ButtonNext").gameObject;
		wordDB = GameObject.Find ("Words_Database").GetComponent<Word_Database>();

	}

	void setImageAndWord ()
	{

		if (PlayerPrefs.HasKey ("levelWord")) 
		{
			
			if (PlayerPrefs.GetInt("numberLanguae") == 0) 
			{
				
				word = wordDB.words_List[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 1)
			{
				
				word = wordDB.language1[PlayerPrefs.GetInt ("levelWord")];
				
			} 
			else if (PlayerPrefs.GetInt("numberLanguae") == 2) 
			{
				
				word = wordDB.language2[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 3) 
			{
				
				word = wordDB.language3[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 4)
			{
				
				word = wordDB.language4[PlayerPrefs.GetInt ("levelWord")];
				
			} 
			else if (PlayerPrefs.GetInt("numberLanguae") == 5)
			{
				
				word = wordDB.language5[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 6)
			{
				
				word = wordDB.language6[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 7) 
			{
				
				word = wordDB.language7[PlayerPrefs.GetInt ("levelWord")];
				
			} 
			else if (PlayerPrefs.GetInt("numberLanguae") == 8) 
			{
				
				word = wordDB.language8[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 9) 
			{
				
				word = wordDB.language9[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 10)
			{
				
				word = wordDB.language10[PlayerPrefs.GetInt ("levelWord")];
				
			}
			else if (PlayerPrefs.GetInt("numberLanguae") == 11)
			{
				
				word = wordDB.language11[PlayerPrefs.GetInt ("levelWord")];
				
			} 
			else if (PlayerPrefs.GetInt("numberLanguae") == 12)
			{
				
				word = wordDB.language12[PlayerPrefs.GetInt ("levelWord")];
				
			} 
			else if (PlayerPrefs.GetInt("numberLanguae") == 13) 
			{
				
				word = wordDB.language13[PlayerPrefs.GetInt ("levelWord")];
				
			}
			
			
			
			
		}
		else 
		{
			
			PlayerPrefs.SetInt ("levelWord", 0);
			word = wordDB.words_List[0];
			
		}
		
		
		
		// PUT THE IMAGE OF THE LEVEL
		GameObject.Find("Image").GetComponent<Renderer>().material.mainTexture = textureFromSprite(wordDB.image[PlayerPrefs.GetInt ("levelWord")]);


	}



	void Update ()
	{

		ClicDetections ();

		ShowCoinsLogs();

	}


	//This function detect the click on the buttons and make the corresponding functions
	void ClicDetections () 
	{

		if (Input.GetMouseButtonUp (0)) 
		{
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			bool fieldFounded = false;
			
			if (hit.collider != null)
			{

				GameObject objectHit = hit.collider.gameObject;
				
				if(objectHit.name.Contains("buttonLeter")) 
				{
					if (!win)
					{
						for (int i = 0; i < word.Length; i++) 
						{
							if (!fieldFounded)
							{
								
								if (separedWord[i] != "/" && separedWord[i] != " ")
								{
									if(instanciedAnswerFields[i].transform.FindChild ("Letter").GetComponent<TextMesh>().text == "") 
									{
										instanciedAnswerFields[i].transform.FindChild ("Letter").GetComponent<TextMesh>().text = objectHit.gameObject.transform.FindChild("Letter").GetComponent<TextMesh>().text;
										objectHit.GetComponent<Collider2D>().enabled = false;
										objectHit.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.4f, 1f);
										fieldFounded = true;
										lettersPressed += 1;
									}
								}
								
							}
						}
						
						ChecksWinOrLose();
					}

				}
				else if (objectHit.name.Contains("fieldAnswer")) 
				{
					if (!win)
					{
						for (int h = 0; h < instanciedButtons.Length; h++) 
						{
							
							if (!instanciedButtons[h].GetComponent<Collider2D>().enabled && instanciedButtons[h].transform.FindChild("Letter").GetComponent<TextMesh>().text == objectHit.transform.FindChild ("Letter").GetComponent<TextMesh>().text)
							{
								
								objectHit.transform.FindChild ("Letter").GetComponent<TextMesh>().text = "";
								instanciedButtons[h].GetComponent<Collider2D>().enabled = true;
								instanciedButtons[h].GetComponent<SpriteRenderer>().color = new Color(0.7867647f, 0.7867647f, 0.7867647f, 1f);
								lettersPressed -= 1;
							}
						}
					}
				} 
				else if(objectHit.name == "ButtonNext")
				{
					
					if (GameObject.Find("LevelIndicator").transform.FindChild("Level").GetComponent<TextMesh>().text == wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 0] +" "+(wordDB.words_List.Length).ToString()) 
					{
						GameObject window = (Instantiate(AreYouSureWindow.gameObject, new Vector3 (AreYouSureWindow.transform.position.x, AreYouSureWindow.transform.position.y, AreYouSureWindow.transform.position.z), transform.rotation)) as GameObject;
						window.transform.FindChild("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 8];
						window.transform.FindChild("TextInfo").GetComponent<TextMesh>().fontSize = 53;
						buttonClicked = objectHit.name;
					} 
					else 
					{
						cameraObj.GetComponent<Animation>().Play("nextQuiz");
					}
					
				}
				else if(objectHit.name == "Image")
				{
					
					if (!lookingPicture)
					{
						objectHit.GetComponent<Animation>().Play("lookPicture");
						lookingPicture = true;
					} 
					else
					{
						objectHit.GetComponent<Animation>().Play("closePicture");
						lookingPicture = false;
					}
					
				} 
				else if(objectHit.name == "HelpSolveWord") 
				{
					
					if (!win)
					{
						
						GameObject window = (Instantiate(AreYouSureWindow.gameObject, new Vector3 (AreYouSureWindow.transform.position.x, AreYouSureWindow.transform.position.y, AreYouSureWindow.transform.position.z), transform.rotation)) as GameObject;


						if (PlayerPrefs.GetInt ("coinsPlayer") >= wordDB.coinsToSolveWord) 
						{

							//Solve puzzle for me
							window.transform.FindChild("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 3] + "\n(-"+wordDB.coinsToSolveWord + " " + wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 5] + ")";

						}
						else 
						{

							window.transform.FindChild("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 9];

						}

						buttonClicked = objectHit.name;
						
					}
					
				} 
				else if(objectHit.name == "HelpAddLetter") 
				{
					
					if (!win)
					{
						
						GameObject window = (Instantiate(AreYouSureWindow.gameObject, new Vector3 (AreYouSureWindow.transform.position.x, AreYouSureWindow.transform.position.y, AreYouSureWindow.transform.position.z), transform.rotation)) as GameObject;
					

						if (PlayerPrefs.GetInt ("coinsPlayer") >= wordDB.coinsToShowOneLetter) 
						{

							//Show one letter
							window.transform.FindChild("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 4] + "\n(-" + wordDB.coinsToShowOneLetter + " " + wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 5] + ")";

						}
						else 
						{

							window.transform.FindChild("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 9];

						}

						buttonClicked = objectHit.name;
						
					}
					
				}
				else if(objectHit.name == "ButtonOk") 
				{
					
					
					if(buttonClicked == "HelpSolveWord") 
					{
						
						HelpSolveWord();
						Destroy(GameObject.Find("AreYouSureWindow(Clone)").gameObject);	

						if (PlayerPrefs.GetInt("sounds") == 1) 
						{
							cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
						}

					}
					else if (buttonClicked == "HelpAddLetter") 
					{
						
						HelpAddLetter();
						Destroy(GameObject.Find("AreYouSureWindow(Clone)").gameObject);	

						if (PlayerPrefs.GetInt("sounds") == 1)
						{
							cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
						}

					} 
					else if (buttonClicked == "ButtonNext") 
					{
						
						PlayerPrefs.SetInt("levelWord", 0);
						cameraObj.GetComponent<Animation>().Play("nextQuiz");
						
					}

					ChecksWinOrLose();
					
				}
				else if(objectHit.name == "ButtonNo") 
				{
					
					Destroy(GameObject.Find("AreYouSureWindow(Clone)").gameObject);	
					
				}
				else if (objectHit.name == "GoMenuButton") 
				{
					
					cameraObj.GetComponent<Camera>().orthographicSize = 0.1f;
					cameraObj.transform.position = new Vector3 (-8, 0, -10);
					Application.LoadLevel(Application.loadedLevelName);
					
				}
				
				
			}
			
		}

	}

	void ShowCoinsLogs ()
	{

		if (addCoinsLog)
		{
			
			timerAddCoinsLog += 1* Time.deltaTime;
			
			if (timerAddCoinsLog >= 2.2f)
			{
				addCoinsLogObj.SetActive(false);
				addCoinsLog = false;
				timerAddCoinsLog = 0;
			}
		}
		
		if (quitCoinsLog) 
		{
			
			timerQuitCoinsLog += 1* Time.deltaTime;
			
			if (timerQuitCoinsLog >= 2.2f)
			{
				quitCoinsLogObj.SetActive(false);
				quitCoinsLog = false;
				timerQuitCoinsLog = 0;
			}
		}

	}


	void HelpSolveWord ()
	{

		//Save the letters of the answer in an array
		if (PlayerPrefs.GetInt ("coinsPlayer") >= wordDB.coinsToSolveWord) 
		{

						for (int j = 0; j < word.Length; j++) 
						{
			
								if (separedWord [j] != " " && separedWord [j] != "/") 
								{
				
										instanciedAnswerFields [j].transform.FindChild ("Letter").GetComponent<TextMesh> ().text = separedWord [j];
									
								}
						
						}

			PlayerPrefs.SetInt ("coinsPlayer", PlayerPrefs.GetInt ("coinsPlayer") - wordDB.coinsToSolveWord);

			quitCoinsLogObj.SetActive(true);
			quitCoinsLogObj.GetComponent<TextMesh>().text = "-" + wordDB.coinsToSolveWord;
			quitCoinsLog = true;


			RefreshBoardCoins ();

		}

	}

	
	void HelpAddLetter () 
	{
	

		if (lettersPressed < word.Length)
		{
			if (PlayerPrefs.GetInt ("coinsPlayer") >= wordDB.coinsToShowOneLetter) 
			{

							int randomNumber = Random.Range (0, instanciedAnswerFields.Length);
						
	
					if (separedWord [randomNumber] != "/" && separedWord [randomNumber] != " ") 
					{
	
							if (instanciedAnswerFields [randomNumber].transform.FindChild ("Letter").GetComponent<TextMesh> ().text == "") 
							{
	
									instanciedAnswerFields [randomNumber].transform.FindChild ("Letter").GetComponent<TextMesh> ().text = separedWord [randomNumber];
									instanciedAnswerFields [randomNumber].GetComponent<Collider2D>().enabled = false;

					
									PlayerPrefs.SetInt ("coinsPlayer", PlayerPrefs.GetInt ("coinsPlayer") - wordDB.coinsToShowOneLetter);
									instanciedAnswerFields [randomNumber].GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.4f, 1f);



									bool RandomletterFoundedInButtons = false;
									for (int i = 0; i < instanciedButtons.Length; i++) 
									{

									
											if (!RandomletterFoundedInButtons && instanciedButtons[i].GetComponent<Collider2D>().enabled == true && instanciedButtons[i].transform.FindChild("Letter").GetComponent<TextMesh>().text == instanciedAnswerFields[randomNumber].transform.FindChild("Letter").GetComponent<TextMesh>().text) 
											{
											
												instanciedButtons[i].GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.4f, 1f);
												instanciedButtons[i].GetComponent<Collider2D>().enabled = false;
												RandomletterFoundedInButtons = true;
								
											}
									

									}	
							




									lettersPressed += 1;
									ChecksWinOrLose ();

									quitCoinsLogObj.SetActive(true);
									quitCoinsLogObj.GetComponent<TextMesh>().text = "-" + wordDB.coinsToShowOneLetter;
									quitCoinsLog = true;

									RefreshBoardCoins ();
	
							}
							else
							{
	
									HelpAddLetter ();
	
							}
	
	
	

	
					}
					else
					{
	
						HelpAddLetter ();
	
					}


			}
		}

	}



	void ChecksWinOrLose ()
	{

		bool lose = false;


		if (!win)
		{


		playerAnswerArray = new string[instanciedAnswerFields.Length];

		//Save the letters of the answer in an array
			for (int j = 0; j < word.Length; j++) 
			{

				if (separedWord[j] == " ")
				{
						
						playerAnswerArray[j] = " ";
						
				} 
				else if (separedWord[j] == "/") 
				{

					playerAnswerArray[j] = "/";

				}
				else
				{
					playerAnswerArray[j] = instanciedAnswerFields[j].transform.FindChild("Letter").GetComponent<TextMesh>().text;
				}
				
			}

		//-----------------------------------------	
			
			for (int y = 0; y < word.Length; y++) 
			{
				if (!win)
				{

					playerAnswer = string.Join ("", playerAnswerArray);
					

					if (playerAnswer == word)
					{

						win = true;
						GameObject.Find("GAME").transform.FindChild("BoxAnswer").GetComponent<Animation>().Play("winAnswer");

						if (PlayerPrefs.GetInt("sounds") == 1) 
						{
							cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().winQuiz);
						}

						//PUT ENABLED THE BUTTON TO NEXT WORD

						buttonNext.GetComponent<Collider2D>().enabled = true;
						buttonNext.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
						buttonNext.transform.FindChild("Letter").GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
						buttonNext.transform.FindChild("Letter").transform.FindChild("LetterShadow").GetComponent<SpriteRenderer>().color = new Vector4(0f, 0.29f, 0f, 1f);

						PlayerPrefs.SetInt ("coinsPlayer", PlayerPrefs.GetInt ("coinsPlayer") + wordDB.coinsWinedByWord);

						if (PlayerPrefs.GetInt ("levelWord") < wordDB.words_List.Length-1)
						{

							PlayerPrefs.SetInt ("levelWord", PlayerPrefs.GetInt ("levelWord")+1);

						}

						addCoinsLogObj.SetActive(true);
						addCoinsLogObj.GetComponent<TextMesh>().text = "+" + wordDB.coinsWinedByWord;
						addCoinsLog = true;

						RefreshBoardCoins();
					}
				}
				
			}

			
		}



		if (instanciedAnswerFields[word.Length - 1].transform.FindChild("Letter").GetComponent<TextMesh>().text != "" && !win && lettersPressed == word.Length) 
		{

			if (!lose) 
			{
		
				lose = true;

				GameObject.Find("BoxAnswer").GetComponent<Animation>().Play("loseAnswer");

				if (PlayerPrefs.GetInt("sounds") == 1) 
				{
					cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().errorWord);
				}

				if (GameObject.Find("BoxAnswer").GetComponent<Animation>().isPlaying)
				{
					GameObject.Find("BoxAnswer").GetComponent<Animation>().Rewind("loseAnswer");
				}
			}
			
		}		
		
	}

	public void RefreshBoardCoins () 
	{
		
		coinsPlayerGame.GetComponent<TextMesh>().text = PlayerPrefs.GetInt ("coinsPlayer").ToString();
		coinsPlayerMenu.GetComponent<TextMesh>().text = PlayerPrefs.GetInt ("coinsPlayer").ToString();
	}


	void veryLongWordCheck ()
	{

		for (int i = 0; i < word.Length; i++) 
		{
				if(separedWord[i] == "/" && slashFounded == false)
				{
					positionSlash = i;
					slashFounded = true;
					lettersPressed += 1;
				}

			if (separedWord[i] == " ") 
			{
				
				lettersPressed += 1;
				
			}
		}
		if (positionSlash >= 12) 
		{
			Debug.LogError ("ERROR IN WORD: #"+(PlayerPrefs.GetInt ("levelWord")+1)+", YOU JUST CAN ADD AN SLASH '/' BEFORE OF THE LETTER 12 OF YOUR WORD.");
		}

	}

	void SepareWord () 
	{

		//Separe word and save in the array separeWord[]
		for (int i = 0; i < word.Length; i++)
		{
			separedWord[i] = word[i].ToString();


			Xposition = i;

			//First fill with the letters of the words to the array
			CombineLetters();
		}

		//latter fill other letters 
		ArraySpaceFilling ();

		//create the buttons in the scene
		InstanciateButtons ();
	}

	void CombineLetters () 
	{

		int randomNumber;

		if (word.Length <= 16) 
		{
			randomNumber = Random.Range (0, 16);
		}
		else 
		{
			randomNumber = Random.Range (0, 24);
		}

		if (allLetters [randomNumber] == null) 
		{

			allLetters [randomNumber] = separedWord[Xposition];

		}
		else
		{

			CombineLetters ();

		}


	}

	void ArraySpaceFilling ()
	{

		if (word.Length <= 16) 
		{

			for (int f = 0; f < 16; f++) 
			{

				//if the space in the array is null or it's an "space" or it's an slash
				if (allLetters[f] == null || allLetters[f] == " " || allLetters[f] == "/")
				{
					int randomNumber = Random.Range (0, 27);
					allLetters [f] = alphabet[randomNumber];
				}

			}

		} 
		else 
		{

			for (int f = 0; f < 24; f++) 
			{
				
				//if the space in the array is null or it's an "space" or it's an slash
				if (allLetters[f] == null || allLetters[f] == " " || allLetters[f] == "/")
				{
					int randomNumber = Random.Range (0, 27);
					allLetters [f] = alphabet[randomNumber];
				}
				
			}


		}


	}
	
	void InstanciateButtons ()
	{
			

		if (word.Length <= 16)
		{

				for (int r = 0; r < 16; r++) 
				{
					
					if (r == 0)
					{
						position = new Vector2(-2.40f, -3.09f);
					}
					else if (r == 8) 
					{
						position = new Vector2(-2.40f, -4.11f);
					}
					
					instanciedButtons[r] = (Instantiate(buttonPrefab.gameObject, new Vector2 (position.x, position.y), transform.rotation)) as GameObject;
					instanciedButtons[r].name = allLetters[r] + "_buttonLeter";
					instanciedButtons[r].transform.FindChild("Letter").GetComponent<TextMesh>().text = allLetters[r].ToString();
					position = new Vector2 (position.x + 0.68f, position.y);
	
	
				}

		}
		else
		{

			for (int r = 0; r < 24; r++) 
			{
				
				if (r == 0)
				{
					position = new Vector2(-2.40f, -2.5f);
				} 
				else if (r == 8)
				{
					position = new Vector2(-2.40f, -3.45f);
				}
				else if (r == 16)
				{
					position = new Vector2(-2.40f, -4.4f);
				}
				
				instanciedButtons[r] = (Instantiate(buttonPrefab.gameObject, new Vector2 (position.x, position.y), transform.rotation)) as GameObject;
				instanciedButtons[r].name = allLetters[r] + "_buttonLeter";
				instanciedButtons[r].transform.FindChild("Letter").GetComponent<TextMesh>().text = allLetters[r].ToString();
				position = new Vector2 (position.x + 0.68f, position.y);
				
				
			}

		}

		position = new Vector2(0, 0);

	}

	void AnswerFields ()
	{

	//---------------------------------- CENTER THE FIELDS -------------------------------- 

		float positionX = 0f;
		int moreLongWord = 0;


			if (word.Length - positionSlash < positionSlash) 
			{
				moreLongWord = positionSlash;
			} 
			else 
			{
				moreLongWord = word.Length - positionSlash;
			}



		if (slashFounded)
		{

					
			if (moreLongWord == 1) 
			{
				positionX = 0f;
			} 
			else if (moreLongWord == 2) 
			{
				positionX = -0.281f;
			}
			else if (moreLongWord == 3) 
			{
				positionX = -0.56f;
			} 
			else if (moreLongWord == 4) 
			{
				positionX = -0.845f;
			} 
			else if (moreLongWord == 5) 
			{
				positionX = -1.116f;
			} 
			else if (moreLongWord == 6)
			{
				positionX = -1.409f;
			} 
			else if (moreLongWord == 7) 
			{
				positionX = -1.674f;
			} 
			else if (moreLongWord == 8) 
			{
				positionX = -1.958f;
			}
			else if (moreLongWord == 9) 
			{
				positionX = -2.24f;
			} 
			else if (moreLongWord == 10)
			{
				positionX = -2.519f;
			}
			else if (moreLongWord >= 11) 
			{
				positionX = -2.8f;
			}

			position = new Vector2 (positionX, -0.72f);

		}
		else 
		{

			moreLongWord -= 1;

			if (word.Length == 1)
			{
				positionX = 0f;
			}
			else if (word.Length == 2) 
			{
				positionX = -0.281f;
			}
			else if (word.Length == 3) 
			{
				positionX = -0.56f;
			}
			else if (word.Length == 4) 
			{
				positionX = -0.845f;
			}
			else if (word.Length == 5) 
			{
				positionX = -1.116f;
			}
			else if (word.Length == 6) 
			{
				positionX = -1.409f;
			}
			else if (word.Length == 7) 
			{
				positionX = -1.674f;
			}
			else if (word.Length == 8) 
			{
				positionX = -1.958f;
			}
			else if (word.Length == 9) 
			{
				positionX = -2.24f;
			}
			else if (word.Length == 10) 
			{
				positionX = -2.519f;
			}
			else if (word.Length >= 11) 
			{
				positionX = -2.8f;
			}

			positionX -= -0.26f;

			position = new Vector2 (positionX, -1.1f);	

			if (word.Length >= 12)
			{
				
				position = new Vector2 (positionX, -0.72f);
				
			}

		}

		//--------------- END CENTER THE FIELDS -------------------------------------------- 

		int numberSlashes = 0;

		for (int i = 0; i < word.Length; i++)
		{

			if (separedWord[i] == "/")
			{
				position = new Vector2(positionX, -1.5f);
				numberSlashes += 1;
				if (numberSlashes >= 2)
				{
					Debug.LogError ("YOU CAN'T HAVE MORE THAN 1 SLASH '/' IN THE WORD");
				}
			}

			if (i == 11 && !slashFounded)
			{
				
				position = new Vector2(positionX, -1.5f);
				
			}
		
	


			if (separedWord[i] != " " && separedWord[i] != "/")
			{

				instanciedAnswerFields[i] = (Instantiate(buttonAnswerPrefab.gameObject, new Vector2 (position.x, position.y), transform.rotation)) as GameObject;
				instanciedAnswerFields[i].name = separedWord[i]+"_fieldAnswer";
				position = new Vector2 (position.x + 0.51f, position.y);

			}
			else if (separedWord[i] != "/")
			{

				position = new Vector2 (position.x + 0.51f, position.y);

			}


		}

	

	}


}
