using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceTest : MonoBehaviour {


	[Header("Submit Button")]
	public GameObject submit;

	[Header("Questions")]
	public GameObject[] Q;

	[Header("Answer Key")]
	public Button[] answerKey;

	//will change to dynamically take answers from one exam game object instead of each question game object
	[Header("Exam")]
	public GameObject exam;

	[Header("Exam Results")]
	public GameObject examResults;

	[Header("Retake Exam Button")]
	public GameObject retakeExam;

	private Button[] allAnswers;
	private List<Button> answerList;

	private Button[] selectedAnswers;	


	// adds listeners to each answer (button) and initialize answer key to number of questions
	void Start () {
		 selectedAnswers = new Button[Q.Length];
		 answerList = new List<Button>();

		for (int i=0; i<Q.Length; i++){
			Component[] qAnswers = Q[i].GetComponentsInChildren(typeof(Button));
			for (int j=0; j<qAnswers.Length; j++){
				AddListeners(qAnswers[j].GetComponent<Button>(), i);
				answerList.Add(qAnswers[j].GetComponent<Button>());
			}
		}
		// adds listener for retake button to reset the test
		retakeExam.GetComponentInChildren(typeof(Button)).GetComponent<Button>().onClick.AddListener(() => ressetTest());
		// stores all answers in a list then converts to an array for easier indexing
		allAnswers = answerList.ToArray();

	}
	
	/* not used
	void Update () {} 
	*/

	public void AddListeners(Button answer, int qNumber) {
	    answer.onClick.AddListener(() => AnswerSelected(answer, qNumber));
	 }

	// Each button (answer) that is pressed will highlight green and be saved per question, resets any changed answers
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

		// checks if answers selected equals number of questions then prepraes the results page
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

	public void ressetTest() {
		selectedAnswers = new Button[Q.Length];
		foreach (Button answer in allAnswers){
			answer.transform.GetComponent<Text>().color = Color.white;
		}

	}


}
