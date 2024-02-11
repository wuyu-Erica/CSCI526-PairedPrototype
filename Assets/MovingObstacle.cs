using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveSpeed = 2f; // 障碍物的移动速度
    public float moveDistance = 3f; // 障碍物左右移动的最大距离
    private Vector3 startPos; // 障碍物的起始位置
    private bool movingRight = true; // 判断障碍物当前移动方向

    void Start()
    {
        startPos = transform.position; // 初始化起始位置
    }

    void Update()
    {
        // 根据movingRight标志，在两个方向之间切换移动障碍物
        if (movingRight)
        {
            if (transform.position.x < startPos.x + moveDistance)
            {
                // 向右移动
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingRight = false; // 到达右侧极限，改变方向
            }
        }
        else
        {
            if (transform.position.x > startPos.x - moveDistance)
            {
                // 向左移动
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingRight = true; // 到达左侧极限，改变方向
            }
        }
    }

    // 在MovingObstacle脚本中
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ObjBehaviourScript playerScript = other.GetComponent<ObjBehaviourScript>();
            if (playerScript != null)
            {
                // 使用public方法来启动协程
                playerScript.StartInvertControlsTemporary(5f);
            }
            gameObject.SetActive(false); // 使消失
        }
    }

}


