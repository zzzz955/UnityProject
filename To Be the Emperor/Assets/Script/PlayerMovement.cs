using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    public float moveSpeed = 4f;  // 초당 픽셀 이동 거리
    public int pixelsPerUnit = 32; // 타일 크기
    private Vector2 movement;

    void Update()
    {
        // 방향키 입력
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 대각선 이동 방지
        if (movement.x != 0) movement.y = 0;
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            Vector3 newPosition = transform.position + (Vector3)movement * moveSpeed * Time.fixedDeltaTime;
            newPosition.x = Mathf.Round(newPosition.x * pixelsPerUnit) / pixelsPerUnit;
            newPosition.y = Mathf.Round(newPosition.y * pixelsPerUnit) / pixelsPerUnit;
            transform.position = newPosition;
        }
    }
}
