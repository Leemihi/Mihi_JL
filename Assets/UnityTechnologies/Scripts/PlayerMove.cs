using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 movement;
    private Animator animator;
    private Rigidbody rb;
    private Quaternion rotation = Quaternion.identity;

    // HeaderAttribute : ������ ������ ���� �� ���
    [HeaderAttribute("ȸ�� �ӵ�")]
    public float turnSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        // Getcomponent : ���۳�Ʈ�� �ҷ���
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // ������, ������ �Է�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ���� �Է�
        movement.Set(horizontal, 0f, vertical);
       
        // ����ȭ
        movement.Normalize();

        // Mathf.Approximately : �ٻ簪 (�Է°�, ���ϴ� ��)
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("IsWalking", isWalking);

        // desiredFoward : �ٶ� ���� ���
        Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);
        // Quaternion.LookRotation : ���͹������� �ٶ�
        rotation = Quaternion.LookRotation(desiredFoward);
    }

    private void OnAnimatorMove()
    {
        // rb.MovePositio : ��ġ �̵�
        // rb.position : ���� ��ġ,
        // movement : �Է� �޾� �����̴� ����
        // animator.deltaPosition.magnitude : �ִϸ��̼ǿ��� �����̴� ��
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
