using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBonusManager : MonoBehaviour
{
    [Header("To be assigned")]
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
    private Camera _camera;
    private List<Square> _squareList = new List<Square>();

    private IEnumerator StartSorting()
    {
        foreach (var square in _squareList)
        {
            Debug.Log(square.name);
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
    }

    Vector3 TargetCalc(Vector3 beginPos, int squaresAlready)
    {
        beginPos.x += squaresAlready * _squareSize;
        return beginPos;
    }

    private void SpawnSquares()
    {
        int squareRow = 1;
        Vector3 lastSquarePos = squareSpawnerPos.position;
        for (int i = 0; i < SquaresToSpawn; i++)
        {
            var square = Instantiate(squarePrefab, lastSquarePos,transform.rotation);
            square.transform.localScale = new Vector3(_squareSize,_squareSize,_squareSize);
            if(i!=0)
                square.transform.position += new Vector3(_squareSize, 0 ,0);

            //check if can be seen
            Vector3 viewPos = _camera.WorldToViewportPoint(square.transform.position);
            if (viewPos.x > 1f || viewPos.x < 0)
            {
                //if not, change its position to next row
                square.transform.position = squareSpawnerPos.position - new Vector3(0, _squareSize * squareRow, 0);
                squareRow++;
            }
            lastSquarePos = square.transform.position;
            _squareList.Add(square.GetComponent<Square>());
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
        _camera = Camera.main;
        _squareSize = CalculateSquareSize(SquaresToSpawn);
        SpawnSquares();
        StartCoroutine(StartSorting());
    }

}
