using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _opponent;

    private Transform _currentTarget;

    private Vector3 _offset;

    private void Start()
    {
        _currentTarget = _player;
        _offset = transform.position - _player.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _currentTarget.position + _offset, Time.deltaTime * 2);
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
