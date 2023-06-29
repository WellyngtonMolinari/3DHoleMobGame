using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerTimer : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int timerDuration;

    private int timer;
    private bool timerIsOn;

    [Header(" Events ")]
    public static Action onTimerOver;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTimer();
        }
    }

    private void Initialize()
    {
        timer = timerDuration;
        timerText.text = FormatSeconds(timer);
    }

    public void StartTimer()
    {
        if (timerIsOn)
        {
            Debug.LogWarning("A timer is already on");
            return;
        }

        Initialize();

        timerIsOn = true;

        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (timerIsOn)
        {
            yield return new WaitForSeconds(1);

            timer--;
            timerText.text = FormatSeconds(timer);

            if (timer == 0)
            {
                timerIsOn = false;
                StopTimer();
            }

        }
    }

    public void StopTimer()
    {
        Debug.Log("Timer is over!");
        onTimerOver?.Invoke();
    }

    private string FormatSeconds(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
