using System.Collections.Generic;
using UnityEngine;

public class ChainMaterialChanger : MonoBehaviour
{
    public Material newMaterial; // 新的材质
    public List<GameObject> objectsToCheck; // 要检测的其他物体列表
    private bool isHandTouching = false; // 检查手是否在触摸

    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>(); // 原始材质的字典
    private HashSet<GameObject> affectedObjects = new HashSet<GameObject>(); // 已经改变材质的物体

    private void Start()
    {
        // 保存每个物体的原始材质
        foreach (var obj in objectsToCheck)
        {
            if (obj != null && obj.GetComponent<Renderer>() != null)
            {
                originalMaterials[obj] = obj.GetComponent<Renderer>().material;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            // 当手触碰到第一个物体时，开始改变材质
            isHandTouching = true;
            ChangeMaterial(gameObject, newMaterial);
            CheckAndChangeConnectedMaterials(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            // 当手离开时，恢复所有物体的原始材质
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
            affectedObjects.Add(obj); // 将已改变材质的物体加入集合
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
                    renderer.material = originalMaterials[obj]; // 还原为各自的原始材质
                }
            }
        }
        affectedObjects.Clear(); // 清空集合
    }

    private void CheckAndChangeConnectedMaterials(GameObject currentObj)
    {
        // 检查当前物体是否与列表中的其他物体有碰撞
        foreach (var obj in objectsToCheck)
        {
            if (obj != null && obj != currentObj && !affectedObjects.Contains(obj))
            {
                if (obj.GetComponent<Collider>().bounds.Intersects(currentObj.GetComponent<Collider>().bounds))
                {
                    ChangeMaterial(obj, newMaterial);
                    // 递归检查连接的物体
                    CheckAndChangeConnectedMaterials(obj);
                }
            }
        }
    }
}
