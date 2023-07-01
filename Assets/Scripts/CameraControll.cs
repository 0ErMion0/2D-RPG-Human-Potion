using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject player; // 플레이어
    private Vector3 playerPos; // 플레이어의 위치
    private Vector3 cameraPosition; // 카메라의 위치
    public float smoothSpeed; // 카메라의 무빙 속도

    [SerializeField]
    Vector2 center; // 맵의 중앙 위치
    [SerializeField]
    Vector2 mapSize; // 맵의 크기
    float height; // 카메라가 비추는 영역의 세로 길이
    float width; // 카메라가 비추는 영역의 가로 길이

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Warrior"); // 플레이어 오브젝트 찾아서 할당
        smoothSpeed = 2;
        center = new Vector2(13, 7); // 맵의 중앙 위치 할당
        mapSize = new Vector2(25, 14.0625f); // 맵의 크기 할당

    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 살아있다면
        if (player.gameObject != null)
        {
            playerPos.Set(player.transform.position.x, player.transform.position.y, transform.position.z); // playerPos를 player의 위치로 설정해줌
            transform.position = Vector3.Lerp(transform.position, playerPos+cameraPosition, smoothSpeed * Time.deltaTime); // 카메라의 이동 속도가 플레이어의 이동 속도에 비례하도록 움직이게 함

            float lx = (mapSize.x/2) - width; // 카메라의 중앙이 화면 밖을 나가지 않기 위해서 움직일 수 있는 맵의 중앙으로부터의 x 길이
            float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x); // 설정한 x 범위 값을 벗어나지 못하도록 범위 제한

            float ly = (mapSize.y/2) - height; // 카메라의 중앙이 화면 밖을 나가지 않기 위해서 움직일 수 있는 맵의 중앙으로부터의 y 길이
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y); // 설정한 y 범위 값을 벗어나지 못하도록 범위 제한

            transform.position = new Vector3(clampX, clampY, -10f); // 설정한 범위 제한값으로 카메라 포지션 설정

        }
    }
    void OnDrawGizmos() // 카메라 범위 쉽게 할 수 있도록 빨간색 기즈모 설정
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
