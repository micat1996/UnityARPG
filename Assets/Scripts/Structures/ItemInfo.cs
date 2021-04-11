using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 하나의 아이템 정보를 나타내기 위한 구조체입니다.
[System.Serializable]
public struct ItemInfo
{
	// 아이템 코드
	public string itemCode;

	// 아이템 타입
	public ItemType itemType;

	// 아이템 이름
	public string itemName;

	// 아이템 설명
	public string itemDescription;

	// 아이템 이미지 경로
	public string itemImagePath;

	// 이 아이템이 슬롯에 몇 개 들어갈 수 있는지를 나타냅니다.
	public int maxSlotCount;

	// 아이템 판매 가격
	public int Price;

	// 아이템 등급
	// 장비 아이템과 소비 아이템에만 적용됩니다.
	//public ItemLevel itemLevel;

}
