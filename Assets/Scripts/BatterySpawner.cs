using UnityEngine;
using System.Collections;

public class BatterySpawner : MonoBehaviour
{

    public Vector3 spawnBounds;
    public float spawnRate;
    public Vector2 powerVariance;

    public GameObject batteryPrefab;

    public Transform batteryContainer;

    float lastSpawn;

    void Awake()
    {
        lastSpawn = spawnRate;
    }

    void Update()
    {
        lastSpawn += Time.deltaTime;
        if (batteryPrefab != null)
        {
            if (lastSpawn >= spawnRate)
            {
                lastSpawn = 0.0f;
                // spawn battery
                GameObject battery = Instantiate(batteryPrefab,
                    new Vector3(
                    Random.Range(-spawnBounds.x, spawnBounds.x),
                    Random.Range(spawnBounds.y * 0.5f, spawnBounds.y),
                    Random.Range(-spawnBounds.z, spawnBounds.z)),
                    Quaternion.identity) as GameObject;

                battery.GetComponent<IntData>().data = Random.Range((int)powerVariance.x, (int)powerVariance.y);
                battery.transform.SetParent(batteryContainer);
            }
        }
        else
        {
            Debug.LogWarning("batteryPrefab not set in " + this.gameObject.name);
        }
    }
}
