using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button RestartButton;
    public List<GameObject> targets;
    public GameObject titleScreen;

    public bool isGameActive;
    public bool isPaused;

    private float spawnRate = 1.0f;
    private int score;

    public int lives;

    public void startGame(int difficulty)
    {
        titleScreen.SetActive(false);
        isGameActive = true;
        isPaused = false;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;

        UpdateScore(0); 
        UpdateLives(0);
        StartCoroutine(SpawnTarget());
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "lives: " + lives;
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void GameOver()
    {
        if (lives <= 0)
        {
            gameOverText.gameObject.SetActive(true);
            RestartButton.gameObject.SetActive(true);
            isGameActive = false; 
        }
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            Time.timeScale = 1.0f;
            isPaused = false;
        }
    }
}
