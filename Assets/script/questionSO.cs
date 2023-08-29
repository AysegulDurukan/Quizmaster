using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(menuName ="Quiz question" , fileName ="New question")] // verdi�imiz bu yeni isim sayesinde assetlerden new question ad�nda yeni bir script assests se�ebilecegiz
public class questionSO: ScriptableObject
{
    [TextArea(2,6) ] // text kutumuzun boyutunu ayarlamada kullan�l�r
    [SerializeField]string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];  // 4 haneli bir dizi olu�turduk ve bu unity �zerinden ayarlanabilcek
    [SerializeField] int correctAnswerIndex;

    public string Getquestion()
    {
        return question;
    }

    public string GetAnswer(int index) // getter metodunu kulland�k
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex() // getter metodu farkl� kullan�m
    {
        return correctAnswerIndex;
    }
 }

