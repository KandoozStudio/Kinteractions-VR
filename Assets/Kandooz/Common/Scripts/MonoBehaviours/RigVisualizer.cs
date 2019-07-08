using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.Common
{
    public class RigVisualizer : MonoBehaviour
    {
        public Color color = Color.red;
        void OnDrawGizmos()
        {
            Gizmos.color =color;
            
            Color childColor = color;
            childColor.r = color.b;
            childColor.g = color.r;
            childColor.b = color.g;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                Gizmos.DrawLine(this.transform.position, child.position);
                var visualizer = child.GetComponent<RigVisualizer>();
                if (!visualizer)
                {
                    child.gameObject.AddComponent<RigVisualizer>().color=childColor;
                }
                else
                {
                    visualizer.color = childColor;
                }

            }
        }
    }

}
