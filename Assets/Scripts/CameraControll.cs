using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject player; // �÷��̾�
    private Vector3 playerPos; // �÷��̾��� ��ġ
    private Vector3 cameraPosition; // ī�޶��� ��ġ
    public float smoothSpeed; // ī�޶��� ���� �ӵ�

    [SerializeField]
    Vector2 center; // ���� �߾� ��ġ
    [SerializeField]
    Vector2 mapSize; // ���� ũ��
    float height; // ī�޶� ���ߴ� ������ ���� ����
    float width; // ī�޶� ���ߴ� ������ ���� ����

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Warrior"); // �÷��̾� ������Ʈ ã�Ƽ� �Ҵ�
        smoothSpeed = 2;
        center = new Vector2(13, 7); // ���� �߾� ��ġ �Ҵ�
        mapSize = new Vector2(25, 14.0625f); // ���� ũ�� �Ҵ�

    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ ����ִٸ�
        if (player.gameObject != null)
        {
            playerPos.Set(player.transform.position.x, player.transform.position.y, transform.position.z); // playerPos�� player�� ��ġ�� ��������
            transform.position = Vector3.Lerp(transform.position, playerPos+cameraPosition, smoothSpeed * Time.deltaTime); // ī�޶��� �̵� �ӵ��� �÷��̾��� �̵� �ӵ��� ����ϵ��� �����̰� ��

            float lx = (mapSize.x/2) - width; // ī�޶��� �߾��� ȭ�� ���� ������ �ʱ� ���ؼ� ������ �� �ִ� ���� �߾����κ����� x ����
            float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x); // ������ x ���� ���� ����� ���ϵ��� ���� ����

            float ly = (mapSize.y/2) - height; // ī�޶��� �߾��� ȭ�� ���� ������ �ʱ� ���ؼ� ������ �� �ִ� ���� �߾����κ����� y ����
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y); // ������ y ���� ���� ����� ���ϵ��� ���� ����

            transform.position = new Vector3(clampX, clampY, -10f); // ������ ���� ���Ѱ����� ī�޶� ������ ����

        }
    }
    void OnDrawGizmos() // ī�޶� ���� ���� �� �� �ֵ��� ������ ����� ����
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
