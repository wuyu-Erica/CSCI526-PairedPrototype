using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public SwitchActivation switchScript; // 引用开关脚本

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switchScript.maxTriggerCount++; // 增加开灯次数
            gameObject.SetActive(false); // 使金币消失
        }
    }
}

