using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public event System.Action OnScoreChanged;

    public int Score;

    private void Awake()
    {
        SingletonThisObject();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("GameOver");
    }
    public void GameCompleted()
    {
        Time.timeScale = 0f;
        Debug.Log("GameCompleted");
    }
    public void IncreaseScore(int value)
    {
        Score+= value;
        OnScoreChanged?.Invoke();
    }

    void SingletonThisObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

