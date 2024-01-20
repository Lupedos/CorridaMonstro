using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterScript : MonoBehaviour
{
    public int Velocity;
    public bool onScream = false;
    private IObjectPool<MonsterScript> objectPool;
    public GameManagerScript gameManagerScript;
    // public property to give the projectile a reference to its ObjectPool
    public GameObject cam;

    public IObjectPool<MonsterScript> ObjectPool { set => objectPool = value; }
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        //cam = GameObject.Find("MainCamera");
       

    }
    void OnEnable()
    {
        Velocity = Random.Range(1000,1000);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Velocity, 0));
    }

    void Update()
    {
        //transform.position = new Vector3(cam.transform.position.x - 39, transform.position.y, transform.position.z);
        if (GetComponent<Renderer>().isVisible)
        {
            onScream = true;
            //Debug.Log(" visivel");
        }
        if (onScream)
        {
            if (!GetComponent<Renderer>().isVisible)
            {
                onScream = false;
                StartCoroutine(TimeToDie());
                //this.gameObject.SetActive(false);
                Debug.Log("nao visivel");
            }


        }
    }
    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(1);
        gameManagerScript.montrinhosReleased++;
        gameManagerScript.monsterPool.Release(this);
    }
}
