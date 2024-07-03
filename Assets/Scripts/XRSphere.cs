using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSphere : XRBaseInteractable
{
    [Serializable]
    [Tooltip("Event called when the value of the sphere is changed")]
    public class RotationChangeEvent : UnityEvent<Quaternion> { }

    [SerializeField]
    [Tooltip("The object that is visually grabbed and manipulated")]
    Transform m_Handle = null;

    [SerializeField]
    [Tooltip("Events to trigger when the sphere is rotated")]
    RotationChangeEvent m_OnRotationChange = new RotationChangeEvent();

    IXRSelectInteractor m_Interactor;

    Quaternion m_BaseRotation;
    Quaternion m_CurrentRotation;

    public Transform handle
    {
        get => m_Handle;
        set => m_Handle = value;
    }

    public RotationChangeEvent onRotationChange => m_OnRotationChange;

    void Start()
    {
        if (m_Handle == null)
            m_Handle = transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(StartGrab);
        selectExited.AddListener(EndGrab);
    }

    protected override void OnDisable()
    {
        selectEntered.RemoveListener(StartGrab);
        selectExited.RemoveListener(EndGrab);
        base.OnDisable();
    }

    void StartGrab(SelectEnterEventArgs args)
    {
        m_Interactor = args.interactorObject;
        m_BaseRotation = m_Handle.rotation;
    }

    void EndGrab(SelectExitEventArgs args)
    {
        m_Interactor = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                UpdateRotation();
            }
        }
    }

    void UpdateRotation()
    {
        if (m_Interactor != null)
        {
            // Get the interactor's rotation
            Quaternion interactorRotation = m_Interactor.GetAttachTransform(this).rotation;

            // Calculate the new rotation
            Quaternion deltaRotation = Quaternion.Inverse(m_BaseRotation) * interactorRotation;
            m_CurrentRotation = m_BaseRotation * deltaRotation;

            // Apply the rotation to the handle
            m_Handle.rotation = m_CurrentRotation;

            // Trigger the rotation change event
            m_OnRotationChange.Invoke(m_CurrentRotation);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (m_Handle != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(m_Handle.position, 0.1f);
        }
    }

    void OnValidate()
    {
        if (m_Handle == null)
            m_Handle = transform;
    }
}