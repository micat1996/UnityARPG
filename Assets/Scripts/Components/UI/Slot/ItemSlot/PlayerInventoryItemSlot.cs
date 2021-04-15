using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInventoryItemSlot:ItemSlot
{
	// 인벤토리 아이템 슬롯 인덱스
	private int _ItemSlotIndex;

	public int itemSlotIndex => _ItemSlotIndex;

	// 인벤토리 아이템 슬롯을 초기화합니다.
	public void InitializeInventoryItemSlot(
		SlotType slotType,
		string itemCode,
		int itemSlotIndex)
	{
		// 슬롯 초기화
		base.InitializeSlot(slotType, itemCode);

		// 인덱스 설정
		_ItemSlotIndex = itemSlotIndex;

		gameObject.name = $"InventorySlot{itemSlotIndex}";

		// 아이템 개수 텍스트 갱신
		UpdateItemCountText();

		// 드래그 드랍 사용
		m_UseDragDrop = true;

		// 드래그 시작 시 실행할 내용 정의
		onSlotBeginDragEvent += (dragDropOperation, dragVisual) =>
			{
				dragVisual.SetDragImageFromSprite(slotImage.sprite);
			};
	}

	// 아이템 개수 텍스트를 갱신합니다.
	private void UpdateItemCountText()
	{
		var playerController = PlayerManager.Instance.playerController as GamePlayerController;
		ItemSlotInfo itemSlotInfo = playerController.playerCharacterInfo.inventoryItemInfos[_ItemSlotIndex];

		SetSlotItemCount(itemSlotInfo.itemCount);
	}
}
