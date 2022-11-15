using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawning : MonoBehaviour
{
    private float timeSince;
    public float spawnInterval = 1.0f;
    public Vector2 spawnPosition;
    public GameObject groundPrefab;
    // Start is called before the first frame update
    void Start()
    {
        timeSince = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSince += Time.deltaTime;
        if (timeSince > spawnInterval){
            
            Debug.Log("ground spawned");
            GameObject justSpawned = Instantiate(groundPrefab);
            justSpawned.transform.position = spawnPosition;
            timeSince = 0.0f;
        }
    }
}
