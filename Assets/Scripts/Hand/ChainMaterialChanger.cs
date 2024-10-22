using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ChainMaterialChanger : MonoBehaviour
{
    public Material newMaterial; 
    public List<GameObject> objectsToCheck; 
    public GameObject independentObject; 
    public Light independentLight;
    public GameObject teleportTrigger;
    
    private bool isHandTouching = false; 

    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>(); 
    private HashSet<GameObject> affectedObjects = new HashSet<GameObject>(); 

    private void Start()
    {
        foreach (var obj in objectsToCheck)
        {
            if (obj != null && obj.GetComponent<Renderer>() != null)
            {
                originalMaterials[obj] = obj.GetComponent<Renderer>().material;
            }
        }
    }
    
    private void Update()
    {
        if (independentObject != null)
        {
            independentObject.transform.Rotate(Vector3.up, 50 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isHandTouching = true;
            ChangeMaterial(gameObject, newMaterial);
            CheckAndChangeConnectedMaterials(gameObject);
            
            if (AllObjectsAffected())
            {
                teleportTrigger.SetActive(true);
                StartCoroutine(IncreaseLightIntensity(independentLight, 10000f, 2.5f));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isHandTouching = false;
            RevertMaterials();
        }
    }

    private void ChangeMaterial(GameObject obj, Material material)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
            affectedObjects.Add(obj); 
        }
    }

    private void RevertMaterials()
    {
        foreach (var obj in affectedObjects)
        {
            if (obj != null && originalMaterials.ContainsKey(obj))
            {
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = originalMaterials[obj]; 
                }
            }
        }
        affectedObjects.Clear();
    }

    private void CheckAndChangeConnectedMaterials(GameObject currentObj)
    {
        foreach (var obj in objectsToCheck)
        {
            if (obj != null && obj != currentObj && !affectedObjects.Contains(obj))
            {
                if (obj.GetComponent<Collider>().bounds.Intersects(currentObj.GetComponent<Collider>().bounds))
                {
                    ChangeMaterial(obj, newMaterial);
                    CheckAndChangeConnectedMaterials(obj);
                }
            }
        }
    }
    
    private bool AllObjectsAffected()
    {
        foreach (var obj in objectsToCheck)
        {
            if (!affectedObjects.Contains(obj))
            {
                return false;
            }
        }
        return true;
    }

    private System.Collections.IEnumerator IncreaseLightIntensity(Light light, float targetIntensity, float duration)
    {
        if (light != null)
        {
            float startIntensity = light.intensity;
            float elapsedTime = 0f;
            
            while (elapsedTime < duration)
            {
                light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            light.intensity = targetIntensity;

            yield return new WaitForSeconds(2f);
        }
    }
}
