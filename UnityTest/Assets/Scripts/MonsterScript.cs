using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class MonsterScript : MonoBehaviour
{
    [Tooltip("minimum speed of 500 to 1000")]
    [SerializeField] private int Velocity;
    [Tooltip("boolean to check if it is visible")]
    [SerializeField] private bool onScreem = false;
    private GameManagerScript gameManagerScript;
    private IObjectPool<MonsterScript> objectPool;
    public IObjectPool<MonsterScript> ObjectPool { set => objectPool = value; }
    /// <summary>
    /// Find the GameManager 
    /// </summary>
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
    /// <summary>
    /// When it is reused, it changes its speed every time it appears on the screen 
    /// </summary>
    void OnEnable()
    {
        Velocity = Random.Range(500,1000);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Velocity, 0));
    }
    /// <summary>
    /// Code to check whether it is displayed on the screen; if it is not, it can be disabled and return to the pool.
    /// </summary>
    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            onScreem = true;
        }
        if (onScreem)
        {
            if (!GetComponent<Renderer>().isVisible)
            {
                onScreem = false;
                StartCoroutine(TimeToDie());
            }


        }
    }
    /// <summary>
    /// After being off the screen, it waits for 1 second to be disabled.
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(1);
        gameManagerScript.monstersReleased++;
        gameManagerScript.monsterPool.Release(this);
    }
}
