using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScrollBar : MonoBehaviour
{
    float time; // 흐르고 있는 시간
    float maxTime; // 리미트 시간
    float leftTime; // 남은 시간
    bool startTimer; // 타이머 시작 여부
    float delayTime = 5f; // 5초 딜레이 시간

    public Text timeText; // 남은 시간 나타내주는 텍스트

    // Start is called before the first frame update
    void Start()
    {
        time = 0; // 흐르고 있는 시간을 0으로 초기화
        maxTime = 150; // 150초 -> 3분
        startTimer = false; // 타이머 시작 안했음
        StartCoroutine(StartTimerWithDelay()); // 코루틴 함수 호출
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer) // 타이머가 시작되면
        {
            time += Time.deltaTime; // 흐르고 있는 시간 time에 더해줌
            leftTime = maxTime - time; // 남은 시간 구함
            this.gameObject.GetComponent<Image>().fillAmount = (float)leftTime / maxTime; // 시간 바에 남은 시간 표시해줌
            timeText.text = (int)leftTime+""; // 남은 시간 텍스트로 표현

            if (maxTime - time <= 0) // 제한 시간이 끝나면
            {
                SceneManager.LoadScene("Fail1"); // Fail1으로 전환
            }
        }
    }

    // 코루틴-> 딜레이 이후에 타이머 시작
    IEnumerator StartTimerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        startTimer = true; // 타이머 시작
    }
}
