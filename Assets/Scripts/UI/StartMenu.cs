using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
   public void OnLoadMainMenu(InputValue value)
	{
		if (value.isPressed)
			SceneManager.LoadScene("Menu");
	}
}
