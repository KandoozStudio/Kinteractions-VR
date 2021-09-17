using UnityEngine;
namespace Kandooz.ScriptableSystem
{
    public interface IScriptableVariable 
    {
    }
    public abstract class ScriptableVariable<T> : ScriptableObject, IScriptableVariable
    {
        public T value;

        public override string ToString()
        {
            return value.ToString();
        }
        public static implicit operator T(ScriptableVariable<T> value)
        {
            return value.value;
        }
    }
}