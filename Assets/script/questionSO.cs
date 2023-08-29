using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(menuName ="Quiz question" , fileName ="New question")] // verdiðimiz bu yeni isim sayesinde assetlerden new question adýnda yeni bir script assests seçebilecegiz
public class questionSO: ScriptableObject
{
    [TextArea(2,6) ] // text kutumuzun boyutunu ayarlamada kullanýlýr
    [SerializeField]string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];  // 4 haneli bir dizi oluþturduk ve bu unity üzerinden ayarlanabilcek
    [SerializeField] int correctAnswerIndex;

    public string Getquestion()
    {
        return question;
    }

    public string GetAnswer(int index) // getter metodunu kullandýk
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex() // getter metodu farklý kullaným
    {
        return correctAnswerIndex;
    }
 }

