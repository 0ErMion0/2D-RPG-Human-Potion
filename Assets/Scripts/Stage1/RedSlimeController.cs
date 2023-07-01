using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedSlimeController : MonoBehaviour
{
    Rigidbody2D rb; // Red Slime의 Rigidbody2D
    public Transform[] moveArr; // 슬라임이 이동할 위치 배열
    public float speed = 0.8f; // 슬라임의 이동 속도
    private int randSpot; // 현재 선택된 위치
    Animator animator; // Red Slime의 애니메이터

    private GameObject player; // 플레이어 오브젝트
    //private PlayerController playerSc; // 플레이어 스크립트
    private Animator playerAnimator; // 플레이어의 애니메이터

    private float redSlimeMaxHp;// = 60; // Red Slime의 최대 체력
    public float redSlimeCurrentHp;// = 60; // Red Slime의 현재 체력
    private float redSlimeDmg;// = 30; // Red Slime이 주는 데미지


    public Transform target; // 타깃인 플레이어
    public float distance; // 플레이어와 슬라임 사이의 거리
    public float attackDelay = 0; // 공격 딜레이
    public float delay = 0.78f; // 다음 공격 받을 때까지 걸리는 시간

    private float playerHp; // 플레이어의 hp
    private float playerAttackDamage; // 플레이어가 주는 데미지

    public GameObject rsHpBar; // Red Slime의 Hp 프로그래스 바
    public GameObject canvas; // hp바가 담겨있는 캔버스
    RectTransform hpBar; // Red Slime의 Hp 프로그래스 바
    private float height = 1.4f; // hp바가 위치하게 될 높이
    Image currentHpBar; // hp바의 실제 hp바

    public AudioSource slimeAudioSource; // 슬라임 데미지 입고 죽을 때 나는 소리

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Red Slime의 Rigidbody2D를 가져옴
        animator = this.GetComponent<Animator>(); // Red Slime의 Animator 가져옴
        player = GameObject.FindGameObjectWithTag("Player");  // Player 태그를 가진 오브젝트 찾음
        //playerSc = GameObject.FindObjectOfType<PlayerController>();
        playerAnimator = player.GetComponent<Animator>(); // Player 태그를 가진 오브젝트의 Animator를 가져옴
        randSpot = 0; // 현재 선택된 위치 0으로 설정

        // 슬라임 체력, 데미지
        redSlimeMaxHp = 60; //60/60
        redSlimeCurrentHp = redSlimeMaxHp;
        redSlimeDmg = 30; // -30/100

        //currentHpBar.value = (float)redSlimeCurrentHp / redSlimeMaxHp; // Red Slime의 Hp바를 현재 체력/최대 체력으로 표현
        // 공격 딜레이
        attackDelay = 0;
        delay = 0.78f;

        // 플레이어 관련
        //playerHp = GameObject.FindObjectOfType<PlayerController>().playerCurrentHp; // PlayerController 스크립트에서 플레이어의 현재 생명 가져옴
        //playerAttackDamage = GameObject.FindObjectOfType<PlayerController>().playerAttackDmg; // PlayerController 스크립트에서 플레이어가 주는 데미지 가져옴

        // 프로그래스 바 관련
        hpBar = Instantiate(rsHpBar, canvas.transform).GetComponent<RectTransform>(); // 체력바 생성(캔버스의 자식으로 생성하고 위치 변경 위해 hpBar에 할당)
        currentHpBar = hpBar.transform.GetChild(0).GetComponent<Image>(); // 자식 오브젝트 가져옴
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime; // 공격 딜레이 카운트다운
        if (attackDelay < 0) { attackDelay = 0; } // 공격 딜레이 0 미만이면 0으로 초기화

        distance = Vector3.Distance(transform.position, target.position); // 슬라임과 플레이어 사이 거리

        if(distance > 1.142868) // 슬라임과 플레이어 사이 거리가 1.142868보다 클 때 Move
        {
            Move(); // 슬라임 움직이는 함수
        }
        else
        {
            // 오브젝트와 닿지 않더라도 이 거리 내에 들어오면 플레이어 몸 돌려주기
            if (player.transform.position.x < this.transform.position.x) // 플레이어가 Red Slime의 왼쪽에 있다면
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1); // 슬라임이 왼쪽을 바라보도록 반전
                player.transform.localScale = new Vector3(5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }
            else // 플레이어가 Red Slime의 오른쪽에 있다면
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1); // 슬라임이 오른쪽을 바라보도록 반전
                player.transform.localScale = new Vector3(-5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }
        }

        if (hpBar != null)
        {
            Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = hpBarPos; // hp바의 위치를 위에서 설정해준 위치로
        }

        currentHpBar.fillAmount = (float)redSlimeCurrentHp/redSlimeMaxHp; // Red Slime의 Hp바를 현재 체력/최대 체력으로 표현
        //currentHpBar.value = (float)redSlimeCurrentHp / redSlimeMaxHp; // Red Slime의 Hp바를 현재 체력/최대 체력으로 표현

        if (redSlimeCurrentHp <= 0) // Red Slime의 체력이 0이하가 되면
        {
            Die(); // 죽음 함수 호출
        }
    }

    // 슬라임 이동
    void Move()
    { 
            speed = 0.8f; // 움직임

            transform.position = Vector2.MoveTowards( // 현재 위치에서 moveArr[randSpot] 위치로 지정된 속도 이동
                transform.position, moveArr[randSpot].position, speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, moveArr[randSpot].position) < 0.2f) // 거리차가 0.2보다 작으면 다음 위치로 이동
            {
                randSpot = Random.Range(0, moveArr.Length); // 두 개의 WayPoint가 있는데 한쪽에 다다르면 다른쪽으로 감
            }

            // 슬라임이 플레이어쪽 보도록
            if (player.transform.position.x < this.transform.position.x) // 플레이어가 Red Slime의 왼쪽에 있다면
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1); // 슬라임이 왼쪽을 바라보도록 반전
                //player.transform.localScale = new Vector3(5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }
            else // 플레이어가 Red Slime의 오른쪽에 있다면
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1); // 슬라임이 오른쪽을 바라보도록 반전
                //player.transform.localScale = new Vector3(-5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }

        // 플레이어 애니메이션 Damaged 중단
        //playerAnimator.SetBool("isSearch", false); // isSearch가 false면 플레이어 공격 받는 애니메이션 중단
    }

    // 공격 받음
    public void Damaged(float damage)
    {
        //print("빨간 슬라임 공격 받음");
        redSlimeCurrentHp -= damage; // 플레이어에게 공격 받았을 시, 빨간 슬라임의 체력 깎음
        slimeAudioSource.Play(); // 오디오 재생
    }

    // 죽음
    void Die()
    {
        speed = 0; // 움직임 멈춤
        animator.SetTrigger("Die"); // 죽음 애니메이션 시작
        GetComponent<Collider2D>().enabled = false; // 충돌 비활성
        //Destroy(GetComponent<Rigidbody2D>()); // 중력 제거
        Destroy(hpBar.gameObject, 1f); // hp바도 1f초 뒤 없앰
        Destroy(gameObject, 1f); // 1f초 뒤 없앰
    }

    // 오브젝트와 닿으면
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 닿은 오브젝트가 player인 경우: 슬라임 움직임 멈춤, 슬라임 보는 방향 전환, 플레이어 공격
        if(collision.gameObject.tag == "Player") // 플레이어와 닿았다면 슬라임 움직임 멈춤
        {
            speed = 0; // 움직임을 멈춤
            attackDelay = delay; // 딜레이 충전

            if (player.transform.position.x < this.transform.position.x) // 플레이어가 Red Slime의 왼쪽에 있다면
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1); // 슬라임이 왼쪽을 바라보도록 반전
                player.transform.localScale = new Vector3(5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }
            else // 플레이어가 Red Slime의 오른쪽에 있다면
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1); // 슬라임이 오른쪽을 바라보도록 반전
                player.transform.localScale = new Vector3(-5, 5, 1); // 플레이어가 슬라임 쪽을 바라보도록(맞는 애니메이션이 때문에)
            }

            playerAnimator.SetBool("isSearch", true); // 플레이어 데미지 입는 애니메이션 시작
            //playerAnimator.SetTrigger("Search");
            //GameObject.FindObjectOfType<PlayerController>().Damaged();

            //if (attackDelay == 0) // 이렇게 하면 딜레이 시간 0되기 전에 Enter 벗어났을 때 데미지 안 들어감
            //{
                GameObject.FindObjectOfType<PlayerController>().Damaged(redSlimeDmg); // 플레이어의 Damaged 함수 호출
               // attackDelay = 2.5f; // 공격 딜레이 다시 채움
            //}

        }
        // 닿은 오브젝트가 player attack인 경우: 공격 받은 애니메이션 시작
        if (collision.gameObject.tag == "PlayerAttack")
        {
            animator.SetTrigger("isHurt"); // 빨간 슬라임 데미지 입는 애니메이션 시작
            //Damaged(); // Red Slime의 Damaged 함수 호출
        }
    }

    // 플레이어와 닿는 상태가 지속되면 계속 공격
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackDelay == 0) // 공격 딜레이 시간이 0이 되야 공격 재개
            {
                GameObject.FindObjectOfType<PlayerController>().Damaged(redSlimeDmg); // 플레이어에게 redSlimeDmg만큼 데미지 입히도록 함수 호출
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
