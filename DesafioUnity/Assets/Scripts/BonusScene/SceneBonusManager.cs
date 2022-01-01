using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBonusManager : MonoBehaviour
{
    [Header("To be assigned")] [SerializeField]
    private GameObject squarePrefab;

    [SerializeField] private Transform squareSpawnerPos;
    [SerializeField] private Transform squareSpawnerPos2;
    [SerializeField] private Transform redBegin;
    [SerializeField] private Transform greenBegin;
    [SerializeField] private Transform blueBegin;
    private int _redSquares, _greenSquares, _blueSquares;
    [Header("Settings")] public int SquaresToSpawn = 64;
    public float MinSpawnSquareSize = 0.5f;
    public float SquareStoppingDistance = 0.1f;
    public float SquareSpeed = 10;

    private float _squareSize;
    private int _squaresThatFillARow = Int32.MaxValue;
    private Camera _camera;
    private List<Square> _squareList, _squareList2;
    private bool _endOfFirstSorting;

    void Start()
    {
        _squareList = new List<Square>();
        _squareList2 = new List<Square>();
        _camera = Camera.main;
        _squareSize = CalculateSquareSize(SquaresToSpawn);
        _squareList = SpawnSquares(squareSpawnerPos.position);
        _squareList2 = SpawnSquares(squareSpawnerPos2.position);
    }

    public void StartSorting()
    {
        StartCoroutine(SortByColor(_squareList));
    }

    private List<Square> SpawnSquares(Vector3 initialPos)
    {
        List<Square> returnList = new List<Square>();
        int squareRow = 1;
        Vector3 lastSquarePos = initialPos;
        for (int i = 0; i < SquaresToSpawn; i++)
        {
            var square = Instantiate(squarePrefab, lastSquarePos, transform.rotation);
            square.GetComponent<Square>().Setup(SquareStoppingDistance, SquareSpeed);
            square.transform.localScale = new Vector3(_squareSize, _squareSize, _squareSize);
            if (i != 0)
                square.transform.position += new Vector3(_squareSize, 0, 0);

            //check if can be seen
            Vector3 viewPos = _camera.WorldToViewportPoint(square.transform.position);
            if (viewPos.x > 1f || viewPos.x < 0)
            {
                _squaresThatFillARow = Mathf.Min(i, _squaresThatFillARow);
                //if not, change its position to next row
                square.transform.position = initialPos - new Vector3(0, _squareSize * squareRow, 0);
                squareRow++;
            }

            lastSquarePos = square.transform.position;
            returnList.Add(square.GetComponent<Square>());
        }

        return returnList;
    }

    private float CalculateSquareSize(int nmbOfSquares)
    {
        float size = Mathf.Ceil((float) nmbOfSquares / 7);
        size = 2 / size;
        size = Mathf.Max(size, MinSpawnSquareSize);
        return size;
    }

    private IEnumerator SortByColor(List<Square> squareList)
    {
        foreach (var square in squareList)
        {
            yield return new WaitForSeconds(0.5f);
            switch (square.color)
            {
                case SquareColor.red:
                    square.MoveTo(TargetCalc(redBegin.position, _redSquares));
                    _redSquares++;
                    break;
                case SquareColor.green:
                    square.MoveTo(TargetCalc(greenBegin.position, _greenSquares));
                    _greenSquares++;
                    break;
                case SquareColor.blue:
                    square.MoveTo(TargetCalc(blueBegin.position, _blueSquares));
                    _blueSquares++;
                    break;
            }
        }

        yield return StartCoroutine(SortByItself(_squareList2));
    }

    private IEnumerator SortByItself(List<Square> squareList)
    {
        int indexOrdered = 0;
        yield return new WaitForSeconds(1f);
        for (int i = indexOrdered; i < squareList.Count; i++)
        {
            if (squareList[i].color == SquareColor.red)
            {
                if (i != 0)
                {
                    squareList[i].MoveTo(squareList[indexOrdered].GetPosition());
                    squareList[indexOrdered].MoveTo(squareList[i].GetPosition());

                    Square tempSquare = squareList[indexOrdered];
                    squareList[indexOrdered] = squareList[i];
                    squareList[i] = tempSquare;
                    indexOrdered++;
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    indexOrdered++;
                }
            }
        }

        for (int i = indexOrdered; i < squareList.Count; i++)
        {
            if (squareList[i].color == SquareColor.green)
            {
                if (i != 0)
                {
                    squareList[i].MoveTo(squareList[indexOrdered].GetPosition());
                    squareList[indexOrdered].MoveTo(squareList[i].GetPosition());

                    Square tempSquare = squareList[indexOrdered];
                    squareList[indexOrdered] = squareList[i];
                    squareList[i] = tempSquare;
                    indexOrdered++;
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    indexOrdered++;
                }
            }
        }
    }

    Vector3 TargetCalc(Vector3 beginPos, int squaresAlready)
    {
        int row = Mathf.FloorToInt((float) squaresAlready / _squaresThatFillARow);
        squaresAlready -= row * _squaresThatFillARow;
        beginPos.x += squaresAlready * _squareSize;
        beginPos.y -= row * _squareSize;
        return beginPos;
    }
}