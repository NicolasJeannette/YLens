using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int lifepoints = 2;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifepoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(other);
    }

    private void TakeDamage(Collider other)
    {
        if (other.tag == "Bullet")
        {
            lifepoints -= 1;
        }
    }
}
