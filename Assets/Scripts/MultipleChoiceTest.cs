using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceTest : MonoBehaviour {

	// [Header("Answers for Question 1")]
	// public Button[] answers1 = new Button[4];

	[Header("Submit Button")]
	public GameObject submit;

	[Header("Questions")]
	public GameObject[] Q;

	[Header("Answer Key")]
	public Button[] answerKey;

	[Header("Exam")]
	public GameObject exam;

	[Header("Exam Results")]
	public GameObject examResults;

	private Button[] allAnswers;
	private List<Button> answerList;

	private Button[] selectedAnswers;	


	// Use this for initialization
	void Start () {
		 selectedAnswers = new Button[Q.Length];
		 answerList = new List<Button>();

		// add Listeners to each Question to select when clicked
		for (int i=0; i<Q.Length; i++){
			Component[] qAnswers = Q[i].GetComponentsInChildren(typeof(Button));
			for (int j=0; j<qAnswers.Length; j++){
				AddListeners(qAnswers[j].GetComponent<Button>(), i);
				answerList.Add(qAnswers[j].GetComponent<Button>());
			}
		}

		allAnswers = answerList.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddListeners(Button answer, int qNumber) {
	    answer.onClick.AddListener(() => AnswerSelected(answer, qNumber));
	 }

	// Each button (answer) that is pressed will highlight green and be saved per question
	public void AnswerSelected(Button answer, int qNumber) {
		int range = qNumber*4;
		for (int i=range; i<range+4; i++){
			if (allAnswers[i] == answer){
				allAnswers[i].transform.GetComponent<Text>().color = Color.green;
				selectedAnswers[qNumber] = answer;
			} else {
				allAnswers[i].transform.GetComponent<Text>().color = Color.white;
			}
		}
		Debug.Log("you chose " + selectedAnswers[qNumber]);

		if (AllAnswered()){
			PrintResults();
			ActivateSubmitButton();
		}
	}

	public void ActivateSubmitButton() {
		submit.SetActive(true);
	}

	private bool AllAnswered(){
		bool answeredAll = true;
		foreach (Button selectedAnswer in selectedAnswers){
			if (!selectedAnswer){
				answeredAll = false;
			} 
		}
		return answeredAll;
	}

	private void PrintResults() {
		int correctAnswers = 0;

		for (int i=0; i<Q.Length; i++){
			if (selectedAnswers[i] == answerKey[i])
				correctAnswers++;
		}
		examResults.GetComponentInChildren(typeof(Text)).GetComponent<Text>().text = "You got " + correctAnswers + " questions correct out of " + Q.Length;
	}


}
