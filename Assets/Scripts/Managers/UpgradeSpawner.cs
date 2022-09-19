using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    private float timer;

    private void Start()
    {
        GetTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnObject();
            GetTimer();
        }
    }

    private void SpawnObject()
    {
        float randomX = Random.Range(-14f, 14f);
        float randomZ = Random.Range(-14f, 14f);
        Vector3 tempPos = new Vector3(randomX, 3f, randomZ);
        Instantiate(spawnObject, tempPos, Quaternion.identity, transform);
    }

    private void GetTimer()
    {
        timer = Random.Range(1f, 2f);
    }
}
