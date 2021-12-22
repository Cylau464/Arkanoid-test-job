using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockConfig _config;
    [SerializeField] private Renderer _renderer;

    private int _health;
    private int _points;
    private AudioClip _hitClip;
    private AudioClip _destroyClip;
    private ParticleSystem _destroyParticle;

    private MaterialPropertyBlock _propBlock;
    private const string CrackAmount = "_CrackAmount";

    public Action<int> OnDestroy;

    private void Awake()
    {
        _destroyParticle = Instantiate(_config.DestroyParticle, transform.position, transform.rotation);
        _destroyParticle.transform.SetParent(transform, true);

        _health = _config.Health;
        _points = _config.Points;
        _hitClip = _config.HitClip;
        _destroyClip = _config.DestroyClip;

        _propBlock = new MaterialPropertyBlock();
        _renderer.sharedMaterial = _config.Material;
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat(CrackAmount, 1f - (_health / _config.Health));
        _renderer.SetPropertyBlock(_propBlock);

        if(_health <= 0)
        {
            AudioController.PlayClipAtPosition(_destroyClip, transform.position, 1f, 100f, Random.Range(.8f, 1.2f));
            _destroyParticle.transform.parent = null;
            _destroyParticle.Play();
            SLS.Data.Game.Score.Value += _points;
            OnDestroy?.Invoke(_points);
            Destroy(gameObject);
        }
        else
        {
            AudioController.PlayClipAtPosition(_hitClip, transform.position, 1f, 100f, Random.Range(.8f, 1.2f));
        }
    }
}
