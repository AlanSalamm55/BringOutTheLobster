using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;


    private State state;
    private float countdownToStart = 3f;
    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 30f;
    [SerializeField] private GameInput gameInput;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        gameInput.OnPauseAction += gameInput_OnPauseAction;
        gameInput.OnInteractAction += gameInput_OnInteractAction;

    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:

                break;

            case State.CountDownToStart:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GameOver:
                // OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }
    }
    
    private void gameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state==State.WaitingToStart)
        {
             state = State.CountDownToStart;
             OnStateChanged?.Invoke(this,EventArgs.Empty);
        }
    }

    private void gameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Resume the game
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 0f; // Pause the game
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
    }
}