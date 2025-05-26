using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public GameObject prefabToCreate;
    public List<GameObject> createdObjects;

    private void Start()
    {
        createdObjects = new List<GameObject>();
    }

    public GameObject GetObject(Vector3 posToSpawn)
    {
        for(int indexObjects = 0; indexObjects < createdObjects.Count; indexObjects++)
        {
            if (!createdObjects[indexObjects].activeInHierarchy)
            {
                createdObjects[indexObjects].transform.position = posToSpawn;
                createdObjects[indexObjects].SetActive(true);
                return createdObjects[indexObjects];
            }
        }
        GameObject createdObject = Instantiate(prefabToCreate, posToSpawn, Quaternion.identity, transform);
        createdObjects.Add(createdObject);
        return createdObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
