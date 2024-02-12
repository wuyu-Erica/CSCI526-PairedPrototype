using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivation : MonoBehaviour
{
    public GameObject[] targetGrounds; // 现在是一个数组
    public int maxTriggerCount = 1; // 最大触发次数限制
    private int currentTriggerCount = 0; // 当前触发次数

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentTriggerCount < maxTriggerCount)
        {
            currentTriggerCount++;
            foreach (var targetGround in targetGrounds)
            {
                // 仅禁用可视化组件，而不是整个GameObject
                SetVisibility(targetGround, true);
            }
            StartCoroutine(HideGroundAfterDelay(5f));
        }
    }

    private IEnumerator HideGroundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var targetGround in targetGrounds)
        {
            // 仅禁用可视化组件，而不是整个GameObject
            SetVisibility(targetGround, false);
        }
        // 可以选择是否重置触发次数
    }

    // 新方法：设置地面的可视化组件的可见性
    void SetVisibility(GameObject ground, bool isVisible)
    {
        var renderers = ground.GetComponentsInChildren<Renderer>(); // 获取所有渲染器组件，包括子对象
        foreach (var renderer in renderers)
        {
            renderer.enabled = isVisible; // 设置渲染器的可见性
        }
    }
}
