using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int rounds = 1;
    public int fibonacciAtual = 1;
    public int fibonacciPassada = 0;
    public List<GameObject> monsters = new List<GameObject>();
    public CanvasScript _canvas;
    void Start()
    {
        
    }

   
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            NextRound();
        }
    }

    void NextRound()
    {
        rounds = rounds + 1;
        fibonacciAtual = fibonacciAtual + fibonacciPassada;
        fibonacciPassada = fibonacciAtual - fibonacciPassada;
        _canvas.minutos = 0;
        _canvas.segundos = 0;
    }
}
