using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameController _instance;
    [SerializeField] private TextMeshProUGUI _tapToStartText;
    [SerializeField] private TextMeshProUGUI _tapToRestart;
    [SerializeField] private bool _isGameOver = false;
    public static event Action OnStart;

    private void Awake()
    {
        if (_instance != null)
            return;
        _instance = this;
        Waypoint.OnFinalPoint += GameOver;
    }

    private void Update()
    {
        if (_tapToStartText.enabled == true && Input.GetMouseButtonDown(0))
        {
            _tapToStartText.enabled = false;
            OnStart();
        }
        if (_isGameOver == true && Input.GetMouseButtonDown(0))
        {
            RestartLevel();
        }
    }

    private void GameOver()
    {
        _tapToRestart.gameObject.SetActive(true);
        _isGameOver = true;
    }

    private void RestartLevel()
    {
        Waypoint.OnFinalPoint -= GameOver;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
