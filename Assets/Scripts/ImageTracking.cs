using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
    Dictionary<string, List<GameObject>> objectsOnMarker = new Dictionary<string, List<GameObject>>();

    [Header("Distance")]
    [SerializeField] float distanceBetweenElements = .5f;

    void Awake()
    {
        // Convert List to Dictionary for fast lookup
        foreach (var entry in markerPrefabs)
        {
            markerPrefabDict[entry.markerName] = entry.prefab;
            objectsOnMarker.Add(entry.markerName, new List<GameObject>());
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
        TrackableId id = trackedImage.trackableId;

        if (!spawnedObjects.ContainsKey(id.ToString()) && markerPrefabDict.ContainsKey(imageName))
        {
            GameObject prefabToSpawn = markerPrefabDict[imageName];
            GameObject spawnObject = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnObject.name = imageName + id.ToString();
            spawnedObjects[id.ToString()] = spawnObject;

            objectsOnMarker.Add(imageName, new List<GameObject> { spawnObject });
        }
    }

    private void UpdateObjectPosition(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.trackableId.ToString(), out GameObject spawnedObject))
        {
            spawnedObject.transform.position = trackedImage.transform.position;
            spawnedObject.transform.rotation = trackedImage.transform.rotation;

            if (!spawnedObject.activeSelf)
                spawnedObject.SetActive(true);
        }

        /*
        if (spawnedObjects.ContainsKey(trackedImage.trackableId.ToString()))
        {
            //Look for same object close by
            GameObject closetsExitens = null;
            float currentDistance = 0;
            foreach (GameObject item in objectsOnMarker[trackedImage.trackableId.ToString()])
            {
                float distance = Vector3.Distance(item.transform.position, transform.transform.position);
                if (distance < distanceBetweenElements)
                {
                    if (closetsExitens == null)
                    {
                        closetsExitens = item;
                        currentDistance = distance;
                    }
                    else if (closetsExitens != null && currentDistance > distanceBetweenElements)
                    {
                        closetsExitens = item;
                        currentDistance = distance;
                    }
                }
            }

            if(closetsExitens == null)
            {
                SpawnObject(trackedImage);
            }
            else
            {
                closetsExitens.transform.position = trackedImage.transform.position;
                closetsExitens.transform.rotation = trackedImage.transform.rotation;

                if (!closetsExitens.activeSelf)
                    closetsExitens.SetActive(true);
            }
        }
        */
    }

    private void RemoveObject(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
        {
            spawnedObject.SetActive(false); // Disable instead of destroy (better performance)
        }
    }
}
