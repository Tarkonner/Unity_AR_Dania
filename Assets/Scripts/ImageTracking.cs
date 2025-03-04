using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static ImageTracking;

public class ImageTracking : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    [System.Serializable]
    public struct MarkerPrefab
    {
        public string markerName; // Name of the marker in the XRReferenceImageLibrary
        public GameObject prefab; // Prefab to spawn for this marker
    }

    public List<MarkerPrefab> markerPrefabs; // Assign in Inspector

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> markerPrefabDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        // Convert List to Dictionary for fast lookup
        foreach (var entry in markerPrefabs)
        {
            markerPrefabDict[entry.markerName] = entry.prefab;
        }
    }

    void OnEnable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
        }
    }

    void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.RemoveAllListeners();
        }
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateObjectPosition(trackedImage);
        }

        //foreach (var trackedImage in eventArgs.removed)
        //{
        //    RemoveObject(trackedImage);
        //}
    }

    private void SpawnObject(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!spawnedObjects.ContainsKey(imageName) && markerPrefabDict.ContainsKey(imageName))
        {
            GameObject prefabToSpawn = markerPrefabDict[imageName];
            GameObject newObject = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedObjects[imageName] = newObject;
        }
    }

    private void UpdateObjectPosition(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
        {
            spawnedObject.transform.position = trackedImage.transform.position;
            spawnedObject.transform.rotation = trackedImage.transform.rotation;

            if (!spawnedObject.activeSelf)
                spawnedObject.SetActive(true);
        }
    }

    private void RemoveObject(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
        {
            spawnedObject.SetActive(false); // Disable instead of destroy (better performance)
        }
    }
}
