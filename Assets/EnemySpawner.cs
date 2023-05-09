using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        HashSet<int> excludedValues = new HashSet<int>() { -1, 0, 1 };
        System.Random rand = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemy);

            enemy.transform.position = new Vector3(player.transform.position.x + rand.Next(-10, 10 - excludedValues.Count), player.transform.position.y, player.transform.position.z + rand.Next(-10, 10 - excludedValues.Count));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
