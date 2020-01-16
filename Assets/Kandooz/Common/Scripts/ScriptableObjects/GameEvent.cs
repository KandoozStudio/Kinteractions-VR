using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Kandooz/Common/GameEvent")]
public class GameEvent : ScriptableObject
{
    public UnityEvent onRaised;
    public void Raise()
    {
        onRaised.Invoke();
    }
}
