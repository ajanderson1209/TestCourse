using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceTest : MonoBehaviour {

	//will change to dynamically take answers from one exam game object instead of each question game object
	[Header("Exam")]
	public GameObject exam;

	[Header("Answer Key")]
	public Button[] answerKey;

	[Header("Exam Results")]
	public GameObject examResults;

	[Header("Submit Button")]
	public GameObject submit;

	[Header("Retake Exam Button")]
	public GameObject retakeExam;

	[Header("ScrollBar")]
	public GameObject scrollbar;

	private Button[][] allAnswers;

	private Button[] selectedAnswers;	

	private Component[] questions;
	private int numQuestions;


	// adds listeners to each answer (button) and initialize answer key to number of questions
	void Start () {
		questions = exam.GetComponentsInChildren(typeof(VerticalLayoutGroup));
		numQuestions = questions.Length;
		selectedAnswers = new Button[numQuestions];
		allAnswers = new Button[numQuestions][];

		scrollbar.GetComponent<Scrollbar>().numberOfSteps = numQuestions;
		//scrollbar.SetActive(false);

		//each question object has a vertical layout group component and each answer object has a button component
		for (int i=0; i<numQuestions; i++){
			Component[] qAnswers = questions[i].GetComponentsInChildren(typeof(Button));
			Button[] tempAnswers = new Button[qAnswers.Length];
			for (int j=0; j<qAnswers.Length; j++){
				AddListeners(qAnswers[j].GetComponent<Button>(), i);
				tempAnswers[j] = qAnswers[j].GetComponent<Button>();
			}
			allAnswers[i] = tempAnswers;
		}

		// adds listener for retake button to reset the test
		retakeExam.GetComponentInChildren(typeof(Button)).GetComponent<Button>().onClick.AddListener(() => ressetTest());
		// stores all answers in a list then converts to an array for easier indexing
		//allAnswers = answerList.ToArray();

	}
	
	/* not used
	void Update () {} 
	*/

	public void AddListeners(Button answer, int qNumber) {
	    answer.onClick.AddListener(() => AnswerSelected(answer, qNumber));
	 }

	// Each button (answer) that is pressed will highlight green and be saved per question, resets any changed answers
	// Currently only works with 4 multiple choice answers
	public void AnswerSelected(Button answer, int qNumber) {
		int range = allAnswers[qNumber].Length;

		for (int i=0; i<range; i++){
			if (allAnswers[qNumber][i] == answer){
				allAnswers[qNumber][i].transform.GetComponent<Text>().color = Color.green;
				selectedAnswers[qNumber] = answer;
			} else {
				allAnswers[qNumber][i].transform.GetComponent<Text>().color = Color.white;
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

		for (int i=0; i<numQuestions; i++){
			if (selectedAnswers[i] == answerKey[i])
				correctAnswers++;
		}
		examResults.GetComponentInChildren(typeof(Text)).GetComponent<Text>().text = "You got " + correctAnswers + " questions correct out of " + numQuestions;
	}

	public void ressetTest() {
		selectedAnswers = new Button[numQuestions];
		foreach (Button[] question in allAnswers){
			foreach(Button answer in question){
				answer.transform.GetComponent<Text>().color = Color.white;
			}
		}
	}

}
