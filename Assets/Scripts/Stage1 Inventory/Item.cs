using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] // 아이템 이름
public class Item : ScriptableObject // 아이템 저장 데이터 컨테이너
{
    public enum ItemType  // 아이템 유형
    {
        Ingredient, // 포션 재료
        life, // 생명 포션 재료
    }

    public string itemName; // 아이템의 이름
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템 이미지-인벤토리 안
    //public GameObject itemPrefab;  // 아이템 프리팹
}
