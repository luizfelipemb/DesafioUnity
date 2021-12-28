using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBonusManager : MonoBehaviour
{
    [Header("To be assigned")]
    [SerializeField] private List<Square> squareList;
    [SerializeField] private Transform redBegin;
    [SerializeField] private Transform greenBegin;
    [SerializeField] private Transform blueBegin;
    private int _redSquares, _greenSquares, _blueSquares;
    [Header("Settings")]
    public float SeparationBetweenSquares = 2;
    
    public IEnumerator StartSorting()
    {
        foreach (var square in squareList)
        {
            Debug.Log(square.name);
            yield return new WaitForSeconds(0.5f);
            switch (square.color)
            {
                case SquareColor.red:
                    square.MoveTo(targetCalc(redBegin.position, _redSquares));
                    _redSquares++;
                    break;
                case SquareColor.green:
                    
                    square.MoveTo(targetCalc(greenBegin.position, _greenSquares));
                    _greenSquares++;
                    break;
                case SquareColor.blue:
                    
                    square.MoveTo(targetCalc(blueBegin.position, _blueSquares));
                    _blueSquares++;
                    break;
            }
        }
    }

    Vector3 targetCalc(Vector3 beginPos, int squaresAlready)
    {
        beginPos.x += squaresAlready * SeparationBetweenSquares;
        return beginPos;
    }
    
    void Start()
    {
        StartCoroutine(StartSorting());
    }

}
