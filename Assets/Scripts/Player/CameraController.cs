using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    [SerializeField] private float[] _lines = new float[3];

    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (_player._isLive)
        {
            Vector3 _targetPosition = _target.position;

            _targetPosition += _offset;
            _targetPosition.x = _lines[_player.CurrentLine];
            _targetPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
        }
    }
}
