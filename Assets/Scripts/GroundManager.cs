using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject finishPrefab;
    public GameObject groundContainer;
    public int roadCount = 10;

    private Transform newSpawnPoint;

    void Start()
    {
        newSpawnPoint = roadPrefab.transform.GetChild(0);

        for (int i = 0; i < roadCount; i++)
        {
            SpawnRoad();
        }

        Instantiate(finishPrefab, newSpawnPoint.position - new Vector3(), finishPrefab.transform.rotation, parent: groundContainer.transform);
    }

    private void SpawnRoad()
    {
        GameObject newRoad = Instantiate(roadPrefab, newSpawnPoint.position, roadPrefab.transform.rotation, parent: groundContainer.transform);
        newSpawnPoint = newRoad.transform.GetChild(0);
    }
}
