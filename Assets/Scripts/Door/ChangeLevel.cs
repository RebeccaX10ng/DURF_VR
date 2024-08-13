using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeLevel : MonoBehaviour
{
    public GameObject objectToActivate; // 触发时激活的物体
    public List<GameObject> objectsToMove; // 要移动的物体列表
    public float moveAmount = 1f; // 移动的单位
    public float moveDuration = 2f; // 移动的持续时间
    
    public GameObject objectToDisable; // 最后禁用的物体
    public AudioSource audioSource1; // 激活的第一个音频源
    public AudioSource audioSource2; // 激活的第二个音频源

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleTrigger());
        }
    }

    private IEnumerator HandleTrigger()
    {
        // 激活物体
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        
        // 移动物体
        float elapsedTime = 0f;
        Vector3 startPosition;
        Vector3 endPosition;

        foreach (GameObject obj in objectsToMove)
        {
            startPosition = obj.transform.position;
            endPosition = startPosition + Vector3.up * moveAmount;

            while (elapsedTime < moveDuration)
            {
                obj.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            obj.transform.position = endPosition; // 确保最终位置准确
            elapsedTime = 0f; // 重置时间
        }

        // 禁用物体
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        
        audioSource1.Play();
        audioSource2.Play();
       
    }
}
