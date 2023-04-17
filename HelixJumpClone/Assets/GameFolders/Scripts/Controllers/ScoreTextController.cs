using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text= GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        GameManager.Instance.OnScoreChanged += HandleOnScoreChanged;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= HandleOnScoreChanged;
    }
    public void HandleOnScoreChanged()
    {
        _text.SetText("SCORE: " + GameManager.Instance.Score);
    }
}
