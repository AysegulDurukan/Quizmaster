using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompletequestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    [SerializeField] quiz quiz;

    public bool isAnsweringquestion = false;
    public float fillFraction;
    public bool loadNextquestion;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    public void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        //  if(isAnsweringquestion) ile if(isAnsweringquestion == true) ayn� anlama geliyor
        if (isAnsweringquestion)  // eger soru cevaplanabiliyorsa kullan�c� taraf�ndan cevaplan�labilirken yani
        {
            if(timerValue > 0)  
            {
                fillFraction = timerValue / timeToCompletequestion;
            } 
            else
            { // zaman dolduysa art�k cevaplanamaz ve dogru cevap gosterilir.
                isAnsweringquestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        } 
        else   // cevaplanam�yorsa  ve eger zaman dolmu�sa di�er soruya ge�ilmi�tir ve tekrar cevaplan�labilinirdir ve s�re tekrar say�lmaya ba�lanm��t�r.
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringquestion = true;
                loadNextquestion = true;
                timerValue = timeToCompletequestion;
            }
        }
    }
}
