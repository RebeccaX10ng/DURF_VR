using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBall : MonoBehaviour
{
    public AudioClip appearSound;
    private AudioSource audioSource;
    private Transform[] balls;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = appearSound;
        
        balls = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            balls[i] = transform.GetChild(i);
            balls[i].gameObject.SetActive(false); 
        }
        
        StartCoroutine(ShowBallsWithInterval());
    }

    private IEnumerator ShowBallsWithInterval()
    {
        yield return new WaitForSeconds(5); 

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].gameObject.SetActive(true); 
            audioSource.Play(); 
            yield return new WaitForSeconds(0.6f); 
            balls[i].gameObject.SetActive(false); 
        }

        yield return new WaitForSeconds(5); 

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].gameObject.SetActive(true); 
            audioSource.Play();
            yield return new WaitForSeconds(0.6f); 
            balls[i].gameObject.SetActive(false); 
        }
    }
}
