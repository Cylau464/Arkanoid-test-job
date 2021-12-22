using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _menuBtn;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private AudioClip[] _clickClips;

    private void Start()
    {
        _continueBtn.onClick.AddListener(Continue);
        _restartBtn.onClick.AddListener(Restart);
        _menuBtn.onClick.AddListener(Menu);

        _scoreText.text = "Score: " + LevelProgressHandler.Instance.LevelScore.ToString();
    }

    private void Continue()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        Application.OpenURL("https://www.youtube.com/watch?v=fdyuOiciwB4");
        GameController.Instance.RestoreHealth(1);
        gameObject.SetActive(false);
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