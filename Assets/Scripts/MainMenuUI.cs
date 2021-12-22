using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _exitBtn;

    [SerializeField] private GameObject _settingsUI;
    [SerializeField] private AudioClip[] _clickClips;

    private void Start()
    {
        _startBtn.onClick.AddListener(StartGame);
        _settingsBtn.onClick.AddListener(OpenSettings);
        _exitBtn.onClick.AddListener(Exit);

        _settingsUI.SetActive(false);
    }

    private void StartGame()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        SceneManager.LoadScene(1);
    }

    private void OpenSettings()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));

        _settingsUI.SetActive(true);
    }

    private void Exit()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));

        Application.Quit();
    }
}