// GENERATED AUTOMATICALLY FROM 'Assets/DefaultInput.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class DefaultInput : InputActionAssetReference
{
    public DefaultInput()
    {
    }
    public DefaultInput(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // PlayerAction
        m_PlayerAction = asset.GetActionMap("PlayerAction");
        m_PlayerAction_Movement = m_PlayerAction.GetAction("Movement");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_PlayerAction = null;
        m_PlayerAction_Movement = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // PlayerAction
    private InputActionMap m_PlayerAction;
    private InputAction m_PlayerAction_Movement;
    public struct PlayerActionActions
    {
        private DefaultInput m_Wrapper;
        public PlayerActionActions(DefaultInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_PlayerAction_Movement; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
    }
    public PlayerActionActions @PlayerAction
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerActionActions(this);
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get

        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
}
