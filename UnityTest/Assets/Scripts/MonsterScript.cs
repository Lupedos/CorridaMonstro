using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public int Velocity;
    public bool onScream = false;
    void Start()
    {
        Velocity = Random.Range(500,1000);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Velocity, 0));
    }

    
    void Update()
    {
        if(GetComponent<Renderer>().isVisible)
        {
            onScream = true;            
        }
        if (onScream)
        {
            if(!GetComponent<Renderer>().isVisible)
            {
                Destroy(gameObject,1f);
            }
            
        }
    }
}
