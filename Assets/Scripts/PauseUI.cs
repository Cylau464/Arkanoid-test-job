using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _menuBtn;

    [SerializeField] private AudioClip[] _clickClips;

    private void Start()
    {
        _continueBtn.onClick.AddListener(ContinueGame);
        _menuBtn.onClick.AddListener(OpenMainMenu);
    }

    private void ContinueGame()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void OpenMainMenu()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}