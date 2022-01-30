using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurveyHandler : MonoBehaviour
{
    public TMP_Text questionSlot;
    public GameObject questionPanel;
    public SCR_ButtonManager buttonManager;

    public string[] questions;

    public int QuestionsDone = 0; // replace with Gamemanager state

    [System.Serializable]
    public struct Question
    {
        public string Title;
        public QuestionType Type;
        [Tooltip("Only required for 'Multi' question type.")]
        public string[] Choices;
    }

    public enum QuestionType
    {
        YesNo,
        Multi,
        Input
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReset()
    {
        QuestionsDone = 0;
        questionPanel.SetActive(true);

        questionSlot.text = "Waiting for question...";
    }

    public string GetQuestion() => questions[Mathf.FloorToInt(Random.value * questions.Length)];

    public void OnSurveyStart()
    {
        if (QuestionsDone >= 3)
        {
            Debug.Log("No more questions today...");
            return;
        }

        string question = GetQuestion();

        questionSlot.text = question;

        // Just yes/no questions for now.
        //switch (question.Type)
        //{
        //    case QuestionType.Multi:
        //    case QuestionType.Input:
        //        throw new System.NotImplementedException($"Question type '{question.Type}' has not been implemented, please use another question.");

        //    case QuestionType.YesNo:
        //    default:
        //        break; // no extra case needed
        //}
    }

    public void OnQuestionSubmit()
    {
        QuestionsDone++;
        buttonManager.gameManager.RewardPlayer();

        if (QuestionsDone >= 3)
        {
            questionPanel.SetActive(false);
            questionSlot.text = "Questions submitted. Come back tomorrow.";
            return;
        }

        string question = GetQuestion();
        questionSlot.text = question;
    }
}
