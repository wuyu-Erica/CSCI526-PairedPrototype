using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ObjBehaviourScript : MonoBehaviour
{
    public float speed = 5f; // 控制左右移动的速度
    public float jumpForce = 12f; // 控制跳跃的力度
    private Rigidbody2D rb; // 对象的Rigidbody2D组件
    private bool isGrounded; // 检测对象是否站在地面上
    private bool controlsInverted = false; // 控制是否颠倒

    public Transform groundCheck; // 地面检测点
    public float checkRadius = 0.5f; // 检测半径
    public LayerMask groundLayer; // 地面图层

    public Image controlInvertProgressBar; // 控制反转进度条


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * (controlsInverted ? -1 : 1);
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Obstacle"))
    //    {
    //        StartCoroutine(InvertControlsTemporary(5f)); // 假设控制颠倒持续5秒
    //    }
    //}

    //IEnumerator InvertControlsTemporary(float duration)  //没有进度条功能
    //{
    //    controlsInverted = !controlsInverted;
    //    yield return new WaitForSeconds(duration);
    //    controlsInverted = !controlsInverted;
    //}

    IEnumerator InvertControlsTemporary(float duration)
    {
        controlsInverted = true;
        Debug.Log("Controls inverted.");
        // 显示进度条并初始化为满
        if (controlInvertProgressBar != null)
        {
            controlInvertProgressBar.gameObject.SetActive(true);
            controlInvertProgressBar.fillAmount = 1.0f;
            Debug.Log("Progress bar shown.");
        }
        else
        {
            Debug.Log("Progress bar reference is null.");
        }

        float timePassed = 0;
        while (timePassed < duration)
        {
            // 更新已过时间
            timePassed += Time.deltaTime;
            // 根据已过时间更新进度条
            if (controlInvertProgressBar != null)
            {
                controlInvertProgressBar.fillAmount = 1.0f - (timePassed / duration);
                Debug.Log("Progress bar updated: " + controlInvertProgressBar.fillAmount.ToString());
            }
            yield return null;
        }

        controlsInverted = false;
        Debug.Log("Controls reverted to normal.");

        // 隐藏进度条
        if (controlInvertProgressBar != null)
        {
            controlInvertProgressBar.gameObject.SetActive(false);
            Debug.Log("Progress bar hidden.");
        }
    }



    // 这个方法是public的，允许外部调用以启动控制颠倒的协程
    public void StartInvertControlsTemporary(float duration)
    {
        StartCoroutine(InvertControlsTemporary(duration));
    }


    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
