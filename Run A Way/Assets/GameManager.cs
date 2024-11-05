using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    public enum GameState
    {
        Playing,
        Paused
    }

    public GameState CurrentGameState { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentGameState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && CurrentGameState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
        }
    }

    public void SetGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;

        switch (CurrentGameState)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                _pausePanel.SetActive(false);
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                _pausePanel.SetActive(true);
                break;
        }
    }

    public void SetGameState(int newGameState)
    {
        SetGameState((GameState)newGameState);
    }

    private void OnDisable()
    {
        Time.timeScale = 1; // Ensure the game is not paused if the object is disabled
    }

}
