using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour
{
    public static DataSender DS;
    public StageStartData Data;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(DS != null)
        {
            Destroy(DS.gameObject);
        }
        DS = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
