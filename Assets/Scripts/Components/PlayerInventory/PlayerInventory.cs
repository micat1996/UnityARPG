using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInventory:MonoBehaviour
{
	// PlayerInventoryWnd 프리팹을 나타냅니다.
	private PlayerInventoryWnd _Wnd_PlayerInventoryPrefab;

	// PlayerInventory 창 객체를 나타냅니다.
	public PlayerInventoryWnd playerInventoryWnd { get; private set; }

	private void Awake()
	{
		_Wnd_PlayerInventoryPrefab = ResourceManager.Instance.LoadResource<GameObject>(
			"Wnd_PlayerInventoryPrefab",
			"Prefabs/UI/Wnds/PlayerInventoryWnd/Wnd_PlayerInventory").GetComponent<PlayerInventoryWnd>();
	}

	// 인벤토리 창을 토글합니다.
	public void ToggleInventoryWnd()
	{
		if (playerInventoryWnd == null) OpenInventoryWnd();
		else CloseInventoryWnd();
	}

	// 인벤토리 창을 엽니다.
	public void OpenInventoryWnd(bool usePrevPosition = true)
	{
		if (playerInventoryWnd != null) return;

		// 인벤토리 창을 생성합니다.
		playerInventoryWnd = PlayerManager.Instance.playerController.
			screenInstance.CreateWnd(_Wnd_PlayerInventoryPrefab, usePrevPosition) as PlayerInventoryWnd;

		// 인벤토리 창이 닫힐 때 playerInventoryWnd 를 null 로 설정합니다.
		playerInventoryWnd.onWndClosedEvent += () => playerInventoryWnd = null;
	}

	// 인벤토리 창을 닫습니다.
	public void CloseInventoryWnd()
	{
		if (playerInventoryWnd == null) return;

		PlayerManager.Instance.playerController.
			screenInstance.CloseWnd(playerInventoryWnd);
	}

	// 인벤토리 아이템을 교체합니다.
	public void SwapItem(PlayerInventoryItemSlot first, PlayerInventoryItemSlot second)
	{
		ref PlayerCharacterInfo playerInfo = ref (PlayerManager.Instance.playerController as PlayerController).playerCharacterInfo;

		// 소지 아이템 정보 변경
		var tempItemInfo = playerInfo.inventoryItemInfos[first.itemSlotIndex];
		playerInfo.inventoryItemInfos[first.itemSlotIndex] = playerInfo.inventoryItemInfos[second.itemSlotIndex];
		playerInfo.inventoryItemInfos[second.itemSlotIndex] = tempItemInfo;

		// 슬롯 아이템 정보 변경
		first.SetItemInfo(playerInfo.inventoryItemInfos[first.itemSlotIndex].itemCode);
		second.SetItemInfo(playerInfo.inventoryItemInfos[second.itemSlotIndex].itemCode);

		// 슬롯 갱신
		first.UpdateInventoryItemSlot();
		second.UpdateInventoryItemSlot();
	}

	// 인벤토리에 아이템을 추가합니다.
	public bool AddItem(ItemSlotInfo newItemSlotInfo)
	{
		// 인벤토리 슬롯에 아이템을 추가합니다.
		void FillSlot(ItemSlotInfo newItemSlotInfo, List<ItemSlotInfo> inventoryItemInfos, int slotIndex)
		{
			// 아이템을 추가할 수 있는 여유 공간이 존재하는지 확인합니다.
			int addableItemCount = inventoryItemInfos[slotIndex].maxSlotCount - inventoryItemInfos[slotIndex].itemCount;
			if (addableItemCount > 0)
			{
				// 추가할 수 있는 여유 공간을 매꾸며, 아이템을 최대한 채웁니다.
				for (int i = 0;
					(i < addableItemCount) && newItemSlotInfo.itemCount > 0;
					++i)
				{
					ItemSlotInfo itemSlotInfo = inventoryItemInfos[slotIndex];

					// 아이템을 추가합니다.
					++itemSlotInfo.itemCount;
					inventoryItemInfos[slotIndex] = itemSlotInfo;

					// 추가한 아이템을 제외합니다.
					--newItemSlotInfo.itemCount;
				}
			}
		}

		// GamePlayerController
		GamePlayerController gamePlayerController = (PlayerManager.Instance.playerController as GamePlayerController);

		// 플레이어 캐릭터 정보를 얻습니다.
		ref PlayerCharacterInfo playerInfo = ref gamePlayerController.playerCharacterInfo;

		List<ItemSlotInfo> inventoryItemInfos = playerInfo.inventoryItemInfos;

		for (int i = 0; i < playerInfo.inventorySlotCount; ++i)
		{
			// 모든 아이템을 추가했다면
			if (newItemSlotInfo.itemCount <= 0)
			{
				// 모든 아이템 추가됨
				return true;
			}

			// 만약 추가하려는 아이템과 동일한 아이템을 갖는 슬롯을 찾았다면
			if (inventoryItemInfos[i].IsSameItem(newItemSlotInfo))
			{
				FillSlot(newItemSlotInfo, inventoryItemInfos, i);

				if (playerInventoryWnd)
					playerInventoryWnd.UpdateInventoryItemSlots();
			}

			// 빈 아이템 슬롯을 찾았다면
			else if (inventoryItemInfos[i].IsEmpty())
			{

				ItemSlotInfo itemSlotInfo = newItemSlotInfo;
				itemSlotInfo.itemCount = 0;

				inventoryItemInfos[i] = itemSlotInfo;

				FillSlot(newItemSlotInfo, inventoryItemInfos, i);

				if (playerInventoryWnd)
					playerInventoryWnd.UpdateInventoryItemSlots();
			}
		}

		return false;
	}


	// 아이템을 인벤토리에서 제거합니다.
	/// - itemSlotIndex : 제거할 슬롯의 인덱스를 전달합니다.
	/// - removeCount : 제거할 개수를 전달합니다.
	public void RemoveItem(int itemSlotIndex, int removeCount)
	{
		ref PlayerCharacterInfo playerInfo = ref (PlayerManager.Instance.playerController as PlayerController).playerCharacterInfo;
		List<ItemSlotInfo> inventoryItemInfos = playerInfo.inventoryItemInfos;

		ItemSlotInfo itemSlotInfo = inventoryItemInfos[itemSlotIndex];
		itemSlotInfo.itemCount -= removeCount;

		inventoryItemInfos[itemSlotIndex] = itemSlotInfo;

		if (inventoryItemInfos[itemSlotIndex].itemCount <= 0)
			inventoryItemInfos[itemSlotIndex].Clear();

		if (playerInventoryWnd)
			playerInventoryWnd.UpdateInventoryItemSlots();
	}
}
