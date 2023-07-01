using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지

    [SerializeField]
    private Text text_Count; // 아이템 개수
    [SerializeField]
    private GameObject go_CountImage; // 아이템 개수 이미지

    // 아이템 이미지의 투명도 조절(투명->불투명)
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int _count = 1) // 빈 슬롯에 아이템 새로 들어올 때
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true); // 아이템 표시
        text_Count.text = itemCount.ToString(); // 아이템 개수를 텍스트로 표시

        SetColor(1); // 아이템 불투명하게
    }

    // 해당 슬롯의 아이템 갯수 업데이트 - 이미 아이템 있을 때
    public void SetSlotCount(int _count)
    {
        itemCount += _count;

        text_Count.text = itemCount.ToString();

        if (itemCount <= 0) // 아이템 개수가 0이하면 슬록 없애기
        {
            ClearSlot();
        }
    }

    // 해당 슬롯 하나 삭제
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
