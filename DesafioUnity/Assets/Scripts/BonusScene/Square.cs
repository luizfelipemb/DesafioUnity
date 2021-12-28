using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SquareColor{red,green,blue}
public class Square : MonoBehaviour
{
    public SquareColor color;
    public float StoppingDistance;
    public float Speed;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _target;
    private bool _haveToMove = false;
    private Vector3 _targetDirection;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        NewColor();
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
        _targetDirection = _target - transform.position;
        _targetDirection = _targetDirection.normalized;
    }

    private void Update()
    {
        if (_haveToMove)
        {
            transform.position += _targetDirection * (Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target) < StoppingDistance)
            {
                _haveToMove = false;
                transform.position = _target;
            }
        }   
    }
}
