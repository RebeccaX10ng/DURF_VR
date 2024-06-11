using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRMovementRecorder : MonoBehaviour
{
    public float recordingDuration = 5.0f;
    public GameObject replicaPrefab; // Prefab for the replica
    public GameObject xrRig; // Reference to the XR Rig
    public ContinuousMoveProviderBase moveProvider; // Reference to the Continuous Move Provider

    private List<Vector3> recordedPositions = new List<Vector3>();
    private List<Quaternion> recordedRotations = new List<Quaternion>();
    private bool isRecording = false;
    private bool isReplaying = false;
    private GameObject replica;
    private XRInputActions inputActions;

    private void Awake()
    {
        inputActions = new XRInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Record.performed += OnRecord;
        inputActions.Player.Replay.performed += OnReplay;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Record.performed -= OnRecord;
        inputActions.Player.Replay.performed -= OnReplay;
        inputActions.Disable();
    }

    private void OnRecord(InputAction.CallbackContext context)
    {
        if (!isRecording)
        {
            StartCoroutine(RecordMovement());
        }
    }

    private void OnReplay(InputAction.CallbackContext context)
    {
        if (!isReplaying)
        {
            StartCoroutine(ReplayMovement());
        }
    }

    IEnumerator RecordMovement()
    {
        recordedPositions.Clear();
        recordedRotations.Clear();
        isRecording = true;
        Debug.Log("Start Recording");
        float timer = 0;
        while (timer < recordingDuration)
        {
            recordedPositions.Add(xrRig.transform.position);
            recordedRotations.Add(xrRig.transform.rotation);
            timer += Time.deltaTime;
            yield return null;
        }

        isRecording = false;
        Debug.Log("Finish Recording");
    }

    IEnumerator ReplayMovement()
    {
        if (isRecording || recordedPositions.Count == 0)
            yield break;

        if (replica != null)
        {
            Destroy(replica);
        }

        replica = Instantiate(replicaPrefab, recordedPositions[0], recordedRotations[0]);
        isReplaying = true;
        Debug.Log("Start Replaying");
        // Disable movement during replay
        moveProvider.enabled = false;

        for (int i = 0; i < recordedPositions.Count; i++)
        {
            replica.transform.position = recordedPositions[i];
            replica.transform.rotation = recordedRotations[i];
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isReplaying = false;
        Debug.Log("Finish Recording");
        // Enable movement after replay
        moveProvider.enabled = true;
    }
}
