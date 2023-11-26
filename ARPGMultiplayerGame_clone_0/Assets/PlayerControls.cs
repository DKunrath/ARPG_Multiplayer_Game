//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""67b9e269-95a9-4f22-ac04-e4cffcb21df9"",
            ""actions"": [
                {
                    ""name"": ""Movement Keyboard"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a8902fb3-2d12-4fe5-838a-d801d83f66e1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement Console"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c0dcd30b-e10a-4b3b-8c49-464fcf93f3f2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1bd42f87-c2ed-4bbf-adf4-596ed8f41d1f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Keyboard"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4054ced4-adf6-498d-b15b-0131e2f58ba4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e7294542-a83c-4577-b0cb-6a424884b83a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a4536f0e-aee3-4478-9a33-b554a614efaf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8e170df3-ea95-436e-b6fa-cbca7c6e6c8d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""b9e82b93-d754-40bc-bbac-b91dd5139484"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Console"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fa912b16-33a6-4fe7-92fc-a253f6a7372d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""795ecf00-b621-456a-99c8-490fcdd03e03"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9d2ed8be-229b-4151-9ab3-b8b13a9eb1f1"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""864848a9-6af3-4059-9c98-6de4fe2f2456"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Player Camera"",
            ""id"": ""b099411e-4cfe-4c9d-baaa-b24517a006b6"",
            ""actions"": [
                {
                    ""name"": ""Camera Controls Mouse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1743c063-0f18-4f10-b243-2d0122444232"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera Controls Console"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7709449b-76a4-44e1-8bbd-edcf06ebe3ba"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee2f4cce-865a-4eab-8a16-937d94fa99ee"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Controls Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Right Stick"",
                    ""id"": ""13531da1-194a-470e-921b-f1cd678ed3d4"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Controls Console"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f3df9e12-760d-42c3-9e5d-2520b07ab750"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Controls Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""031db7fa-8924-4a6e-a197-8cf58706c589"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Controls Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""275204a6-5c06-4234-99be-5328a308d132"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Controls Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""616bbbb0-b1f4-440f-8d7a-bc34c81adee2"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Controls Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Player Actions"",
            ""id"": ""8352d3e0-7c8e-486f-8d4f-f31d53592bb1"",
            ""actions"": [
                {
                    ""name"": ""Dodge Keyboard"",
                    ""type"": ""Button"",
                    ""id"": ""57c1b455-4cc8-4396-8f31-fdc2f234930f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint Keyboard"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7a6b8623-1404-4452-b2ab-52ff021e44a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump Keyboard"",
                    ""type"": ""Button"",
                    ""id"": ""f42563f2-a3cd-47dc-97a2-c109509e2051"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dodge Console"",
                    ""type"": ""Button"",
                    ""id"": ""586d2cd3-d23a-41b4-be0c-0db035d614d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump Console"",
                    ""type"": ""Button"",
                    ""id"": ""e2d55adb-823e-4720-b21c-cce25f4d858a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint Console"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e970e13d-fcda-4b20-8fb5-b832891988e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ManaDrainTest"",
                    ""type"": ""Button"",
                    ""id"": ""433a2d22-4337-44d8-bc98-7fc62035b52b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LMB Attack Keyboard"",
                    ""type"": ""Button"",
                    ""id"": ""fb45628c-b1f9-4f7a-a8d3-aa6708150f76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RB Attack Console"",
                    ""type"": ""Button"",
                    ""id"": ""344c8c65-3622-47e4-9083-fa4af1800463"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""da3e9602-1432-4634-b6f4-55dfc3278ff2"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2ad8d41-c22a-4730-abd0-77d4cea83e97"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2fab4e3-8075-417c-a59f-6b37850eaf54"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4aaffb73-41f8-4acc-9c06-89f95245ce67"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManaDrainTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea5e97bb-7718-4857-95b7-d24a31533864"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eee061e7-1326-4e69-9d2c-412cc1668ecb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""522b2b2c-bd08-4df5-83b2-9ad792f716f7"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc9f0097-57e8-4be7-940f-4d05110cd15d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LMB Attack Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ecfbe6a-e8db-4723-9ed5-83a591e22dd9"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RB Attack Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""e6ba6b30-e90b-4a6e-b81b-3570a99ddea0"",
            ""actions"": [
                {
                    ""name"": ""MouseLeftButton"",
                    ""type"": ""Button"",
                    ""id"": ""5cd72e1b-cd34-4915-9822-7e174573fa16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""X Console"",
                    ""type"": ""Button"",
                    ""id"": ""fb96abb4-f7af-4c82-af05-fee6c5885d80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fbeb6463-b9c7-4457-8e1e-812ac1a43a81"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e70d739-52b3-4d9c-bdfd-a47b2a50e366"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_MovementKeyboard = m_PlayerMovement.FindAction("Movement Keyboard", throwIfNotFound: true);
        m_PlayerMovement_MovementConsole = m_PlayerMovement.FindAction("Movement Console", throwIfNotFound: true);
        // Player Camera
        m_PlayerCamera = asset.FindActionMap("Player Camera", throwIfNotFound: true);
        m_PlayerCamera_CameraControlsMouse = m_PlayerCamera.FindAction("Camera Controls Mouse", throwIfNotFound: true);
        m_PlayerCamera_CameraControlsConsole = m_PlayerCamera.FindAction("Camera Controls Console", throwIfNotFound: true);
        // Player Actions
        m_PlayerActions = asset.FindActionMap("Player Actions", throwIfNotFound: true);
        m_PlayerActions_DodgeKeyboard = m_PlayerActions.FindAction("Dodge Keyboard", throwIfNotFound: true);
        m_PlayerActions_SprintKeyboard = m_PlayerActions.FindAction("Sprint Keyboard", throwIfNotFound: true);
        m_PlayerActions_JumpKeyboard = m_PlayerActions.FindAction("Jump Keyboard", throwIfNotFound: true);
        m_PlayerActions_DodgeConsole = m_PlayerActions.FindAction("Dodge Console", throwIfNotFound: true);
        m_PlayerActions_JumpConsole = m_PlayerActions.FindAction("Jump Console", throwIfNotFound: true);
        m_PlayerActions_SprintConsole = m_PlayerActions.FindAction("Sprint Console", throwIfNotFound: true);
        m_PlayerActions_ManaDrainTest = m_PlayerActions.FindAction("ManaDrainTest", throwIfNotFound: true);
        m_PlayerActions_LMBAttackKeyboard = m_PlayerActions.FindAction("LMB Attack Keyboard", throwIfNotFound: true);
        m_PlayerActions_RBAttackConsole = m_PlayerActions.FindAction("RB Attack Console", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_MouseLeftButton = m_UI.FindAction("MouseLeftButton", throwIfNotFound: true);
        m_UI_XConsole = m_UI.FindAction("X Console", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private List<IPlayerMovementActions> m_PlayerMovementActionsCallbackInterfaces = new List<IPlayerMovementActions>();
    private readonly InputAction m_PlayerMovement_MovementKeyboard;
    private readonly InputAction m_PlayerMovement_MovementConsole;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementKeyboard => m_Wrapper.m_PlayerMovement_MovementKeyboard;
        public InputAction @MovementConsole => m_Wrapper.m_PlayerMovement_MovementConsole;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Add(instance);
            @MovementKeyboard.started += instance.OnMovementKeyboard;
            @MovementKeyboard.performed += instance.OnMovementKeyboard;
            @MovementKeyboard.canceled += instance.OnMovementKeyboard;
            @MovementConsole.started += instance.OnMovementConsole;
            @MovementConsole.performed += instance.OnMovementConsole;
            @MovementConsole.canceled += instance.OnMovementConsole;
        }

        private void UnregisterCallbacks(IPlayerMovementActions instance)
        {
            @MovementKeyboard.started -= instance.OnMovementKeyboard;
            @MovementKeyboard.performed -= instance.OnMovementKeyboard;
            @MovementKeyboard.canceled -= instance.OnMovementKeyboard;
            @MovementConsole.started -= instance.OnMovementConsole;
            @MovementConsole.performed -= instance.OnMovementConsole;
            @MovementConsole.canceled -= instance.OnMovementConsole;
        }

        public void RemoveCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Camera
    private readonly InputActionMap m_PlayerCamera;
    private List<IPlayerCameraActions> m_PlayerCameraActionsCallbackInterfaces = new List<IPlayerCameraActions>();
    private readonly InputAction m_PlayerCamera_CameraControlsMouse;
    private readonly InputAction m_PlayerCamera_CameraControlsConsole;
    public struct PlayerCameraActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerCameraActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraControlsMouse => m_Wrapper.m_PlayerCamera_CameraControlsMouse;
        public InputAction @CameraControlsConsole => m_Wrapper.m_PlayerCamera_CameraControlsConsole;
        public InputActionMap Get() { return m_Wrapper.m_PlayerCamera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerCameraActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerCameraActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerCameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerCameraActionsCallbackInterfaces.Add(instance);
            @CameraControlsMouse.started += instance.OnCameraControlsMouse;
            @CameraControlsMouse.performed += instance.OnCameraControlsMouse;
            @CameraControlsMouse.canceled += instance.OnCameraControlsMouse;
            @CameraControlsConsole.started += instance.OnCameraControlsConsole;
            @CameraControlsConsole.performed += instance.OnCameraControlsConsole;
            @CameraControlsConsole.canceled += instance.OnCameraControlsConsole;
        }

        private void UnregisterCallbacks(IPlayerCameraActions instance)
        {
            @CameraControlsMouse.started -= instance.OnCameraControlsMouse;
            @CameraControlsMouse.performed -= instance.OnCameraControlsMouse;
            @CameraControlsMouse.canceled -= instance.OnCameraControlsMouse;
            @CameraControlsConsole.started -= instance.OnCameraControlsConsole;
            @CameraControlsConsole.performed -= instance.OnCameraControlsConsole;
            @CameraControlsConsole.canceled -= instance.OnCameraControlsConsole;
        }

        public void RemoveCallbacks(IPlayerCameraActions instance)
        {
            if (m_Wrapper.m_PlayerCameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerCameraActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerCameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerCameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerCameraActions @PlayerCamera => new PlayerCameraActions(this);

    // Player Actions
    private readonly InputActionMap m_PlayerActions;
    private List<IPlayerActionsActions> m_PlayerActionsActionsCallbackInterfaces = new List<IPlayerActionsActions>();
    private readonly InputAction m_PlayerActions_DodgeKeyboard;
    private readonly InputAction m_PlayerActions_SprintKeyboard;
    private readonly InputAction m_PlayerActions_JumpKeyboard;
    private readonly InputAction m_PlayerActions_DodgeConsole;
    private readonly InputAction m_PlayerActions_JumpConsole;
    private readonly InputAction m_PlayerActions_SprintConsole;
    private readonly InputAction m_PlayerActions_ManaDrainTest;
    private readonly InputAction m_PlayerActions_LMBAttackKeyboard;
    private readonly InputAction m_PlayerActions_RBAttackConsole;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DodgeKeyboard => m_Wrapper.m_PlayerActions_DodgeKeyboard;
        public InputAction @SprintKeyboard => m_Wrapper.m_PlayerActions_SprintKeyboard;
        public InputAction @JumpKeyboard => m_Wrapper.m_PlayerActions_JumpKeyboard;
        public InputAction @DodgeConsole => m_Wrapper.m_PlayerActions_DodgeConsole;
        public InputAction @JumpConsole => m_Wrapper.m_PlayerActions_JumpConsole;
        public InputAction @SprintConsole => m_Wrapper.m_PlayerActions_SprintConsole;
        public InputAction @ManaDrainTest => m_Wrapper.m_PlayerActions_ManaDrainTest;
        public InputAction @LMBAttackKeyboard => m_Wrapper.m_PlayerActions_LMBAttackKeyboard;
        public InputAction @RBAttackConsole => m_Wrapper.m_PlayerActions_RBAttackConsole;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Add(instance);
            @DodgeKeyboard.started += instance.OnDodgeKeyboard;
            @DodgeKeyboard.performed += instance.OnDodgeKeyboard;
            @DodgeKeyboard.canceled += instance.OnDodgeKeyboard;
            @SprintKeyboard.started += instance.OnSprintKeyboard;
            @SprintKeyboard.performed += instance.OnSprintKeyboard;
            @SprintKeyboard.canceled += instance.OnSprintKeyboard;
            @JumpKeyboard.started += instance.OnJumpKeyboard;
            @JumpKeyboard.performed += instance.OnJumpKeyboard;
            @JumpKeyboard.canceled += instance.OnJumpKeyboard;
            @DodgeConsole.started += instance.OnDodgeConsole;
            @DodgeConsole.performed += instance.OnDodgeConsole;
            @DodgeConsole.canceled += instance.OnDodgeConsole;
            @JumpConsole.started += instance.OnJumpConsole;
            @JumpConsole.performed += instance.OnJumpConsole;
            @JumpConsole.canceled += instance.OnJumpConsole;
            @SprintConsole.started += instance.OnSprintConsole;
            @SprintConsole.performed += instance.OnSprintConsole;
            @SprintConsole.canceled += instance.OnSprintConsole;
            @ManaDrainTest.started += instance.OnManaDrainTest;
            @ManaDrainTest.performed += instance.OnManaDrainTest;
            @ManaDrainTest.canceled += instance.OnManaDrainTest;
            @LMBAttackKeyboard.started += instance.OnLMBAttackKeyboard;
            @LMBAttackKeyboard.performed += instance.OnLMBAttackKeyboard;
            @LMBAttackKeyboard.canceled += instance.OnLMBAttackKeyboard;
            @RBAttackConsole.started += instance.OnRBAttackConsole;
            @RBAttackConsole.performed += instance.OnRBAttackConsole;
            @RBAttackConsole.canceled += instance.OnRBAttackConsole;
        }

        private void UnregisterCallbacks(IPlayerActionsActions instance)
        {
            @DodgeKeyboard.started -= instance.OnDodgeKeyboard;
            @DodgeKeyboard.performed -= instance.OnDodgeKeyboard;
            @DodgeKeyboard.canceled -= instance.OnDodgeKeyboard;
            @SprintKeyboard.started -= instance.OnSprintKeyboard;
            @SprintKeyboard.performed -= instance.OnSprintKeyboard;
            @SprintKeyboard.canceled -= instance.OnSprintKeyboard;
            @JumpKeyboard.started -= instance.OnJumpKeyboard;
            @JumpKeyboard.performed -= instance.OnJumpKeyboard;
            @JumpKeyboard.canceled -= instance.OnJumpKeyboard;
            @DodgeConsole.started -= instance.OnDodgeConsole;
            @DodgeConsole.performed -= instance.OnDodgeConsole;
            @DodgeConsole.canceled -= instance.OnDodgeConsole;
            @JumpConsole.started -= instance.OnJumpConsole;
            @JumpConsole.performed -= instance.OnJumpConsole;
            @JumpConsole.canceled -= instance.OnJumpConsole;
            @SprintConsole.started -= instance.OnSprintConsole;
            @SprintConsole.performed -= instance.OnSprintConsole;
            @SprintConsole.canceled -= instance.OnSprintConsole;
            @ManaDrainTest.started -= instance.OnManaDrainTest;
            @ManaDrainTest.performed -= instance.OnManaDrainTest;
            @ManaDrainTest.canceled -= instance.OnManaDrainTest;
            @LMBAttackKeyboard.started -= instance.OnLMBAttackKeyboard;
            @LMBAttackKeyboard.performed -= instance.OnLMBAttackKeyboard;
            @LMBAttackKeyboard.canceled -= instance.OnLMBAttackKeyboard;
            @RBAttackConsole.started -= instance.OnRBAttackConsole;
            @RBAttackConsole.performed -= instance.OnRBAttackConsole;
            @RBAttackConsole.canceled -= instance.OnRBAttackConsole;
        }

        public void RemoveCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_MouseLeftButton;
    private readonly InputAction m_UI_XConsole;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLeftButton => m_Wrapper.m_UI_MouseLeftButton;
        public InputAction @XConsole => m_Wrapper.m_UI_XConsole;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @MouseLeftButton.started += instance.OnMouseLeftButton;
            @MouseLeftButton.performed += instance.OnMouseLeftButton;
            @MouseLeftButton.canceled += instance.OnMouseLeftButton;
            @XConsole.started += instance.OnXConsole;
            @XConsole.performed += instance.OnXConsole;
            @XConsole.canceled += instance.OnXConsole;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @MouseLeftButton.started -= instance.OnMouseLeftButton;
            @MouseLeftButton.performed -= instance.OnMouseLeftButton;
            @MouseLeftButton.canceled -= instance.OnMouseLeftButton;
            @XConsole.started -= instance.OnXConsole;
            @XConsole.performed -= instance.OnXConsole;
            @XConsole.canceled -= instance.OnXConsole;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovementKeyboard(InputAction.CallbackContext context);
        void OnMovementConsole(InputAction.CallbackContext context);
    }
    public interface IPlayerCameraActions
    {
        void OnCameraControlsMouse(InputAction.CallbackContext context);
        void OnCameraControlsConsole(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnDodgeKeyboard(InputAction.CallbackContext context);
        void OnSprintKeyboard(InputAction.CallbackContext context);
        void OnJumpKeyboard(InputAction.CallbackContext context);
        void OnDodgeConsole(InputAction.CallbackContext context);
        void OnJumpConsole(InputAction.CallbackContext context);
        void OnSprintConsole(InputAction.CallbackContext context);
        void OnManaDrainTest(InputAction.CallbackContext context);
        void OnLMBAttackKeyboard(InputAction.CallbackContext context);
        void OnRBAttackConsole(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMouseLeftButton(InputAction.CallbackContext context);
        void OnXConsole(InputAction.CallbackContext context);
    }
}
