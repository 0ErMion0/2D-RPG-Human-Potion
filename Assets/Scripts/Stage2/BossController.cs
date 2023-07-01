using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    Rigidbody2D rb; // 보스의 Rigidbody2D
    Animator animator; // 보스의 animator
    public GameObject player; // 플레이어 오브젝트
    //private Animator playerAnimator; // 플레이어의 애니메이터

    public float bossMaxHp = 1000; // 보스의 최대 체력
    public float bossCurrentHp; // 보스의 현재 체력
    //public float bossAttackDmg = 30; // 보스가 주는 칼 공격 데미지
    //public float bossCastDmg = 20; // 보스가 주는 마법 공격 데미지

    GameObject attackObj; // 칼 궤적 프리팹 넣을 오브젝트
    public Vector3 attackObjMove; // 칼 궤적 프리팹의 이동 방향 - 왜 있는지 다시 보기
    GameObject castObj; // 소환 공격 프리팹 넣을 오브젝트
    
    public float attackDelay; // 다음 공격할 때까지 걸리는 시간
    public float castDelay; // 다음 소환 공격할 때까지 걸리는 시간
    private float attackDelta; // 칼 궤적 공격 시간 체크
    private float castDelta; // 소환 공격 시간 체크


    [SerializeField]
    GameObject attackPrefab; // 공격할 때 나가는 칼 궤적
    [SerializeField]
    GameObject castPrefab; // 소환 공격할 때 떨어지는 불

    public GameObject currentHpBar; // hp바의 실제 hp바
    public Text currentHpText; // hp를 표시해주는 text

    Vector3 xyplus; // 프리팹 생성 위치 조정
    Vector3 xyminus;

    public GameObject Eye; // 눈알 아이템
    public GameObject NextStageUI; // 다음 스테이지 가이드 UI
    public GameObject NextStagePotal; // 다음 스테이지 포탈
    //public GameObject RunAwayUI; // 도망 가이드 UI
    //public GameObject RunAwayPotal; // 도망갈 수 있도록 해주는 포탈

    // 오디오
    public AudioSource swordSound; // 칼 휘두르는 소리
    public AudioSource castSound; // 소환하는 소리
    public AudioSource damagedSound; // 데미지 입는 소리

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 보스의 Rigidbody2D를 가져옴
        animator = this.GetComponent<Animator>(); // 보스의 Animator 가져옴
        player = GameObject.FindGameObjectWithTag("Player");  // Player 태그를 가진 오브젝트 찾음
        //playerAnimator = player.GetComponent<Animator>(); // Player 태그를 가진 오브젝트의 Animator를 가져옴

        // 보스의 체력, 공격 데미지
        bossMaxHp = 1000;
        bossCurrentHp = bossMaxHp;
        //bossAttackDmg = 30;
        //bossCastDmg = 20;

        attackDelay = 7f; // 공격 딜레이 시간
        castDelay = 3f; // 소환 공격 딜레이 시간


        // 프로그래스 바 관련
        currentHpBar = GameObject.Find("BossHpBar");

        xyplus = new Vector3(-5, -1.3f, 0);  // 보스의 중심점으로 프리팹 생성될 수 있도록 더해주는 벡터
        xyminus = new Vector3(5, -1.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        attackDelta += Time.deltaTime; // 칼 궤적 공격 시간 카운트
        castDelta += Time.deltaTime; // 소환 공격 시간 카운트

        if (attackDelta > attackDelay) // 칼 공격 딜레이 시간보다 지난 시간이 크다면
        {
            attackDelta = 0; // 흐른 시간 카운트 0으로 초기화
            animator.SetTrigger("Attack"); // 공격 애니메이션 시작
            swordSound.Play(); // 칼 휘두르는 소리 재생
            //Invoke("SwordTrail", 0.4f); // 칼 궤적 발사 함수 0.4초 뒤 호출
        }
        if (castDelta > castDelay) // 소환 공격 딜레이 시간보다 지난 시간이 크다면
        {
            castDelta = 0; // 흐른 시간 카운트 0으로 초기화
            animator.SetTrigger("Cast"); // 소환 공격 애니메이션 시작
            castSound.Play(); // 소환하는 소리 재생
            //Invoke("Cast", 0.5f); // 소환 공격 함수 0.5초 후 호출
            //Invoke("Cast", 0.5f); // 소환 공격 함수 0.5초 후 호출
        }

        // 보스가 보는 방향 설정
        if (player.transform.position.x < this.transform.position.x) // 플레이어의 위치가 보스의 왼쪽에 있을 시
        {
            this.gameObject.transform.localScale = new Vector3(10, 10, 1); // 보스가 왼쪽 봄
            this.gameObject.transform.position = new Vector3(25, 2.55f, 1); // 보스의 위치(피벗이 보스의 가운데에 있는게 아니라서)
        }
        else // 플레이어의 위치가 보스의 오른쪽에 있을 시
        {
            this.gameObject.transform.localScale = new Vector3(-10, 10, 1); // 보스가 오른쪽 봄
            this.gameObject.transform.position = new Vector3(18.15f, 2.55f, 1); // 보스의 새 위치(피벗이 보스의 가운데에 있는게 아니라서)
        }

        if (bossCurrentHp <= 0) // 보스의 체력이 0보다 작다면
        {
            animator.SetTrigger("Die"); // 죽은 모습 보여주다가 죽은 모습 애니메이션 끝나면 Die()함수 호출하며 씬 전환
        }

        currentHpBar.GetComponent<Image>().fillAmount = (float)bossCurrentHp / bossMaxHp; // Player의 Hp바를 현재 체력/최대 체력으로 표현
        currentHpText.text = bossCurrentHp + "/1000"; // player의 hp를 나타내주는 글씨 수정
    }

    // 칼 궤적 발사
    public void SwordTrail()
    {
        // 칼 궤적 발사
        if (player.transform.position.x<this.transform.position.x) // 플레이어의 위치가 보스의 왼쪽에 있을 시 오른쪽으로 발사
        {
            attackObj = Instantiate(attackPrefab, transform.position + Vector3.left+ xyplus, transform.rotation); // 프리팹, 생성될 위치, 생성될 오브제젝트의 회전값
            attackObjMove = new Vector3(-15, 0, 0); // 왼쪽으로 발사
            attackObj.transform.localScale = new Vector3(7, 7, 1); // 궤적의 좌우반전 관여(왼쪽 봄)
        }
        else
        {
            attackObj = Instantiate(attackPrefab, transform.position + Vector3.right + xyminus, transform.rotation); // 프리팹, 생성될 위치, 생성될 오브제젝트의 회전값
            attackObjMove = new Vector3(15, 0, 0); // 오른쪽으로 발사
            attackObj.transform.localScale = new Vector3(-7, 7, 1); // 궤적의 좌우반전 관여(오른쪽 봄)
        }

        Destroy(attackObj, 1f); // 1초 뒤에 없어짐
    }

    // 소환 공격 호출
    public void Cast()
    {
        castObj = Instantiate(castPrefab); // 프리팹으로 오브젝트 생성
        int position = Random.Range(-10, 36); // 이 영역 안에서 랜덤으로 오브젝트 생성
        castObj.transform.position = new Vector3(position, 1.45f, 0); // 프리팹의 생성 위치 설정
        Destroy(castObj, 1f); // 1초 뒤에 없어짐
    }

    // 공격 받음
    public void Damaged(float gotDamage)
    {
        damagedSound.Play(); // 데미지 입는 소리 재생
        bossCurrentHp -= gotDamage; // 플레이어에게 공격 받았을 시, 보스의 체력 깎음
    }

    // 죽음
    void Die()
    {
        //animator.SetTrigger("Die"); // 죽음 애니메이션 시작
        //GetComponent<Collider2D>().enabled = false; // 충돌 비활성 - 없어지기 전에 달려들면 플레이어 공격 받는 모션 작동될 때도 있어서 넣음
        //Destroy(currentHpBar.gameObject, 1f); // hp바도 1f초 뒤 없앰
        //Destroy(gameObject, 1f); // 자기자신 1f초 뒤 없앰

        Destroy(gameObject, 1f); // 자기 자신 1초 뒤 없앰

        Eye.SetActive(true); // 눈알 아이템 나타냄
        NextStageUI.SetActive(true); // 다음 스테이지 가이드 UI 나타냄
        NextStagePotal.SetActive(true); // 다음 스테이지 이동 포탈 나타냄

        //// runaway관련 ui 나타나는 조건 충족했지만 플레이어가 보스 죽인다면 다음 스테이지 가이드 ui와 겹쳐서 나타나는 문제 해결용
        //RunAwayUI.SetActive(false); // 도망 가이드 UI가 사라지도록함
        //RunAwayPotal.SetActive(false); // 도망 포탈 사라지도록 함

        // 실패 화면 2로 전환
        //SceneManager.LoadScene("Clear2");
    }
}
