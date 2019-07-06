using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigVisualizer : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            
            Gizmos.DrawLine(this.transform.position, child.position);
            if (!child.GetComponent<RigVisualizer>())
            {
                child.gameObject.AddComponent<RigVisualizer>();
            }
            
        }
    }
}
