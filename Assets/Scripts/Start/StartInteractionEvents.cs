using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInteractionEvents : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject wall;
    
    public AudioSource audioSourceWall;
    public AudioSource audioSourceNarration;
    public AudioClip wallMoveClip;
    public AudioClip voiceOver1;
    public AudioClip voiceOver2;
    private bool isActivated = false;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (targetObject.activeSelf && !isActivated)
        {
            isActivated = true;
            StartCoroutine(Narration());
            StartCoroutine(Objects());
        }
    }
    
    private IEnumerator Narration()
    {
        Debug.Log("Target object activated, starting follow-up events.");
        
        //event1-narration
        audioSourceNarration.PlayOneShot(voiceOver1);
        yield return new WaitForSeconds(voiceOver1.length + 1f);
        
        audioSourceNarration.PlayOneShot(voiceOver2);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Objects()
    {
        //event1-wall movement and sound effect
        Vector3 startPosition = wall.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 3, startPosition.z);
        
        audioSourceWall.PlayOneShot(wallMoveClip);
        
        float elapsedTime = 0;
        while (elapsedTime < 2f)
        {
            wall.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        wall.transform.position = endPosition;
        Debug.Log("Movement complete.");
        yield return new WaitForSeconds(1f);
        
        //event2-glass trigger
    }
}
