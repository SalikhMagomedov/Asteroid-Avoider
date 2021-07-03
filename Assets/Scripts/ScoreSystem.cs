﻿using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    private bool _shouldCount = true;
    private float _score;
    
    private void Update()
    {
        if (!_shouldCount) return;
        
        _score += Time.deltaTime * scoreMultiplier;
        scoreText.text = $"{_score:0}";
    }

    public int EndTimer()
    {
        _shouldCount = false;
        scoreText.text = string.Empty;

        return Mathf.FloorToInt(_score);
    }
}