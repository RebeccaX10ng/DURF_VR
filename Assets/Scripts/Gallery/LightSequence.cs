using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightSequence : MonoBehaviour
{
    public List<GameObject> lights; 
    public List<GameObject> clickableObjects; 
    public List<GameObject> materialChangeObjects;
    public float lightOnDuration = 1.0f; 
    public float delayBetweenLights = 0.5f; 
    public float initialDelay = 2.0f; 
    
    private int currentIndex = 0; 
    private bool isCorrectSequence = true;
    private bool hasEntered = false;
    private bool isSecondRound = false;
    private bool isThirdRound = false;

    public AudioSource soundEffectAudioSource;
    public AudioClip success;
    public AudioClip failure;
    public GameObject door;
    public GameObject tag; 
    public GameObject wall;
    
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();

    void Start()
    {
        if (lights.Count != clickableObjects.Count)
        {
            Debug.LogError("Lights and Clickable Objects lists must have the same length.");
            return;
        }
        
        foreach (var obj in materialChangeObjects)
        {
            SaveOriginalMaterials(obj);
        }
        EnableInteraction(false); 
    }
    
    private void SaveOriginalMaterials(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterials[obj] = renderer.material;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            StartCoroutine(InitialDelayAndPlaySequence());
            hasEntered = true;
        }
    }
    
    IEnumerator InitialDelayAndPlaySequence()
    {
        yield return new WaitForSeconds(4f); 
        StartCoroutine(PlayLightSequence());
    }
    
    IEnumerator PlayLightSequence()
    {
        RestoreOriginalMaterials();
        yield return new WaitForSeconds(initialDelay);
        
        foreach (var light in lights)
        {
            light.SetActive(true);
            yield return new WaitForSeconds(lightOnDuration);
            light.SetActive(false);
            yield return new WaitForSeconds(delayBetweenLights);
        }
        
        isCorrectSequence = true;
        currentIndex = 0;
        EnableInteraction(true); 
    }

    public void OnObjectClicked(GameObject clickedObject)
    {
        if (isCorrectSequence && clickedObject == clickableObjects[currentIndex])
        {
            currentIndex++;
            if (currentIndex == clickableObjects.Count)
            {
                Debug.Log("Success! Player clicked the correct sequence.");
                EnableInteraction(false);
                soundEffectAudioSource.PlayOneShot(success);
                if (!isSecondRound)
                {
                    ActivateObjectAndStartSecondRound();
                }
                else if (!isThirdRound)
                {
                    ActivateObjectAndStartThirdRound();
                }
                else
                {
                    Debug.Log("Third round complete!");
                    wall.SetActive(false);
                }
            }
        }
        else
        {
            isCorrectSequence = false;
            EnableInteraction(false);
            Debug.Log("Failure! Player clicked the wrong object.");
            soundEffectAudioSource.PlayOneShot(failure);
            RestoreOriginalMaterials();
            StartCoroutine(PlayLightSequence());
        }
    }
    
    void ActivateObjectAndStartSecondRound()
    {
        door.SetActive(true);
        SwapObjectsAndLightsRound1();
        isSecondRound = true;
        StartCoroutine(PlayLightSequence());
    }
    
    void ActivateObjectAndStartThirdRound()
    {
        tag.SetActive(true);
        SwapObjectsAndLightsRound2();
        isThirdRound = true;
        StartCoroutine(PlayLightSequence());
    }
    
    void SwapObjectsAndLightsRound1()
    {
        Swap(0, 2);
        Swap(1, 3);
    }

    void SwapObjectsAndLightsRound2()
    {
        Swap(0, 3);
        Swap(1, 2);
    }

    void Swap(int indexA, int indexB)
    {
        var tempLight = lights[indexA];
        lights[indexA] = lights[indexB];
        lights[indexB] = tempLight;
        
        var tempObject = clickableObjects[indexA];
        clickableObjects[indexA] = clickableObjects[indexB];
        clickableObjects[indexB] = tempObject;
    }

    void EnableInteraction(bool enable)
    {
        foreach (var obj in clickableObjects)
        {
            var interactionScript = obj.GetComponent<XRGrabInteractable>();
            if (interactionScript != null)
            {
                interactionScript.enabled = enable;
            }
        }
    }
    
    void RestoreOriginalMaterials()
    {
        foreach (var kvp in originalMaterials)
        {
            var renderer = kvp.Key.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = kvp.Value;
            }
        }
    }
}
