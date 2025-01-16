using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimator : MonoBehaviour
{
    public Animator ani;
    [SerializeField]
    RuntimeAnimatorController[] anis;

    // Start is called before the first frame update
    void Start()
    {
        ani.runtimeAnimatorController = anis[Random.Range(0, anis.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
