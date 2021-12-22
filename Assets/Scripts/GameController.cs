using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _playerHealth = 3;
    public int PlayerHealth { get { return _playerHealth; } }

    [Header("Ball")]
    [SerializeField] private Ball _ballPrefab;
    private List<Ball> _activeBalls;

    [Header("Platform Properties")]
    [SerializeField] private PlatformController _platformPrefab;
    [SerializeField] private Vector3 _platformStartPos;
    [SerializeField] private float _platformBorderOffset = 5f;
    private PlatformController _platform;

    [Header("Game Diffcutly Configs")]
    [SerializeField] private GameDifficultyConfig[] _difficutlyConfigs;

    private GameDifficultyConfig _difficultyConfig;
    private bool _gameIsEnd;

    public static GameController Instance;

    public Action<int> OnHealthChanged;
    public Action<bool> OnLevelEnd;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (SLS.Data.Settings.GameDifficutly.Value == GameDifficulty.None)
            _difficultyConfig = _difficutlyConfigs.FirstOrDefault(x => x.Diffuculty == GameDifficulty.Easy);
        else
            _difficultyConfig = _difficutlyConfigs.FirstOrDefault(x => x.Diffuculty == SLS.Data.Settings.GameDifficutly.Value);

        _platform = Instantiate(_platformPrefab, _platformStartPos, Quaternion.identity);
        _platform.Init(_platformStartPos, _platformBorderOffset, _difficultyConfig);
        _activeBalls = new List<Ball>();
        SpawnBall();
    }

    private Ball SpawnBall()
    {
        Ball ball = Instantiate(_ballPrefab);
        ball.Init(_difficultyConfig);
        ball.OnDestroy += OnBallDestroyed;
        _activeBalls.Add(ball);
        _platform.SpawnBall(ball);

        return ball;
    }

    private void OnBallDestroyed(Ball ball)
    {
        ball.OnDestroy -= OnBallDestroyed;
        _activeBalls.Remove(ball);

        if (_activeBalls.Count <= 0)
        {
            _playerHealth--;
            OnHealthChanged?.Invoke(_playerHealth);
        }

        if (_playerHealth <= 0)
            LevelEnd(false);
        else
            SpawnBall();
    }

    public void RestoreHealth(int value)
    {
        _playerHealth += value;

        if (_gameIsEnd == true)
            _gameIsEnd = false;

        if (_activeBalls.Count <= 0)
            Invoke(nameof(SpawnBall), 1f);
    }

    public void LevelEnd(bool victory)
    {
        if (_gameIsEnd == true)
            return;

        if(victory == true)
        {

        }
        else
        {

        }

        _gameIsEnd = true;
        OnLevelEnd?.Invoke(victory);
    }

    private void OnDestroy()
    {
        foreach (Ball ball in _activeBalls)
            ball.OnDestroy -= OnBallDestroyed;
    }
}

public enum GameDifficulty { None, Easy, Normal, Hard }