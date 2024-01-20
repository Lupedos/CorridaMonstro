using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

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
    public int objetosInativos;
    public int montrinhos = 0;
    [Tooltip("Prefab to shoot")]
    [SerializeField] private MonsterScript monsterPrefab;
    public IObjectPool<MonsterScript> monsterPool;
    [SerializeField] private bool collectionCheck = true;

    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity = 1000;
    [SerializeField] public int maxSize = 10000;
    public int montrinhosReleased = 0;
    private void Awake()
    {
        monsterPool = new ObjectPool<MonsterScript>(CreateProjectile,
        OnGetFromPool, OnReleaseToPool,
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }
    // invoked when creating an item to populate the object pool
    private MonsterScript CreateProjectile()
    {
        MonsterScript projectileInstance = Instantiate(monsterPrefab);
        projectileInstance.ObjectPool = monsterPool;
        return projectileInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(MonsterScript pooledObject)
    {
        pooledObject.gameObject.SetActive(false);

    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(MonsterScript pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(MonsterScript pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    void Update()
    {
        //objetosInativos = monsterPool.CountInactive;
        //    if (Input.GetKeyDown("space"))
        //    {
        //        NextRound();
        //    }
        if (fibonacciAtual == montrinhosReleased && !pausa)
        {
            StartCoroutine(Interval());
            
        }
        //    if (terminol)
        //    {
        //        //SpawMonster();
        //    }

    }
    private void FixedUpdate()
    {
        // get a pooled object instead of instantiating


        if (montrinhos >= fibonacciAtual)
        {
            return;
        }
        montrinhos++;
        MonsterScript monsterObject = monsterPool.Get();

        if (monsterObject == null)
            return;

        // align to gun barrel/muzzle position
        int randy = UnityEngine.Random.Range(-20, 20);
        monsterObject.transform.position = new Vector2(-50, randy);


    }
    void NextRound()
    {
        
        rounds = rounds + 1;
        fibonacciAtual = fibonacciAtual + fibonacciPassada;
        fibonacciPassada = fibonacciAtual - fibonacciPassada;
        _canvas.minutos = 0;
        _canvas.segundos = 0;
        montrinhosReleased = 0;
        pausa = false;
        montrinhos = 0;
    }
    //void SpawMonster()
    //{
    //    //int objetosAtivos = Array.FindAll(monsters, obj => obj.activeSelf).Length;
    //    //List<GameObject> objetosAtivos = monsters.FindAll(MonsterRunner => MonsterRunner.activeSelf);
    //    //Debug.Log(objetosAtivos.Count);
    //    if (montrinhos >= fibonacciAtual)
    //    {
    //        terminol = false;
    //        return;
    //    }
    //    if (objetosAtivos.Count == monsters.Count)
    //    {
    //        GameObject newMonster = Instantiate(prefabMonster);
    //        monsters.Add(newMonster);
    //    }
    //    monsters[lastMonstersIndex].SetActive(true);
    //    int randy = UnityEngine.Random.Range(-20, 20);
    //    monsters[lastMonstersIndex].transform.position = new Vector2(-50, randy);
    //    montrinhos++;
    //    //Debug.Log("foi");
    //    //Debug.Log(monsters.Count);
    //    lastMonstersIndex++;
    //    if (lastMonstersIndex > monsters.Count - 1)
    //    {
    //        lastMonstersIndex = 0;
    //    }
    //}
    IEnumerator Interval()
    {
        pausa = true;
        yield return new WaitForSeconds(1);
        NextRound();
    }

}
