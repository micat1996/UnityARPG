﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInventoryWnd:ClosableWnd
{
	[SerializeField] private RectTransform _Panel_ItemSlots;

	private PlayerInventoryItemSlot _Panel_PlayerInventoryItemSlotPrefab;

	protected override void Awake()
	{
		base.Awake();

		_Panel_PlayerInventoryItemSlotPrefab = ResourceManager.Instance.LoadResource<GameObject>(
			"_Panel_PlayerInventoryItemSlot",
			"Prefabs/UI/Slot/Panel_PlayerInventoryItemSlot").GetComponent<PlayerInventoryItemSlot>();

		// 인벤토리 창 초기화
		InitializeInventoryWnd();
	}

	// 인벤토리 창을 초기화합니다.
	private void InitializeInventoryWnd()
	{
		PlayerController playerController = (PlayerManager.Instance.playerController as PlayerController);
		ref PlayerCharacterInfo playerCharacterInfo = ref playerController.playerCharacterInfo;

		for (int i = 0; i < playerCharacterInfo.inventorySlotCount; ++i)
		{
			var newItemSlot = CreateItemSlot();
			newItemSlot.InitializeInventoryItemSlot(
				SlotType.InventorySlot,
				playerCharacterInfo.inventoryItemInfos[i].itemCode,
				i);
		}

		// 창이 종료될 때
		onWndClosedEvent += () =>
			// 드래그 드랍 작업을 끝냅니다.
			m_ScreenInstance.FinishDragDropOperation();
	}

	private PlayerInventoryItemSlot CreateItemSlot() =>
		Instantiate(_Panel_PlayerInventoryItemSlotPrefab, _Panel_ItemSlots);



}