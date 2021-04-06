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
	public List<EquipItemInfo> Partsinfos;

	public void Initialize()
	{
		Partsinfos = new List<EquipItemInfo>();

		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("000200")); // 몸통

		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("000001")); // 머리
		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("000020")); // 얼굴
		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("000040")); // 머리카락
		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("001200")); // 모자

		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("003200")); // 주무기
		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("004000")); // 방패

		Partsinfos.Add(ResourceManager.Instance.LoadJson<EquipItemInfo>("002000")); // 가방
	}



}