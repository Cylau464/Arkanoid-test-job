using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button _nextLevelBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _menuBtn;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private AudioClip[] _clickClips;

    private void Start()
    {
        _nextLevelBtn.onClick.AddListener(Restart);
        _restartBtn.onClick.AddListener(Restart);
        _menuBtn.onClick.AddListener(Menu);

        _scoreText.text = "Score: " + LevelProgressHandler.Instance.LevelScore.ToString();
    }

    private void Restart()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Menu()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        SceneManager.LoadScene(0);
    }
}