using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int days = Mathf.FloorToInt(elapsedTime / 86400); // seconds in day
        int hours = Mathf.FloorToInt((elapsedTime % 86400) / 3600); // seconds in hour
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60); // seconds in minute
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // seconds

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", days, hours, minutes, seconds);
    }
}