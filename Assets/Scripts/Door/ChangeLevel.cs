using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WaitForSeconds = UnityEngine.WaitForSeconds;

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

        yield return new WaitForSeconds(1f);

        foreach (GameObject obj in objectsToMove)
        {
            StartCoroutine(MoveObject(obj));
        }

        audioSource1.Play();
        audioSource2.Play();

    }
    private IEnumerator MoveObject(GameObject obj){
        
        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveAmount;

        while (elapsedTime < moveDuration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = endPosition; // 确保最终位置准确
        
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
    }
}
