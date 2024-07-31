using UnityEngine;
using UnityEngine.UI;  // Include the UI namespace

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabsToInstantiate;
    private int currentPrefabIndex = 0;
    private GameObject currentInstance;
    public GameObject TargetPre;
    public Transform pointer; // Assign the pointer's transform in the inspector
    public Text errorText;    // Assign your Text UI component in the inspector
    public Text instruct;
    Collider coll;
    bool state;
    public GameObject fixation;
    //private float x_loc;
    //private Vector3 targetPosition;


    private void Start()
    {
        state = false;
    }

    private void Update()
    {
        if (!fixation.activeSelf)
        {
            if (currentInstance == null)
            {
                ReplacePrefab();
                
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fixation.SetActive(true);
                instruct.text = "press Space to start";
                errorText.text = "";
            }
        }
        else
        {
            Destroy(currentInstance);
        }

        // Add null checks
        if (pointer == null || pointer.GetComponent<SphereCollider>() == null)
        {
            Debug.LogError("Pointer or SphereCollider on pointer is not assigned!");
            return;
        }

        if (errorText == null)
        {
            Debug.LogError("ErrorText UI component is not assigned!");
            return;
        }
        if (currentInstance != null && instruct.text != "press Space to start")
        {
            //print(coll.bounds.max[0]);
            //print(coll.bounds.min[0]);
            //coll = currentInstance.GetComponent<Collider>()
            state = OnTriggerStay(pointer.GetComponent<SphereCollider>());
        }

    }

    void ReplacePrefab()
    {

        Vector3 spawnPosition = new Vector3(0f, 1.5f, 0f);
        currentInstance = Instantiate(prefabsToInstantiate[currentPrefabIndex], spawnPosition, Quaternion.identity);
        coll = currentInstance.GetComponent<Collider>();

        float radius_loc = currentInstance.name[currentInstance.name.Length - 8] - '0';
        radius_loc = radius_loc / 100f;

        char det = 'l';
        bool dett = (currentInstance.name[0] == det);
        //print(dett);
        //print(coll.bounds.min);
        //print(coll.bounds.min.x);
        //print(coll.bounds.max);
        
        print(coll.bounds.min.GetType());
        
        if (dett)
        {
            //float x_loc = coll.bounds.min.x;
            Vector3 targetPosition = coll.bounds.min;
            //print(targetPosition);
            GameObject endTarget = Instantiate(TargetPre, targetPosition, Quaternion.identity);
            SphereCollider collider = endTarget.GetComponent<SphereCollider>();
            collider.radius = radius_loc;
        }
        else
        {
            //float x_loc = coll.bounds.max.x;
            Vector3 targetPosition = coll.bounds.max;
            GameObject endTarget = Instantiate(TargetPre, targetPosition, Quaternion.identity);
            SphereCollider collider = endTarget.GetComponent<SphereCollider>();
            collider.radius = radius_loc;
        }
        //目前position 和radius都是錯的
        //print(x_loc);
        //float x_loc = coll.bounds.min.x;
        //print(x_loc);
        //Vector3 targetPosition = new Vector3(x_loc, 1.5f, coll.bounds.min.z);
        //print(targetPosition);
        currentPrefabIndex = (currentPrefabIndex + 1) % prefabsToInstantiate.Length;
    }


    bool OnTriggerStay(Collider other)
    {
        if (coll.bounds.Contains(other.bounds.max) && coll.bounds.Contains(other.bounds.min))
        {
            if (state == false)
            {
                print("central sphere is in the pipe");
                errorText.text = ""; // Clear error text if overlapping
            }
            return true;

        }
        else
        {   
            if (state == true)
            { 
                print("central sphere is out of pipe");
                errorText.text = "Pointer is out of range."; // Display error if not overlapping
            }
            return false;
        }
    }
}
