using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _sfxMixer;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    public static AudioController Instance;

    private const string SFXVolumeParamName = "SFXVolume";
    private const string MusicVolumeParamName = "MusicVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _musicSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _musicSource.outputAudioMixerGroup = _musicMixer;
        _sfxSource.outputAudioMixerGroup = _sfxMixer;
    }

    public static AudioSource PlayClipAtPosition(AudioClip clip, Vector3 position, float volume = 1f, float minDistance = 1f, float pitch = 1f, AudioMixerGroup mixerGroup = null, Transform parent = null)
    {
        GameObject go = new GameObject("One Shot Audio");
        go.transform.position = position;
        go.transform.parent = parent;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = 0f;
        source.minDistance = minDistance;
        source.pitch = Random.Range(pitch - .2f, pitch + .2f);
        source.outputAudioMixerGroup = mixerGroup == null ? Instance._sfxMixer : mixerGroup;
        source.Play();
        Destroy(go, Mathf.Max(.1f, source.clip.length));

        return source;

    }
}