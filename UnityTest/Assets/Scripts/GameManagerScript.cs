using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

public class GameManagerScript : MonoBehaviour
{
    [Tooltip("Number of rounds")]
    public int rounds = 1;
    [Tooltip("Number of currentFibonacci")]
    [SerializeField] private int currentFibonacci = 1;
    [Tooltip("Number of previousFibonacci")]
    [SerializeField] private int previousFibonacci = 0;
    [Tooltip("Link to CanvasScript")]
    [SerializeField] private CanvasScript _canvas;
    [Tooltip("boolean to check if it is in pause between rounds")]
    [SerializeField] private bool pause;
    [Tooltip("All Monsters Spawned")]
    public int monterSpawned = 0;
    [Tooltip("Link to Script of PrefabMonster")]
    [SerializeField] private MonsterScript monsterPrefabScript;  
    public IObjectPool<MonsterScript> monsterPool;  
    private bool collectionCheck = true;  
    private int defaultCapacity = 1000;
    private int maxSize = 10000;
    [Tooltip("All Monsters Released")]
    public int monstersReleased = 0;  

    /// <summary>
    /// Initialize the monster pool
    /// </summary>
    private void Awake()
    {
        
        monsterPool = new ObjectPool<MonsterScript>(CreateProjectile,
        OnGetFromPool, OnReleaseToPool,
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    /// <summary>
    /// Invoked when creating an item to populate the object pool
    /// </summary>
    /// <returns></returns>
    private MonsterScript CreateProjectile()
    {
        MonsterScript projectileInstance = Instantiate(monsterPrefabScript);
        projectileInstance.ObjectPool = monsterPool;
        return projectileInstance;
    }

    /// <summary>
    /// Invoked when returning an item to the object pool
    /// </summary>
    /// <param name="pooledObject"></param>
    private void OnReleaseToPool(MonsterScript pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// Invoked when retrieving the next item from the object pool
    /// </summary>
    /// <param name="pooledObject"></param>
    private void OnGetFromPool(MonsterScript pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// Invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    /// </summary>
    /// <param name="pooledObject"></param>
    private void OnDestroyPooledObject(MonsterScript pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    /// <summary>
    /// Enter pause between rounds
    /// </summary>
    void Update()
    {
        if (currentFibonacci == monstersReleased && !pause)
        {
            StartCoroutine(Interval());
        }
    }
    /// <summary>
    /// instantiate the monsters and Randomizes the Y-axis
    /// </summary>
    private void FixedUpdate()
    {
        // Get a pooled object instead of instantiating
        if (monterSpawned >= currentFibonacci)
        {
            return;
        }
        monterSpawned++;
        MonsterScript monsterObject = monsterPool.Get();

        if (monsterObject == null)
            return;

        
        int randy = UnityEngine.Random.Range(-20, 20);
        monsterObject.transform.position = new Vector2(-50, randy);
    }
    /// <summary>
    /// This code resets the timer and initiates another round following the Fibonacci sequence
    /// </summary>
    void NextRound()
    {
        rounds = rounds + 1;
        currentFibonacci = currentFibonacci + previousFibonacci;
        previousFibonacci = currentFibonacci - previousFibonacci;
        _canvas.minutos = 0;
        _canvas.seconds = 0;
        monstersReleased = 0;
        pause = false;
        monterSpawned = 0;
    }

    /// <summary>
    /// Coroutine to handle the interval between rounds
    /// </summary>
    /// <returns></returns>
    IEnumerator Interval()
    {
        pause = true;
        yield return new WaitForSeconds(1);
        NextRound();
    }


}
