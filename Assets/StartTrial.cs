using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTrial : MonoBehaviour
{
    public Transform pointer; // Assign the pointer's transform in the inspector
    public Text instruct;    // Assign your Text UI component in the inspector
    public GameObject objectToControl;

    private Collider coll;
    private Renderer visibility;//fixation visiability
    //private bool isActive = false;//fixation activate or not
    private bool start = true;
    // Start is called before the first frame update

    void Start()
    {
        visibility = objectToControl.GetComponent<Renderer>();
        visibility.enabled = false;
        coll = objectToControl.GetComponent<SphereCollider>();
        instruct.text = "press Space to start";
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (instruct.text == "press Space to start" && visibility.enabled==false)
            {
                visibility.enabled = true;
                instruct.text = "";
                start = false;
                Debug.Log("press space at begining");
            }
            else
            {
                instruct.text = "press Space to start";
                objectToControl.SetActive(true);
                start = true;
                Debug.Log("press space at th end");
            }
            
        }

        if (OnTriggerStay(coll, pointer.GetComponent<SphereCollider>()) && visibility.enabled)
        {
            visibility.enabled = false;
            objectToControl.SetActive(false);
        }
    }

    bool OnTriggerStay(Collider mainCollider, Collider other)
    {
        return mainCollider.bounds.Contains(other.bounds.min) && mainCollider.bounds.Contains(other.bounds.max);
    }
}
