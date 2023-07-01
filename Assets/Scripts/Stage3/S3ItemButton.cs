using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S3ItemButton : MonoBehaviour
{
    // Stage3 ���� ����
    public List<Button> buttons; // ��ư ����Ʈ
    public GameObject HumanPotion; // human potion �̹���

    private int clickedButtonCount; // ��ư�� � Ŭ���Ǿ����� ī��Ʈ

    Scene scene; // ���� �� Ȯ��

    public AudioSource getItemAudioSource; // human potion ���� �Ϸ� �����

    private void Start()
    {
        clickedButtonCount = 0;

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnClickItemButton(button)); // ��ư Ŭ���� ������ �̺�Ʈ ������ �߰�, �Լ� ȣ��
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene(); // ������ �� ������ ������
    }

    private void OnClickItemButton(Button clickedButton)
    {
        clickedButton.interactable = false; // Ŭ���� ��ư ��Ȱ��ȭ
        clickedButtonCount++; // ��ư ���� Ƚ�� ����

        if (clickedButtonCount == buttons.Count) // Ŭ���� ��ư�� ���� ����Ʈ�� ����� ��ư�� ���� �����ϴٸ�
        {
            HumanPotion.SetActive(true); // human potion Ȱ��ȭ
            getItemAudioSource.Play(); // ���� ���� �˸��� ����� ���
        }
    }

    public void OnClickGetPotion()
    {
        if (scene.name == "Stage3") // ��������3���
        {
            SceneManager.LoadScene("Clear2"); // Clear2�� ��ȯ
        }
        if (scene.name == "Stage3_2") // ��������3_2���
        {
            SceneManager.LoadScene("Clear1"); // Clear1�� ��ȯ
        }
    }
}
