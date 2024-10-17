//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.0
//     from Assets/XRInputActions.inputactions
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

public partial class @XRInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @XRInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""XRInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f7ec2f72-728e-4753-ac31-1cee0c642963"",
            ""actions"": [
                {
                    ""name"": ""Record"",
                    ""type"": ""Button"",
                    ""id"": ""b240a329-7e8c-47f3-b6c0-9c8704dc8540"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Replay"",
                    ""type"": ""Button"",
                    ""id"": ""d6c73b12-1ffe-4814-ab20-771f87a46fc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlaceInPortal"",
                    ""type"": ""Button"",
                    ""id"": ""004368b8-64d6-4ef0-8fa4-f5c584b2144b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlaceOutPortal"",
                    ""type"": ""Button"",
                    ""id"": ""f67eb726-33b1-4067-94cb-723253a2ec0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d5110a94-a130-4603-8ebb-fa3f0e5e0a96"",
                    ""path"": ""<OculusTouchController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Record"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d926142a-a8bf-4437-a45b-a280354d4e67"",
                    ""path"": ""<OculusTouchController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Replay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a82f9f9-f4b5-4523-bf13-75fa31df7717"",
                    ""path"": ""<XRController>{LeftHand}/{PrimaryButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceInPortal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b97c1c3c-84a9-4aeb-b018-2c2308e0844d"",
                    ""path"": ""<XRController>{LeftHand}/{SecondaryButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceOutPortal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Record = m_Player.FindAction("Record", throwIfNotFound: true);
        m_Player_Replay = m_Player.FindAction("Replay", throwIfNotFound: true);
        m_Player_PlaceInPortal = m_Player.FindAction("PlaceInPortal", throwIfNotFound: true);
        m_Player_PlaceOutPortal = m_Player.FindAction("PlaceOutPortal", throwIfNotFound: true);
    }

    ~@XRInputActions()
    {
        UnityEngine.Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, XRInputActions.Player.Disable() has not been called.");
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Record;
    private readonly InputAction m_Player_Replay;
    private readonly InputAction m_Player_PlaceInPortal;
    private readonly InputAction m_Player_PlaceOutPortal;
    public struct PlayerActions
    {
        private @XRInputActions m_Wrapper;
        public PlayerActions(@XRInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Record => m_Wrapper.m_Player_Record;
        public InputAction @Replay => m_Wrapper.m_Player_Replay;
        public InputAction @PlaceInPortal => m_Wrapper.m_Player_PlaceInPortal;
        public InputAction @PlaceOutPortal => m_Wrapper.m_Player_PlaceOutPortal;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Record.started += instance.OnRecord;
            @Record.performed += instance.OnRecord;
            @Record.canceled += instance.OnRecord;
            @Replay.started += instance.OnReplay;
            @Replay.performed += instance.OnReplay;
            @Replay.canceled += instance.OnReplay;
            @PlaceInPortal.started += instance.OnPlaceInPortal;
            @PlaceInPortal.performed += instance.OnPlaceInPortal;
            @PlaceInPortal.canceled += instance.OnPlaceInPortal;
            @PlaceOutPortal.started += instance.OnPlaceOutPortal;
            @PlaceOutPortal.performed += instance.OnPlaceOutPortal;
            @PlaceOutPortal.canceled += instance.OnPlaceOutPortal;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Record.started -= instance.OnRecord;
            @Record.performed -= instance.OnRecord;
            @Record.canceled -= instance.OnRecord;
            @Replay.started -= instance.OnReplay;
            @Replay.performed -= instance.OnReplay;
            @Replay.canceled -= instance.OnReplay;
            @PlaceInPortal.started -= instance.OnPlaceInPortal;
            @PlaceInPortal.performed -= instance.OnPlaceInPortal;
            @PlaceInPortal.canceled -= instance.OnPlaceInPortal;
            @PlaceOutPortal.started -= instance.OnPlaceOutPortal;
            @PlaceOutPortal.performed -= instance.OnPlaceOutPortal;
            @PlaceOutPortal.canceled -= instance.OnPlaceOutPortal;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnRecord(InputAction.CallbackContext context);
        void OnReplay(InputAction.CallbackContext context);
        void OnPlaceInPortal(InputAction.CallbackContext context);
        void OnPlaceOutPortal(InputAction.CallbackContext context);
    }
}
