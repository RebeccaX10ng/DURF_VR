using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CameraMove))]
public class PortalPlacementXR : MonoBehaviour
{
    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private PortalPreviewManager portalPreviewManager;

    [SerializeField]
    private InputActionReference placeInPortalAction;

    [SerializeField]
    private InputActionReference placeOutPortalAction;

    private CameraMove cameraMove;

    private void Awake()
    {
        cameraMove = GetComponent<CameraMove>();

        // Subscribe to the input actions
        placeInPortalAction.action.performed += ctx => OnPlaceInPortal();
        placeOutPortalAction.action.performed += ctx => OnPlaceOutPortal();
    }

    private void OnEnable()
    {
        // Enable the input actions
        placeInPortalAction.action.Enable();
        placeOutPortalAction.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        placeInPortalAction.action.Disable();
        placeOutPortalAction.action.Disable();
    }

    private void OnPlaceInPortal()
    {
        if (portalPreviewManager.IsPreviewActive)
        {
            FirePortal(0);
        }
    }

    private void OnPlaceOutPortal()
    {
        if (portalPreviewManager.IsPreviewActive)
        {
            FirePortal(1);
        }
    }

    private void FirePortal(int portalID)
    {
        Vector3 pos = portalPreviewManager.GetPreviewPosition();
        Quaternion dir = portalPreviewManager.GetPreviewRotation();

        RaycastHit hit;
        if (Physics.Raycast(pos, dir * Vector3.forward, out hit, 250.0f))
        {
            // Orient the portal according to the surface direction
            Vector3 portalForward = -hit.normal;
            Vector3 portalRight = Vector3.Cross(Vector3.up, portalForward).normalized;
            Vector3 portalUp = Vector3.Cross(portalForward, portalRight);
            Quaternion portalRotation = Quaternion.LookRotation(portalForward, portalUp);

            portals.Portals[portalID].PlacePortal(null, hit.point, portalRotation);
        }
        else
        {
            // If no surface is hit, place the portal in space in the direction of the controller
            Vector3 portalPosition = pos + dir * Vector3.forward * 250.0f;
            Quaternion portalRotation = Quaternion.LookRotation(dir * Vector3.forward, Vector3.up);

            portals.Portals[portalID].PlacePortal(null, portalPosition, portalRotation);
        }

        portalPreviewManager.SetPreviewActive(false);
    }
}
