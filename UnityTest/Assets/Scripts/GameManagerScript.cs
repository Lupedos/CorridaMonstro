using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManagerScript : MonoBehaviour
{
    public int rounds = 1;
    public int fibonacciAtual = 1;
    public int fibonacciPassada = 0;
    public GameObject prefabMonster;
    
    public List<GameObject> monsters = new List<GameObject>();
    public CanvasScript _canvas;
    public int lastMonstersIndex = 0;
    public bool terminol;
    public bool pausa;
    public List<GameObject> objetosAtivos; 
    public int montrinhos;
    void Awake()
    {
        //GameObject newMonster = Instantiate(prefabMonster);
        //monsters.Add(newMonster);
        
    }
    void Update()
    {
        objetosAtivos = monsters.FindAll(MonsterRunner => MonsterRunner.activeSelf);
        if(Input.GetKeyDown("space"))
        {
            NextRound();
        }
        if(objetosAtivos.Count == 0 && !terminol && !pausa)
        {
            
            StartCoroutine(Interval());
        }
        if(terminol)
        {
            SpawMonster();
        }
        
    }

    void NextRound()
    {
        if(terminol)
        return;
        rounds = rounds + 1;
        fibonacciAtual = fibonacciAtual + fibonacciPassada;
        fibonacciPassada = fibonacciAtual - fibonacciPassada;
        _canvas.minutos = 0;
        _canvas.segundos = 0;
        terminol = true;
        pausa = false;
        montrinhos = 0;
    }
    void SpawMonster()
    {
        //int objetosAtivos = Array.FindAll(monsters, obj => obj.activeSelf).Length;
        //List<GameObject> objetosAtivos = monsters.FindAll(MonsterRunner => MonsterRunner.activeSelf);
        //Debug.Log(objetosAtivos.Count);
        if(montrinhos >= fibonacciAtual)
        {
            terminol = false;
            return ;
        }
        if(objetosAtivos.Count == monsters.Count)
        {
            GameObject newMonster = Instantiate(prefabMonster);
            monsters.Add(newMonster);
        }
        monsters[lastMonstersIndex].SetActive(true);
        int randy = UnityEngine.Random.Range(-20,20);
        monsters[lastMonstersIndex].transform.position = new Vector2(-50, randy);
        montrinhos++;
        //Debug.Log("foi");
        //Debug.Log(monsters.Count);
        lastMonstersIndex++;
        if(lastMonstersIndex > monsters.Count -1)
        {
            lastMonstersIndex = 0;
        }
    }
    IEnumerator Interval()
    {
        pausa = true;
        yield return new WaitForSeconds(2);
        NextRound();
    }
 
}
