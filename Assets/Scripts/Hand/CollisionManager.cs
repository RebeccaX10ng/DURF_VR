using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public CollisionChecker[] objectsToCheck; // 四个需要检测的物体
    public SpecialCollisionChecker specialObject; // 需要检测两个碰撞的特殊物体

    private void Update()
    {
        CheckAllCollisions();
    }

    private void CheckAllCollisions()
    {
        // 检查四个普通物体是否与Block碰撞
        foreach (var checker in objectsToCheck)
        {
            if (!checker.IsCollidingWithBlock)
            {
                return; // 如果有任何一个物体没有碰撞到Block，则退出
            }
        }

        // 检查特殊物体是否与至少两个Block碰撞
        //if (specialObject.IsCollidingWithTwoBlocks)
        {
          //  Debug.Log("All conditions met!");
        }
    }
}