using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ItemEditor : MonoBehaviour
{

	private void Awake()
	{

		LoadAllItemInfos();
	}

	// 모든 아이템 정보를 읽어옵니다.
	private void LoadAllItemInfos()
	{

	}

	// 아이템 코드 버튼을 추가합니다.
	private void AddItemCodeButton(ItemInfo? newItemInfo)
	{
		if (newItemInfo.HasValue)
		{

		}
		else
		{

		}

	}
}
