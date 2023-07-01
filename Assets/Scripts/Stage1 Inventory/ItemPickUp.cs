using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item; // 알맞은 아이템.cs 타입의 데이터 에셋 여기에 할당(Item.cs는 ScriptableObject 상속 받아서 오브젝트에 컴포넌트로 붙일 수 없음)
}
