using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMaster
{
    List<WayPoint> closedList = new List<WayPoint>(), openList = new List<WayPoint>(), pathList = new List<WayPoint>();

    public List<WayPoint> FindPath(WayPoint prev, WayPoint next, WayPoint dest)
    {
        openList.Clear();
        closedList.Clear();
        pathList.Clear();

        if (prev == dest)
        {
            pathList.Add(prev);
            return pathList;
        }
        else if (next == dest)
        {
            pathList.Add(next);
            return pathList;
        }

        prev.g = 0;
        prev.h = Vector3.Distance(prev.transform.position, dest.transform.position);
        prev.f = prev.g + prev.h;
        openList.Add(prev);

        if (prev != next)
        {
            next.g = 0;
            next.h = Vector3.Distance(next.transform.position, next.transform.position);
            next.f = next.g + next.h;
            openList.Add(next);
        }

        while (openList.Count != 0)
        {
            float f = openList[0].f;
            int index = 0;
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].f < f)
                {
                    f = openList[i].f;
                    index = i;
                }
            }
            closedList.Add(openList[index]);
            openList.RemoveAt(index);
            WayPoint LastNode = closedList[closedList.Count - 1];
            foreach (WayPoint child in LastNode.childList)
            {
                int skip = 0;
                foreach (WayPoint node in closedList)
                {
                    if (child == node)
                    {
                        skip = 1;
                        break;
                    }
                }
                if (skip == 0)
                {
                    foreach (WayPoint node in openList)
                    {
                        if (child == node)
                        {
                            skip = 1;
                            float dist = Vector3.Distance((closedList[closedList.Count - 1] as WayPoint).transform.position, child.transform.position);
                            if (((closedList[closedList.Count - 1] as WayPoint).g + dist) < child.g)
                            {
                                child.parentNode = closedList[closedList.Count - 1];
                                child.g = (closedList[closedList.Count - 1] as WayPoint).g + dist;
                                child.h = Vector3.Distance(child.transform.position, dest.transform.position);
                                child.f = child.g + child.h;
                            }
                        }
                    }
                }

                if (skip == 0)
                {
                    child.parentNode = closedList[closedList.Count - 1];
                    child.g = (closedList[closedList.Count - 1] as WayPoint).g + Vector3.Distance((closedList[closedList.Count - 1] as WayPoint).transform.position, child.transform.position);
                    child.h = Vector3.Distance(child.transform.position, dest.transform.position);
                    child.f = child.g + child.h;
                    openList.Add(child);
                }
                if (child == dest)
                {
                    WayPoint nextPoint = LastNode;
                    pathList.Insert(0, dest);
                    while (nextPoint != null)
                    {
                        pathList.Insert(0, nextPoint);
                        nextPoint = nextPoint.parentNode;
                    }

                    return pathList;
                }
            }
        }

        pathList.Insert(0, dest);
        return pathList;
    }
}
