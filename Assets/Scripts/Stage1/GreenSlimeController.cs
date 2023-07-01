using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenSlimeController : MonoBehaviour
{
    public Rigidbody2D rb; // Green Slime의 Rigidbody2D
    Animator animator; // Green Slime의 애니메이터
    public GameObject greenSlime; // 생성할 객체의 프리팹(사용x)

    private GameObject player; // 플레이어 오브젝트
    private Animator playerAnimator; // 플레이어의 애니메이터

    public float greenSlimeMaxHp = 10; // Red Slime의 최대 체력
    public float greenSlimeCurrentHp; // Red Slime의 현재 체력
    public float greenSlimeDmg = 10; // Red Slime이 주는 데미지

    public Transform target; // 타깃인 플레이어
    public float distance; // 플레이어와 슬라임 사이의 거리
    public float attackDelay; // 공격 딜레이
    public float delay; // 다음 공격 받을 때까지 걸리는 시간

    public float playerHp; // 플레이어의 hp
    public float playerAttackDamage; // 플레이어가 주는 데미지

    public GameObject gsHpBar; // Red Slime의 Hp 프로그래스 바
    public GameObject canvas; // hp바가 담겨있는 캔버스
    RectTransform hpBar; // Red Slime의 Hp 프로그래스 바
    public float height = 0.5f; // hp바가 위치하게 될 높이
    Image currentHpBar; // hp바의 실제 hp바

    public AudioSource slimeAudioSource; // 슬라임 데미지 입고 죽을 때 나는 오디오

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Green Slime의 Rigidbody2D를 가져옴
        animator = this.GetComponent<Animator>(); // Green Slime의 Animator 가져옴
        player = GameObject.FindGameObjectWithTag("Player");  // Player 태그를 가진 오브젝트 찾음
        playerAnimator = player.GetComponent<Animator>(); // Player 태그를 가진 오브젝트의 Animator를 가져옴

        // 슬라임 체력, 데미지
        greenSlimeMaxHp = 10;
        greenSlimeCurrentHp = greenSlimeMaxHp;
        greenSlimeDmg = 10;

        // 공격 딜레이
        attackDelay = 0;
        delay = 0.78f;

        hpBar = Instantiate(gsHpBar, canvas.transform).GetComponent<RectTransform>(); // 체력바 생성(캔버스의 자식으로 생성하고 위치 변경 위해 hpBar에 할당)
        currentHpBar = hpBar.transform.GetChild(0).GetComponent<Image>(); // 자식 오브젝트 가져옴
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime; // 공격 딜레이 시간 카운트다운
        if (attackDelay < 0) { attackDelay = 0; } // 공격 딜레이 시간 0 미만 되면 0으로 설정

        // 슬라임이 플레이어쪽 보도록
        if (player.transform.position.x < transform.position.x) // 플레이어가 Green Slime의 왼쪽에 있다면
        {
            transform.localScale = new Vector3(-1f, 1f, 1); // 슬라임이 왼쪽을 바라보도록 반전
        }
        else // 플레이어가 Green Slime의 오른쪽에 있다면
        {
            transform.localScale = new Vector3(1f, 1f, 1); // 슬라임이 오른쪽을 바라보도록 반전
        }

        if (hpBar != null)
        {
            Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = hpBarPos; // hp바의 위치를 위에서 설정해준 위치로 설정
        }

        currentHpBar.fillAmount = (float)greenSlimeCurrentHp / greenSlimeMaxHp; // green Slime의 Hp바를 현재 체력/최대 체력으로 표현

        if (greenSlimeCurrentHp <= 0) // Green Slime의 체력이 0이하가 되면
        {
            Die(); // 죽음 함수 호출
        }
    }


    // 공격 받음
    public void Damaged(float gotDamage)
    {
        greenSlimeCurrentHp -= gotDamage; // 플레이어에게 공격 받았을 시, 빨간 슬라임의 체력 깎음
        slimeAudioSource.Play(); // 슬라임이 데미지 얻는 소리 재생
    }

    // 죽음
    void Die()
    {
        animator.SetTrigger("Die"); // 죽음 애니메이션 시작
        GetComponent<Collider2D>().enabled = false; // 충돌 비활성 - 없어지기 전에 달려들면 플레이어 공격 받는 모션 작동될 때도 있어서 넣음
        Destroy(hpBar.gameObject, 1f); // hp바도 1f초 뒤 없앰
        Destroy(gameObject, 1f); // 자기자신 1f초 뒤 없앰
    }

    // 오브젝트와 닿으면
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 닿은 오브젝트가 player인 경우: 슬라임 움직임 멈춤, 슬라임 보는 방향 전환, 플레이어 공격
        if (collision.gameObject.tag == "Player") // 플레이어와 닿았다면 슬라임 움직임 멈춤
        {
            attackDelay = delay; // 딜레이 충전

            if (player.transform.position.x < this.transform.position.x) // 플레이어가 Green Slime의 왼쪽에 있다면
            {
                transform.localScale = new Vector3(-1f, 1f, 1); // 슬라임이 왼쪽을 바라보도록 반전
                player.transform.localScale = new Vector3(5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }
            else // 플레이어가 Red Slime의 오른쪽에 있다면
            {
                transform.localScale = new Vector3(1f, 1f, 1); // 슬라임이 오른쪽을 바라보도록 반전
                player.transform.localScale = new Vector3(-5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }

            playerAnimator.SetBool("isSearch", true); // 플레이어 데미지 입는 애니메이션 시작
            //playerAnimator.SetTrigger("Damaged");

            GameObject.FindObjectOfType<PlayerController>().Damaged(greenSlimeDmg); // 플레이어의 Damaged 함수 호출

        }
    }

    // 플레이어와 닿는 상태가 지속되면 계속 공격
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackDelay == 0) // 공격 딜레이 시간이 0이 되야 공격 재개
            {
                GameObject.FindObjectOfType<PlayerController>().Damaged(greenSlimeDmg); // 플레이어가 greenSlimeDmg만큼 데미지 입도록 함수 호출
                attackDelay = delay; // 공격 딜레이 다시 채움
            }
        }
    }

    // 플레이어와 접촉이 끝나자마자 플레이어의 공격받는 애니메이션 종료
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerAnimator.SetBool("isSearch", false);
    }
}
