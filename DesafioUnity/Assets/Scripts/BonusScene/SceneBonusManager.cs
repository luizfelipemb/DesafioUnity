using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBonusManager : MonoBehaviour
{
    [Header("To be assigned")]
    [SerializeField] private List<Square> squareList;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Transform squareSpawnerPos;
    [SerializeField] private Transform redBegin;
    [SerializeField] private Transform greenBegin;
    [SerializeField] private Transform blueBegin;
    private int _redSquares, _greenSquares, _blueSquares;
    [Header("Settings")] 
    public int SquaresToSpawn = 64;
    public float SeparationBetweenSquares = 2;
    public float MinSpawnSquareSize = 0.5f;

    private float _squareSize;
    private float _gapBetweenSquares;
    
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

    private void SpawnSquares()
    {
        Vector3 lastSquarePos = squareSpawnerPos.position;
        for (int i = 0; i < SquaresToSpawn; i++)
        {
            var square = Instantiate(squarePrefab, lastSquarePos,transform.rotation);
            square.transform.localScale = new Vector3(_squareSize,_squareSize,_squareSize);
            if(i!=0)
                square.transform.position += new Vector3(_gapBetweenSquares, 0, 0);
            lastSquarePos = square.transform.position;
        }
    }
    private float CalculateSquareSize(int nmbOfSquares)
    {
        float size = Mathf.Ceil((float)nmbOfSquares / 7);
        size = 2 / size;
        size = Mathf.Max(size, MinSpawnSquareSize);
        return size;
    }
    void Start()
    {
        _gapBetweenSquares = (float) 10 / (float)SquaresToSpawn;
        _squareSize = CalculateSquareSize(SquaresToSpawn);
        SpawnSquares();
        StartCoroutine(StartSorting());
    }

}
