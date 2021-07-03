using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    
    public void EndGame()
    {
        asteroidSpawner.enabled = false;
        gameOverText.text = $"Your Score: {scoreSystem.EndTimer()}";
        gameOverDisplay.gameObject.SetActive(true);
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}