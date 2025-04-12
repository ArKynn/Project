using System.Collections.Generic;
using UnityEngine;

public static class Detector
{
    private static List<T> GetObjectsInArea<T>(Transform areaCenter, float areaRadius, LayerMask layerMask)
    {
        var foundObjects = new List<T>();
        var hit = new RaycastHit[100];
        Physics.SphereCastNonAlloc(areaCenter.position, areaRadius, areaCenter.forward, hit, 0, layerMask);
        foreach (var obj in hit)
        {
            if(obj.transform == null) continue;
            
            T temp;
            if((temp = obj.transform.GetComponent<T>()) != null) foundObjects.Add(temp);
        }
        
        return foundObjects;
    }

    public static T GetClosestInArea<T>(Transform areaCenter, float areaRadius, LayerMask layerMask) where T : Component
    {
        var foundObjects = GetObjectsInArea<T>(areaCenter, areaRadius, layerMask);
        if(foundObjects == null || foundObjects.Count == 0) return null;
        
        foundObjects.Sort((t1, t2) => 
            Vector3.Distance(areaCenter.position, t1.transform.position).CompareTo(Vector3.Distance(areaCenter.position, t2.transform.position)));
        
        return foundObjects[0];
    }
}