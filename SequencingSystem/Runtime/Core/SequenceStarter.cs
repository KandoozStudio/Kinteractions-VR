using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.Kuest
{
    public class SequenceStarter : MonoBehaviour
    {
        [SerializeField] public Sequence sequence;
        [SerializeField] private bool starOnAwake = true;
        [SerializeField]private float delay=0;
        private async void OnEnable()
        {
            if (starOnAwake)
            {
                while (delay>0)
                {
                    await Task.Yield();
                    delay -= Time.deltaTime;
                }
                sequence.Begin();
            }
        }

        public void StartQuest()
        {
            sequence.Begin();
        }
    }
}