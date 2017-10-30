using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // LoadScene
using UnityEngine.UI;
using UnityEngine;

public class MenuOptions : MonoBehaviour {

	// Load correct scene
    public void LoadScene(string _name) {
        SceneManager.LoadScene(_name, LoadSceneMode.Single);
    }

    public void Quit() {
    	Application.Quit();
    }

}
