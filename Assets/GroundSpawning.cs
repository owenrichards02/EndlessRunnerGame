using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GroundSpawning : MonoBehaviour
{
    private float timeSince;
    public float spawnInterval;
    public float spawnIntervalDefault;
    public float initialDelay; //NEED TO IMPLEMENT
    public Vector2 spawnPosition;
    public GameObject section1;
    public GameObject section2;
    public List<GameObject> prefabs = new List<GameObject>();
    bool spawnedFirst = false;
    // Start is called before the first frame update
    void Start()
    {
        timeSince = 0.0f;
        initialDelay = 2.0f;
        spawnInterval = spawnIntervalDefault;
    }

    // Update is called once per frame
    void Update()
    {
        initialDelay -= Time.deltaTime;
        timeSince += Time.deltaTime;
        if ((timeSince > spawnInterval) || !spawnedFirst && initialDelay < 0.0f){
            spawnedFirst = true;
            Debug.Log("ground spawned");
            spawnRandom(0);
            timeSince = 0.0f;
        }
    }

    void spawnRandom (float yOffset){
            var r = new System.Random();
            int choice = r.Next(0,prefabs.Count);
            Vector2 pos = new Vector2(spawnPosition.x, spawnPosition.y + yOffset);
            GameObject justSpawned = Instantiate(prefabs[choice]);
            justSpawned.transform.position = pos;
    }
}
