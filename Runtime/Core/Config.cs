using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{
    [CreateAssetMenu]
    public class Config : ScriptableObject
    {
        [SerializeField] private InputManagerBase inputManager;
        [SerializeField] private LayerMask leftHandLayer;
        [SerializeField] private LayerMask rightHandLayer;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private GameObject gameManager;
        [SerializeField] private InputManagerType inputType;
        
        public int LeftHandLayer => leftHandLayer;
        public int RightHandLayer => leftHandLayer;
        public InputManagerBase InputManager
        {
            get
            {
                if (inputManager) return inputManager;
                if (gameManager) return CreateInputManager();
                gameManager = new GameObject("VR Manager");
                DontDestroyOnLoad(gameManager);
                return CreateInputManager();
            }
        }
        private InputManagerBase CreateInputManager()
        {
            #if UNITY_EDITOR_OSX
            if (inputManager != null && inputManager is KeyboardBasedInput) return inputManager;
            if (inputManager) Destroy(inputManager);
            inputManager = gameManager.AddComponent<KeyboardBasedInput>();
            return inputManager;
            #endif
            switch (inputType)
            {
                case InputManagerType.UnityAxisBased:
                    if (inputManager != null && inputManager is AxisBasedInputManagerBase) return inputManager;
                    if (inputManager) Destroy(inputManager);
                    inputManager = gameManager.AddComponent<AxisBasedInputManagerBase>();
                    break;
                case InputManagerType.InputSystem:
                    if (inputManager != null && inputManager is InputSystemBased) return inputManager;
                    if (inputManager) Destroy(inputManager);
                    inputManager = gameManager.AddComponent<InputSystemBased>();
                    break;
                case InputManagerType.KeyboardMock:
                    if (inputManager != null && inputManager is KeyboardBasedInput) return inputManager;
                    if (inputManager) Destroy(inputManager);
                    inputManager = gameManager.AddComponent<KeyboardBasedInput>();

                    break;
            }

            return inputManager;
        }
        private enum InputManagerType
        {
            UnityAxisBased = 0,
            InputSystem = 1,
            KeyboardMock = -1
        }

    }
    
    public class InputSystemBased : InputManagerBase
    {
        
    }

}