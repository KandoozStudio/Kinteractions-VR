using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CrossFadingFloat
{
    private float start;
    private float target;
    private float value;
    private float rate;

    public float Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    public CrossFadingFloat()
    {

    }
}
public class FloatLerper : MonoBehaviour {
    private static float[] floats;
    private static FloatLerper instance;

    public static FloatLerper Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
}
