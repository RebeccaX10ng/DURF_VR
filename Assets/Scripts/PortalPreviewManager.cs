using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PortalPreviewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPreview;

    [SerializeField]
    private float maxDistance = 10.0f;

    [SerializeField]
    private InputActionReference leftTriggerAction;
    
    [SerializeField]
    private InputActionReference rightTriggerAction;

    private bool isLeftTriggerPressed = false;
    private bool isRightTriggerPressed = false;

    public bool IsPreviewActive { get; private set; } = false;

    private void OnEnable()
    {
        leftTriggerAction.action.Enable();
        rightTriggerAction.action.Enable();
    }

    private void OnDisable()
    {
        leftTriggerAction.action.Disable();
        rightTriggerAction.action.Disable();
    }

    private void Update()
    {
        isLeftTriggerPressed = leftTriggerAction.action.ReadValue<float>() > 0.1f;
        isRightTriggerPressed = rightTriggerAction.action.ReadValue<float>() > 0.1f;

        if (isLeftTriggerPressed)
        {
            UpdatePortalPreview(Camera.main.transform);
            portalPreview.SetActive(true);
            IsPreviewActive = true;
        }
        else if (isRightTriggerPressed)
        {
            UpdatePortalPreview(Camera.main.transform);
            portalPreview.SetActive(true);
            IsPreviewActive = true;
        }
        else
        {
            portalPreview.SetActive(false);
            IsPreviewActive = false;
        }
    }

    private void UpdatePortalPreview(Transform controller)
    {
        RaycastHit hit;
        Vector3 position;
        Quaternion rotation;

        if (Physics.Raycast(controller.position, controller.forward, out hit, maxDistance))
        {
            position = hit.point;
            rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            position = controller.position + controller.forward * maxDistance;
            rotation = Quaternion.LookRotation(controller.forward);
        }

        portalPreview.transform.position = position;
        portalPreview.transform.rotation = rotation;
    }

    public void SetPreviewActive(bool isActive)
    {
        portalPreview.SetActive(isActive);
        IsPreviewActive = isActive;
    }

    public Vector3 GetPreviewPosition()
    {
        return portalPreview.transform.position;
    }

    public Quaternion GetPreviewRotation()
    {
        return portalPreview.transform.rotation;
    }
}
