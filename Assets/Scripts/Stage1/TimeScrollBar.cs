using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScrollBar : MonoBehaviour
{
    float time; // �帣�� �ִ� �ð�
    float maxTime; // ����Ʈ �ð�
    float leftTime; // ���� �ð�
    bool startTimer; // Ÿ�̸� ���� ����
    float delayTime = 5f; // 5�� ������ �ð�

    public Text timeText; // ���� �ð� ��Ÿ���ִ� �ؽ�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        time = 0; // �帣�� �ִ� �ð��� 0���� �ʱ�ȭ
        maxTime = 150; // 150�� -> 3��
        startTimer = false; // Ÿ�̸� ���� ������
        StartCoroutine(StartTimerWithDelay()); // �ڷ�ƾ �Լ� ȣ��
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer) // Ÿ�̸Ӱ� ���۵Ǹ�
        {
            time += Time.deltaTime; // �帣�� �ִ� �ð� time�� ������
            leftTime = maxTime - time; // ���� �ð� ����
            this.gameObject.GetComponent<Image>().fillAmount = (float)leftTime / maxTime; // �ð� �ٿ� ���� �ð� ǥ������
            timeText.text = (int)leftTime+""; // ���� �ð� �ؽ�Ʈ�� ǥ��

            if (maxTime - time <= 0) // ���� �ð��� ������
            {
                SceneManager.LoadScene("Fail1"); // Fail1���� ��ȯ
            }
        }
    }

    // �ڷ�ƾ-> ������ ���Ŀ� Ÿ�̸� ����
    IEnumerator StartTimerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        startTimer = true; // Ÿ�̸� ����
    }
}
