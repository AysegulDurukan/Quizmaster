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

        //  if(isAnsweringquestion) ile if(isAnsweringquestion == true) ayný anlama geliyor
        if (isAnsweringquestion)  // eger soru cevaplanabiliyorsa kullanýcý tarafýndan cevaplanýlabilirken yani
        {
            if(timerValue > 0)  
            {
                fillFraction = timerValue / timeToCompletequestion;
            } 
            else
            { // zaman dolduysa artýk cevaplanamaz ve dogru cevap gosterilir.
                isAnsweringquestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        } 
        else   // cevaplanamýyorsa  ve eger zaman dolmuþsa diðer soruya geçilmiþtir ve tekrar cevaplanýlabilinirdir ve süre tekrar sayýlmaya baþlanmýþtýr.
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
