
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    [CreateAssetMenu(menuName = "Kandooz/KVR/Gesture")]
    public class GestureVariable : ScriptableObject
    {
        public Gesture value;
    }

}
