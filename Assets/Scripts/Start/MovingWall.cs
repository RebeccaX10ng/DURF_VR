using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall: MonoBehaviour
{
    public GameObject wall;
    void Start()
    {
        StartCoroutine(MovingObjects());
    }
    
    void Update()
    {
        
    }

    private IEnumerator MovingObjects()
    {
        //wall movement and sound effect
        Vector3 startPosition = wall.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 3, startPosition.z);
        
        float elapsedTime = 0;
        while (elapsedTime < 2f)
        {
            wall.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        wall.transform.position = endPosition;
        Debug.Log("Wall Movement complete.");
        yield return new WaitForSeconds(1f);
    }
}
