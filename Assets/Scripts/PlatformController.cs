using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private LayerMask _ballLayer;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _maxBallReflectionAngle = 60f;
    [Space]
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private LineRenderer _trajectoryRenderer;
    [SerializeField] private Renderer _renderer;

    private float _borderOffset;
    private Vector3 _startPos;
    private float _width;
    private Camera _camera;

    private Ball _ball;

    private void Start()
    {
        _camera = Camera.main;
        _trajectoryRenderer.gameObject.SetActive(false);
    }

    private void Update()
    {
        bool isMoves = false;
        bool isBallLaunch = false;

#if UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            isMoves = t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary;
            isBallLaunch = t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended;  
        }
#else
        isMoves = Input.GetMouseButton(0);
        isBallLaunch = Input.GetMouseButtonUp(0);
#endif

        if (_ball != null)
        {
            Vector3 direction = CalculateBallTrajectory(transform.position.x / _borderOffset);

            if (isBallLaunch == true)
            {
                _ball.transform.parent = null;
                _ball.Launch(direction);
                _ball = null;
            }
            else if (isMoves == true)
            {
                if (_trajectoryRenderer.gameObject.activeSelf == false)
                    _trajectoryRenderer.gameObject.SetActive(true);

                _trajectoryRenderer.SetPosition(1, direction);
            }
        }
        else if (_trajectoryRenderer.gameObject.activeSelf == true)
            _trajectoryRenderer.gameObject.SetActive(false);

        if (isMoves == true)
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_camera.transform.position.z));
            mousePos.x = Mathf.Clamp(mousePos.x, -_borderOffset + _renderer.bounds.size.x / 2f, _borderOffset - _renderer.bounds.size.x / 2f);
            transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        }
    }

    private Vector3 CalculateBallTrajectory(float t)
    {
        float angle = 90f - Mathf.Lerp(0f, _maxBallReflectionAngle, Mathf.Abs(t));
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        direction.x *= Mathf.Sign(t);

        return direction;
    }

    public void Init(Vector3 startPos, float borderOffset, GameDifficultyConfig difficutlyConfig)
    {
        transform.position = _startPos = startPos;
        _borderOffset = borderOffset;
        _width = difficutlyConfig.PlatformWidth;
        transform.localScale = new Vector3(_width, transform.localScale.y, transform.localScale.z);
    }

    public void SpawnBall(Ball ball)
    {
        ball.transform.position = _ballSpawnPoint.position;
        ball.transform.SetParent(_ballSpawnPoint);
        _ball = ball;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((1 << collision.gameObject.layer & _ballLayer) != 0)
        {
            if (collision.transform.position.y < transform.position.y) return;

            float t = transform.InverseTransformPoint(collision.GetContact(0).point).x / .5f;
            collision.gameObject.GetComponent<Ball>().Reflection(CalculateBallTrajectory(t));
        }
    }
}