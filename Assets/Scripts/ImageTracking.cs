using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ImageTracking : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject prefabToSpawn;  // Assign the 3D model prefab in Inspector

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateObjectPosition(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            RemoveObject(trackedImage);
        }
    }

    private void SpawnObject(ARTrackedImage trackedImage)
    {
        if (!spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject newObject = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedObjects[trackedImage.referenceImage.name] = newObject;
        }
    }

    private void UpdateObjectPosition(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
        {
            spawnedObject.transform.position = trackedImage.transform.position;
            spawnedObject.transform.rotation = trackedImage.transform.rotation;
        }
    }

    private void RemoveObject(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
        {
            Destroy(spawnedObject);
            spawnedObjects.Remove(trackedImage.referenceImage.name);
        }
    }
}
