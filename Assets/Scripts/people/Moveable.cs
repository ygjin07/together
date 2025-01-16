using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moveable : MonoBehaviour
{
    NavMeshAgent agent;
    Transform[] wayPoints;
    [SerializeField]
    GameObject ParentWPs;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = ParentWPs.GetComponentsInChildren<Transform>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Move()
    {
        float ranTime;
        while (true)
        {
            ranTime = Random.Range(1f, 3f);
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Length)].position);

            yield return new WaitForSeconds(ranTime);
        }
    }
}
