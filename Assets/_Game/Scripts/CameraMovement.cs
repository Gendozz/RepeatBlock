using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _opponent;

    [SerializeField] private Transform _origin;

    [SerializeField] private float _speedModifier;

    // Zoom camera due to resolution
    [SerializeField] private Vector2 _referenceResolution;

    [SerializeField] private float _zoomFactor;

    private Transform _currentTarget;

    private Camera _camera;

    private float _originalCameraSize;

    private Vector3 _originalOffset;

    private Vector3 _currentOffset;

    private float _cameraSizeModifier;

    private void Start()
    {
        _currentTarget = _origin;

        _originalOffset = transform.position - _origin.position;

        _currentOffset = _originalOffset;

        _camera = GetComponent<Camera>();

        _cameraSizeModifier = GetCameraSizeModifier();

        _originalCameraSize = _camera.orthographicSize;

        SetupCamera();
    }

    private void Update()
    {
        float newModifier = GetCameraSizeModifier();
        if (newModifier != _cameraSizeModifier)
        {
            SetupCamera();
        }
    }

    private void SetupCamera()
    {
        _cameraSizeModifier = GetCameraSizeModifier();
        _camera.orthographicSize = _originalCameraSize * _cameraSizeModifier;

        _currentOffset = new Vector3(_originalOffset.x,
                                     Mathf.Clamp(_originalOffset.y * (_cameraSizeModifier * 0.8f), 10, 20),
                                     _originalOffset.z);

        if (Screen.width > Screen.height)
        {
            _currentOffset = _originalOffset;
        }
        transform.position = _currentTarget.position + _currentOffset;
    }

    private float GetCameraSizeModifier()
    {
        if (Screen.width > Screen.height)
        {
            return 1;
        }


        float refRatio = _referenceResolution.x / _referenceResolution.y;

        float ratio = (float)Screen.width / (float)Screen.height;

        return (refRatio / ratio) * _zoomFactor;
    }

    public void StartMoving()
    {
        StartCoroutine(FollowTarget());
    }


    private IEnumerator FollowTarget()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, _currentTarget.position + _currentOffset, Time.deltaTime * _speedModifier);
            yield return null;
        }
    }

    public void SetOpponentAsTarget()
    {
        _currentTarget = _opponent;
    }

    public void SetPlayerAsTarget()
    {
        _currentTarget = _player;
    }
}
