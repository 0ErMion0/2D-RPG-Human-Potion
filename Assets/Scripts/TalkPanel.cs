using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkPanel : MonoBehaviour
{
    public GameObject talkPanel; // ��ȭ UI �г� ������Ʈ
    public Text text; // ��ȭ �ؽ�Ʈ
    int clickCount;// = 0; // ��ư ���� Ƚ��
    //int getGem = false;
    
    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
        text.text = "��ȭ�Ӵ� ������..."; // �ʱ� text
    }

    // Update is called once per frame
    void Update()
    {
        //switch (clickCount) // �� �ƴµ� ���ڱ� �ȵ�.. clickCount�� �þ�ߵǴµ� 0, 1, ...�� ���°� �����ؼ� ���̽�0 �ؽ�Ʈ�� ������
        //{
        //    case 0:
        //        text.text = "��ȭ�Ӵ� ������...";
        //        print("0");
        //        break;
        //    case 1:
        //        text.text = "���͵��� �ϳ��� ��Ÿ���� ...";
        //        print("1");
        //        break;
        //    case 2:
        //        text.text = "�ΰ����� ���ͷ� ���ϱ� �����Ͽ��� ...";
        //        print("2");
        //        break;
        //    case 3:
        //        text.text = "�ΰ����ν� ���� ���ؼ��� ��Ḧ ��� ������ �����ߵȴٴ� �ҹ��� ������ ...";
        //        break;
        //    case 4:
        //        text.text = "� ��Ḧ ��� ������ ������!!";
        //        break;
        //    case 5:
        //        text.text = "";
        //        SceneManager.LoadScene("Stage1"); // ��������1���� �� ��ȯ
        //        break;

        //}
    }

    public void OnClick()
    {
        clickCount+=1; // Front ��ư ������ ���� ��ǳ������ �Ѿ
        switch (clickCount)
        {
            case 0:
                text.text = "��ȭ�Ӵ� ������...";
                print("0");
                break;
            case 1:
                text.text = "���͵��� �ϳ��� ��Ÿ���� ...";
                print("1");
                break;
            case 2:
                text.text = "�ΰ����� ���ͷ� ���ϱ� �����Ͽ��� ...";
                print("2");
                break;
            case 3:
                text.text = "�ΰ����ν� ���� ���ؼ��� ��Ḧ ��� ������ �����ߵȴٴ� �ҹ��� ������ ...";
                break;
            case 4:
                text.text = "� ��Ḧ ��� ������ ������!!";
                break;
            case 5:
                text.text = "";
                SceneManager.LoadScene("Stage1"); // ��������1���� �� ��ȯ
                break;
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene("Stage1"); // ��������1���� �� ��ȯ
    }
}
