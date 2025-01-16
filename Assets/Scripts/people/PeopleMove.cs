using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMove : MonoBehaviour
{
    public float Speed;
    int isStop = 1;
    Vector3 moveVec;
    WayPoint[] wayPoints;
    Transform des;
    Coroutine move;
    WayPointMaster WPM = new WayPointMaster();
    public WayPoint PrevWayPoint, NextWayPoint;
    public GameObject ParentWPs;
    [SerializeField]
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = ParentWPs.GetComponentsInChildren<WayPoint>();
        StartMoveCor();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop == 1)
        {
            if (moveVec.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    public void SetDest(Transform dest)
    {
        Vector3 tmp = dest.position - this.transform.position;
        des = dest;
        moveVec = tmp / tmp.magnitude;
    }

    public void Stop()
    {
        isStop = 0;
    }

    public void Go()
    {
        isStop = 1;
    }

    public void StopMoveCor()
    {
        if (move != null)
            StopCoroutine(move);
    }

    public void StartMoveCor()
    {
        move = StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        List<WayPoint> path;
        WayPoint destPoint;
        while (true)
        {
            ClearWayList();
            destPoint = wayPoints[Random.Range(0, wayPoints.Length)];
            path = WPM.FindPath(PrevWayPoint, NextWayPoint, destPoint);
            for (int i = 0; i < path.Count; i++)
            {
                SetDest(path[i].transform);
                PrevWayPoint = path[i];
                if (i + 1 < path.Count)
                {
                    NextWayPoint = path[i + 1];
                }

                yield return StartCoroutine(MoveNextPoint());
            }
        }
    }

    IEnumerator MoveNextPoint()
    {
        while((des.position - this.transform.position).sqrMagnitude > 0.1)
        {
            this.gameObject.transform.position += moveVec * Speed * Time.deltaTime * isStop;
            yield return null;
        }
    }

    void ClearWayList()
    {
        foreach(WayPoint wp in wayPoints)
        {
            wp.parentNode = null;
        }
    }
}
