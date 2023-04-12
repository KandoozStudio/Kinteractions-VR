using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.Interactions.Runtime
{
    [CreateAssetMenu]
    public class Config : ScriptableObject
    {
        private enum InputManagerType
        {
            UnityAxisBased = 0,
            InputSystem = 1,
            KeyboardMock = -1
        }

        [FormerlySerializedAs("inputManager")] [SerializeField]
        private InputManagerBase inputManagerBase;

        [SerializeField] private GameObject gameManager;

        [SerializeField] private InputManagerType inputType;
        public InputManagerBase InputManagerBase
        {
            get
            {
                if (!gameManager)
                {
                    gameManager = new GameObject("VR Manager");
                    DontDestroyOnLoad(gameManager);
                }

                return CreateInputManager();
            }
        }
        
        private InputManagerBase CreateInputManager()
        {
            switch (inputType)
            {
                case InputManagerType.UnityAxisBased:
                    if (inputManagerBase != null && inputManagerBase is AxisBasedInputManagerBase) return inputManagerBase;
                    if (inputManagerBase) Destroy(inputManagerBase);
                    inputManagerBase = gameManager.AddComponent<AxisBasedInputManagerBase>();
                    break;
                case InputManagerType.InputSystem:
                    if (inputManagerBase != null && inputManagerBase is InputSystemBased) return inputManagerBase;
                    if (inputManagerBase) Destroy(inputManagerBase);
                    inputManagerBase = gameManager.AddComponent<InputSystemBased>();
                    break;
                case InputManagerType.KeyboardMock:
                    if (inputManagerBase != null && inputManagerBase is KeyboardBasedInput) return inputManagerBase;
                    if (inputManagerBase) Destroy(inputManagerBase);
                    inputManagerBase = gameManager.AddComponent<KeyboardBasedInput>();

                    break;
            }

            return inputManagerBase;
        }
    }
    
    public class InputSystemBased : InputManagerBase
    {
        
    }

}