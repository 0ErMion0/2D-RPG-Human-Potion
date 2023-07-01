using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarriorAttack : MonoBehaviour
{
    Vector3 move; // 칼 궤적 발사 방향
    public float playerAttackDmg = 20; // 플레이어가 공격할 때 주는 데미지
    // private GameObject redSlime; // 플레이어 오브젝트
    //private Animator redSlimeAnimator; // Red Slime의 애니메이터

    // Start is called before the first frame update
    void Start()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name == "Stage1") // 스테이지1인 경우
            move = GameObject.FindObjectOfType<PlayerController>().objMove; // PlayerController 스크립트에서 칼 궤적 발사 방향 가져옴
        //else // 스테이지2인 경우
            //move = GameObject.FindObjectOfType<S2PlayerController>().objMove; // S2PlayerController 스크립트에서 칼 궤적 발사 방향 가져옴
        //redSlime = GameObject.FindGameObjectWithTag("RedSlime");  // RedSlime 태그를 가진 오브젝트 찾음
        // redSlimeAnimator = redSlime.GetComponent<Animator>(); // RedSlime 태그를 가진 오브젝트의 Animator를 가져옴
        playerAttackDmg = 20; // 20/60 또는 20/10
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move * Time.deltaTime); // 칼 퀘적 해당 방향으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject); // 얘랑 부딪히는거 다 죽여
        //Destroy(gameObject);


        // 닿으면 닿은 적 체력 감소
        //GameObject.FindObjectOfType<PlayerController>().Attack();
        if (collision.gameObject.tag == "RedSlime") // 닿은 오브젝트의 태그가 RedSlime일 경우
        {
            //GameObject.FindObjectOfType<RedSlimeController>().Damaged(playerAttackDmg); // Red Slime의 Damaged 함수 호출.. 이렇게 하면.. 슬라임 여러개일때 아무 슬라임이나 호출해버린다..
            collision.gameObject.GetComponent<RedSlimeController>().Damaged(playerAttackDmg);
        }
        if (collision.gameObject.tag == "GreenSlime") // 닿은 오브젝트의 태그가 GreenSlime일 경우
        {
            //ameObject.FindObjectOfType<GreenSlimeController>().Damaged(playerAttackDmg); // Green Slime의 Damaged 함수 호출
            collision.gameObject.GetComponent<GreenSlimeController>().Damaged(playerAttackDmg);
        }
        if(collision.gameObject.tag=="Boss") // 닿은 오브젝트의 태그가 Boss인 경우
        {
            collision.gameObject.GetComponent<BossController>().Damaged(playerAttackDmg);
        }

        Destroy(gameObject); // 부딪혀서 없어짐
        //redSlimeAnimator.SetBool("Red Hurt - Animation", false);
    }
}
