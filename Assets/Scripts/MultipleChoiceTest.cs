using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceTest : MonoBehaviour {

	[Header("Answers for Question 1")]
	public Button[] answers1 = new Button[4];

	[Header("Number of Questions")]
	public int numAnswers;

	[Header("Submit Button")]
	public GameObject submit;

	private int[] selectedAnswers = new int[1];
	private bool answeredAll = false;

	private void init() {
		for (int i=0; i< numAnswers; i++){
			string curAnswers = "answers" + (i+1);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("answer " + selectedAnswers[0]);
		for (int i=0; i< answers1.Length; i++){
			Debug.Log(i);
			AddListeners(answers1[i], answers1);	
			// EditorGUILayout.PropertyField(answers1.GetArrayElementAtIndex(i)new GUIContent ("Answer "+ (i+1).ToString());
		}
		// foreach (Button answer in answers1){

		// }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddListeners(Button answer, Button[] answers) {
	    answer.onClick.AddListener(() => AnswerSelected(answer, answers));
	 }

	public void AnswerSelected(Button answer, Button[] answers) {
		 // Debug.Log("answer " + (answer));
		foreach (Button b in answers){
			if (b == answer){
				b.transform.GetComponent<Text>().color = Color.green;
			} else {
				b.transform.GetComponent<Text>().color = Color.white;
			}
		}
		selectedAnswers[0] = System.Array.IndexOf(answers, answer) + 1;
		Debug.Log("answer " + selectedAnswers[0]);
		
		//question.transform.GetComponent<Text>().color = Color.green;

		answeredAll = true;
		foreach (int selectedAnswer in selectedAnswers){
			if (selectedAnswer == 0){
				answeredAll = false;
			} 
		}

		if (answeredAll)
			ActivateSubmitButton();
	}

	public void ActivateSubmitButton() {
		submit.SetActive(true);
	}
}
