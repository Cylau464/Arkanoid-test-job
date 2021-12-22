using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Button _pauseBtn;

    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _winUI;

    [SerializeField] private AudioClip[] _clickClips;

    private void Start()
    {
        SLS.Data.Game.Score.OnValueChanged += OnScoreChanged;
        GameController.Instance.OnHealthChanged += OnHealthChanged;
        GameController.Instance.OnLevelEnd += OnLevelEnd;

        _scoreText.text = SLS.Data.Game.Score.Value.ToString();
        _healthText.text = GameController.Instance.PlayerHealth.ToString();

        _pauseBtn.onClick.AddListener(Pause);
        _pauseUI.SetActive(false);
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
    }

    private void OnScoreChanged(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnHealthChanged(int health)
    {
        _healthText.text = health.ToString();
    }

    private void OnLevelEnd(bool victory)
    {
        if(victory == true)
            _winUI.SetActive(true);
        else
            _loseUI.SetActive(true);
    }

    private void Pause()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        Time.timeScale = 0f;
        _pauseUI.SetActive(true);
    }

    private void OnDestroy()
    {
        SLS.Data.Game.Score.OnValueChanged -= OnScoreChanged;
        GameController.Instance.OnHealthChanged -= OnHealthChanged;
        GameController.Instance.OnLevelEnd -= OnLevelEnd;
    }
}