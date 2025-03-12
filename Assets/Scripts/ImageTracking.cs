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

    //Closet target
    public static ImageTracking instance;
    public GameObject closetObject { get; private set; }
    [SerializeField] float maxDistance = 2;

    void Awake()
    {
        instance = this;

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
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                UpdateObjectPosition(trackedImage);
            }
            else if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None)
            {
                HandleMarkerOutOfView(trackedImage);
            }
        }
        
        foreach (var trackedImage in eventArgs.removed)
        {
            RemoveObject(trackedImage.Value);
        }
    }

    private void Update()
    {
        if (spawnedObjects == null || spawnedObjects.Count == 0)
            return;

        float closetDistance = maxDistance;
        foreach (var item in spawnedObjects)
        {
            float distance = Vector3.Distance(Camera.main.transform.position, item.Value.transform.position);
            if(distance < closetDistance)
            {
                closetDistance = distance;
                closetObject = item.Value;
            }
        }
    }

    private void HandleMarkerOutOfView(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.trackableId.ToString(), out GameObject spawnedObject))
        {
            spawnedObject.SetActive(false); // Hide object when marker is lost
        }
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
    }

    private void RemoveObject(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
        {
            spawnedObject.SetActive(false); // Disable instead of destroy (better performance)
        }
    }
}
