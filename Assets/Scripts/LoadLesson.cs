using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLesson : MonoBehaviour {

	// Load correct scene
    public void Load(string _name) {
        Application.LoadLevel(_name);
    }

}
