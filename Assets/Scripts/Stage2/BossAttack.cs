using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Vector2 move; // 칼 궤적 발사 방향
    public float bossAttackDmg = 30; // 보스가 주는 칼 공격 데미지

    private void Start()
    {
        move = GameObject.FindObjectOfType<BossController>().attackObjMove; // BossController 스크립트에서 칼 궤적 발사 방향 가져옴
    }

    private void Update()
    {
        transform.Translate(move * Time.deltaTime); // 칼 퀘적 해당 방향으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision) // 닿으면
    {
       if(collision.gameObject.tag == "Player")
        {
            print("닿음");
            collision.gameObject.GetComponent<PlayerController>().Damaged(bossAttackDmg);
            // 닿은 오브젝트의 플레이어 컨트롤 스크립트의 Damaged함수 호출하여 플레이어에게 데미지 줌
            Destroy(gameObject); // 닿았으니 오브젝트 없앰
        }
    }
}
