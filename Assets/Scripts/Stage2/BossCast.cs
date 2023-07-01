using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCast : MonoBehaviour
{
    public float bossCastDmg = 20; // 보스가 주는 마법 공격 데미지
    // 0.2초 뒤에 콜라이더 켜지도록
    private BoxCollider2D prefabCollider; // 소환 공격 프리팹의 콜라이더

    private void Start()
    {
        prefabCollider = GetComponent<BoxCollider2D>(); // 프리팹의 콜라이더 가져옴
        prefabCollider.enabled = false; // 콜라이더 꺼둠
        Invoke("ActivateCollider", 0.2f); // 0.2초 뒤에 콜라이더 킴
    }

    private void ActivateCollider()
    {
        prefabCollider.enabled = true; // 콜라이더 킴
    }

    //private void OnCollisionEnter2D(Collision2D collision) // 닿으면 -> 이렇게하면 닿자마자 이펙트 다 안 보여주고 사라짐
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<S2PlayerController>().Damaged(bossCastDmg);
    //        // 닿은 오브젝트의 플레이어 컨트롤 스크립트의 Damaged함수 호출하여 플레이어에게 데미지 줌
    //        Destroy(gameObject); // 닿았으니 오브젝트 없앰
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damaged(bossCastDmg);
            // 닿은 오브젝트의 플레이어 컨트롤 스크립트의 Damaged함수 호출하여 플레이어에게 데미지 줌
            Destroy(gameObject); // 닿았으니 오브젝트 없앰
        }
    }
}
