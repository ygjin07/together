using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectNormal : MonoBehaviour
{
    public PeopleMove PM;
    public GameObject tagObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (tagObj.tag == "Corona")
        {
            if (collision.tag == "Normal")
            {
                Debug.Log("Detect");
                PM.StopMoveCor();
                PM.SetDest(collision.transform);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (tagObj.tag == "Corona")
        {
            if (collision.tag == "Normal")
            {
                PM.StartMoveCor();
            }
        }
    }
}
