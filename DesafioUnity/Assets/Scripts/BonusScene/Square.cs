using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SquareColor{red,green,blue}
public class Square : MonoBehaviour
{
    public SquareColor color;
    private float _stoppingDistance;
    private float _speed;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _target;
    private Vector3 _initialPosition;
    private bool _haveToMove = false;
    private Vector3 _targetDirection;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        NewColor();
    }

    public void Setup(float stoppingDistance, float speed)
    {
        _stoppingDistance = stoppingDistance;
        _speed = speed;
    }
    private void NewColor()
    {
        color = (SquareColor) Random.Range(0, System.Enum.GetValues(typeof(SquareColor)).Length);
        switch(color)
        {
            case SquareColor.red:
                _spriteRenderer.color = Color.red;
                break;
            case SquareColor.green:
                _spriteRenderer.color = Color.green;
                break;
            case SquareColor.blue:
                _spriteRenderer.color = Color.blue;
                break;
        }
    }

    public void MoveTo(Vector3 target)
    {
        _haveToMove = true;
        _target = target;
        _initialPosition = transform.position;
        _targetDirection = _target - _initialPosition;
        _targetDirection = _targetDirection.normalized;
    }

    private void Update()
    {
        if (_haveToMove)
        {
            transform.position += _targetDirection * (_speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target) < _stoppingDistance || 
                Vector3.Distance(transform.position, _target) > Vector3.Distance(_target,_initialPosition))
            {
                _haveToMove = false;
                transform.position = _target;
            }
        }   
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
        
}
