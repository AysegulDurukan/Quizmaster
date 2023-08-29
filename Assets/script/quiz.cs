using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class quiz : MonoBehaviour
{
    [Header("questions")]
    questionSO currentQuestion;  //questionSO classýný çaðýrýyoruz.
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<questionSO> questions = new List<questionSO>(); 
   

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons; // 4 adet cevap butonumuz olduðu için gameobjecti array olarak tanýmladýk 
    int correctAnswerIndex;
    bool hasAnsweredEarly; // þuanda true

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image TimerImage;
    Timer timer;

    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
    }

    void Update()
    {
        TimerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextquestion)
        {
            hasAnsweredEarly = false;
            GetNextquestion() ;// süre bittiðinde ve loadnextquestion bool u true olldugunda getnext calýscak
            timer.loadNextquestion = false;                   //  ve bir sonraki soru için loadnext question false olcak 
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringquestion) // and && kosulu
        {
            DisplayAnswer(-1); // -1 vermemizin sebebi indexin 0 3 arasý sayýlar olmasý bu sayýlarý seçemeyiz. çünkü bu kod hiçbirþey seçilmediðinde çalýþacak
            SetButtonState(false);
        }
       
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false); // çevap seçildiðinde bu satýrý çagýrdýk
        timer.CancelTimer(); // süreyi sýfýrlayan fonksiyonu çagýrdýk
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();  // doðru cevap verildiðinde görüntünün deðiþmesi için
            buttonImage.sprite = correctAnswerSprite;

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
        SetButtonState(true);
        Displayquestion();
        SetDefaultButtonSprites();
        GetRandomQuestion();
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
            buttonText.text = currentQuestion.GetAnswer(i);   // yazdýgýmýz bu iki kod sadece ilk buttonun cevabýný çekmeye yarýyor diger butonlarýn
                                                       // cevaplarýný çekmek için forloop olusturduk
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














