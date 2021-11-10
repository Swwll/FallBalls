using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private const string GAME_SCENE = "Game";
    private const string BEST_SCORE = "BestScore";

    [SerializeField]
    private Player _player;

    private IInput _playerInput;

    public event UnityAction<int, int> Lost;

    private void Start()
    {
        _player.Died += OnDied;
        _playerInput = _player.Input;
        Unpause();
    }

    private void OnDied()
    {
        Loose();
    }

    private void Loose()
    {
        Pause();
        var bestScore = PlayerPrefs.GetInt(BEST_SCORE, 0);

        if (_player.Points > bestScore)
        {
            bestScore = _player.Points;
            PlayerPrefs.SetInt(BEST_SCORE, bestScore);
        }

        Lost?.Invoke(_player.Points, bestScore);
    }

    public void Restart()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void Pause()
    {
        _playerInput.Lock();
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        _playerInput.Unlock();
        Time.timeScale = 1;
    }
}