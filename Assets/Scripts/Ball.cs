using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask _blockLayer;
    [SerializeField] private LayerMask _platformLayer;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Collider _collider;

    [SerializeField] private AudioClip _wallHitClip;
    [SerializeField] private AudioClip _platformHitClip;

    private float _moveSpeed;
    private int _damage = 1;

    public Action<Ball> OnDestroy;

    public void Init(GameDifficultyConfig difficultyConfig)
    {
        _moveSpeed = difficultyConfig.BallSpeed;
        _collider.enabled = false;
    }

    public void Launch(Vector3 direction)
    {
        _collider.enabled = true;
        Reflection(direction);
    }

    public void Reflection(Vector3 direction)
    {
        _rigidBody.velocity = direction * _moveSpeed;
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.magnitude < _moveSpeed)
            _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _moveSpeed);
    }

    private void OnBecameInvisible()
    {
        if (transform.position.y >= 0f) return;

        OnDestroy?.Invoke(this);
        Destroy(gameObject, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer & _blockLayer) != 0)
        {
            collision.gameObject.GetComponent<Block>().GetDamage(_damage);
        }
        else if ((1 << collision.gameObject.layer & _platformLayer) != 0)
        {
            AudioController.PlayClipAtPosition(_platformHitClip, transform.position, 1f, 100f, Random.Range(.8f, 1.2f));
        }
        else
        {
            AudioController.PlayClipAtPosition(_wallHitClip, transform.position, 1f, 100f, Random.Range(.8f, 1.2f));
        }
    }
}