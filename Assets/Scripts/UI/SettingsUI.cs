using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button _backToMenuBtn;
    [SerializeField] private Button _easyBtn;
    [SerializeField] private Button _normalBtn;
    [SerializeField] private Button _hardBtn;
    [SerializeField] private TMP_Text _difficultyText;

    [SerializeField] private AudioClip[] _clickClips;

    private void Start()
    {
        _easyBtn.onClick.AddListener(() => SelectDifficulty(GameDifficulty.Easy));
        _normalBtn.onClick.AddListener(() => SelectDifficulty(GameDifficulty.Normal));
        _hardBtn.onClick.AddListener(() => SelectDifficulty(GameDifficulty.Hard));
        _backToMenuBtn.onClick.AddListener(BackToMenu);

        _difficultyText.text = SLS.Data.Settings.GameDifficutly.Value == GameDifficulty.None ? "Easy" : SLS.Data.Settings.GameDifficutly.Value.ToString();
    }

    private void SelectDifficulty(GameDifficulty difficulty)
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        SLS.Data.Settings.GameDifficutly.Value = difficulty;
        _difficultyText.text = difficulty.ToString();

    }

    private void BackToMenu()
    {
        AudioController.PlayClipAtPosition(_clickClips[Random.Range(0, _clickClips.Length)], transform.position, 1f, 1f, Random.Range(1f, 1.2f));
        gameObject.SetActive(false);
    }
}