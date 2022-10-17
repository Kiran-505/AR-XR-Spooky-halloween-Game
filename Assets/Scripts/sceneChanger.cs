using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
		
	}
	
	public void ReloadScene()
	{
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);	
	}
	
	
	public void QuitGame()
	{
		Application.Quit();
	}

	
}
