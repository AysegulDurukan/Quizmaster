using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class quiz : MonoBehaviour
{
    [Header("questions")]
    questionSO currentQuestion;  //questionSO class�n� �a��r�yoruz.
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<questionSO> questions = new List<questionSO>(); 
   

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons; // 4 adet cevap butonumuz oldu�u i�in gameobjecti array olarak tan�mlad�k 
    public int correctAnswerIndex;
    bool hasAnsweredEarly; // �uanda true

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image TimerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        TimerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextquestion)
        {
            hasAnsweredEarly = false;
            GetNextquestion() ;// s�re bitti�inde ve loadnextquestion bool u true olldugunda getnext cal�scak
            timer.loadNextquestion = false;                   //  ve bir sonraki soru i�in loadnext question false olcak 
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringquestion) // and && kosulu
        {
            DisplayAnswer(-1); // -1 vermemizin sebebi indexin 0 3 aras� say�lar olmas� bu say�lar� se�emeyiz. ��nk� bu kod hi�bir�ey se�ilmedi�inde �al��acak
            SetButtonState(false);
        }
       
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false); // �evap se�ildi�inde bu sat�r� �ag�rd�k
        timer.CancelTimer(); // s�reyi s�f�rlayan fonksiyonu �ag�rd�k
        scoreText.text = scoreKeeper.GetScoreString();
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();  // do�ru cevap verildi�inde g�r�nt�n�n de�i�mesi i�in
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "sorry the correct answer was;" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }
    void GetNextquestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            Displayquestion();
            scoreKeeper.IncrementQuestionSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }

    }

    void Displayquestion()
    {

        questionText.text = currentQuestion.Getquestion();

        for (int i = 0; i < answerButtons.Length ; i++) //i<4 yerine i<answerButtons.Length de yazabilirdik
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);   // yazd�g�m�z bu iki kod sadece ilk buttonun cevab�n� �ekmeye yar�yor diger butonlar�n
                                                       // cevaplar�n� �ekmek i�in forloop olusturduk
        }
    }


    void SetButtonState(bool state )
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}














