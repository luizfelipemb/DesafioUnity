using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBonusManager : MonoBehaviour
{
    [SerializeField] private List<Square> squareList;
    [SerializeField] private Transform redBegin;
    [SerializeField] private Transform greenBegin;
    [SerializeField] private Transform blueBegin;
    private int _redSquares, _greenSquares, _blueSquares;
    
    public IEnumerator StartSorting()
    {
        foreach (var square in squareList)
        {
            Debug.Log(square.name);
            yield return new WaitForSeconds(0.5f);
            switch (square.color)
            {
                case SquareColor.red:
                    Vector3 target = redBegin.position;
                    target.x += _redSquares * 2;
                    square.MoveTo(target);
                    _redSquares++;
                    break;
                case SquareColor.green:
                    
                    square.MoveTo(greenBegin.position);
                    _greenSquares++;
                    break;
                case SquareColor.blue:
                    
                    square.MoveTo(blueBegin.position);
                    _blueSquares++;
                    break;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSorting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
