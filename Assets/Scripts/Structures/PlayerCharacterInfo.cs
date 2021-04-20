using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 캐릭터 정보를 나타내기 위한 구조체입니다.
/// - 이 형식으로 생성된 데이터는 각각의 캐릭터마다 하나씩 가져야 합니다.
/// - 사용중인 데이터는 GamePlayerController 를 통해 얻을 수 있습니다.
[System.Serializable]
public struct PlayerCharacterInfo
{
	// 현재 장착중인 아이템 정보
	public List<EquipItemInfo> partsInfos;

	// 인벤토리 슬롯 개수
	public int inventorySlotCount;

	// 소지중인 아이템 정보
	public List<ItemSlotInfo> inventoryItemInfos;

	// 소지금
	public int silver;


	public void Initialize()
	{
		inventorySlotCount = 50;

		silver = 10000;

		inventoryItemInfos = new List<ItemSlotInfo>();
		for (int i = 0; i < inventorySlotCount; ++i)
			inventoryItemInfos.Add(new ItemSlotInfo());

		inventoryItemInfos[3] = new ItemSlotInfo("90002", 3, 10);
		inventoryItemInfos[6] = new ItemSlotInfo("90004", 4, 10);
		inventoryItemInfos[9] = new ItemSlotInfo("90000", 5, 10);
		inventoryItemInfos[12] = new ItemSlotInfo("90005", 6, 10);






		partsInfos = new List<EquipItemInfo>();

		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "000200.json")); // 몸통

		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "000001.json")); // 머리
		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "000020.json")); // 얼굴
		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "000040.json")); // 머리카락

		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "001200.json")); // 모자
		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "003200.json")); // 주무기
		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "004000.json")); // 방패

		partsInfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("EquipItemInfos", "002000.json")); // 가방
	}
}