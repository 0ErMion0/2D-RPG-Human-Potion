using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���

    [SerializeField]
    private Text text_Count; // ������ ����
    [SerializeField]
    private GameObject go_CountImage; // ������ ���� �̹���

    // ������ �̹����� ���� ����(����->������)
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1) // �� ���Կ� ������ ���� ���� ��
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true); // ������ ǥ��
        text_Count.text = itemCount.ToString(); // ������ ������ �ؽ�Ʈ�� ǥ��

        SetColor(1); // ������ �������ϰ�
    }

    // �ش� ������ ������ ���� ������Ʈ - �̹� ������ ���� ��
    public void SetSlotCount(int _count)
    {
        itemCount += _count;

        text_Count.text = itemCount.ToString();

        if (itemCount <= 0) // ������ ������ 0���ϸ� ���� ���ֱ�
        {
            ClearSlot();
        }
    }

    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}
