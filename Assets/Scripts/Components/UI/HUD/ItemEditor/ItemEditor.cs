using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ItemEditor : MonoBehaviour
{

	[SerializeField] private Button _Button_AddItemInfo;
	[SerializeField] private Transform _Panel_ItemCodes;
	[SerializeField] private ItemInfoPanel _Panel_ItemInfo;

	private ItemCodeButtonPanel _Panel_ItemCodeButtonPrefab;

	private List<ItemCodeButtonPanel> _ItemCodePanels = new List<ItemCodeButtonPanel>();

	public ItemInfoPanel itemInfoPanel => _Panel_ItemInfo;

	private void Awake()
	{
		_Panel_ItemCodeButtonPrefab = ResourceManager.Instance.LoadResource<GameObject>(
			"_Panel_ItemCodeButtonPrefab",
			"Prefabs/ScreenInstance/ItemEditor/Panel_ItemCodeButton").GetComponent<ItemCodeButtonPanel>();


		BindButtonEvents();

		// 모든 아이템 정보를 읽어들입니다.
		ReadAllItemInfos();
	}

	private void BindButtonEvents()
	{
		_Button_AddItemInfo.onClick.AddListener(() => { AddItemCodeButton(null); });
	}

	// 모든 아이템 정보를 읽어들입니다.
	private void ReadAllItemInfos()
	{
		bool fileNotFound;
		var findedItemDatas = ResourceManager.Instance.LoadJsonAll<ItemInfo>("ItemInfos", out fileNotFound);

		// 아이템을 읽지 못했다면 실행 취소
		if (fileNotFound)
		{
			AddItemCodeButton(null);
			return;
		}


		// 읽어들인 정보를 바탕으로 아이템 코드 버튼을 추가합니다.
		foreach(var itemData in findedItemDatas)
			AddItemCodeButton(itemData);
	}

	// 아이템 코드 버튼을 추가합니다.
	private void AddItemCodeButton(ItemInfo? newItemInfo)
	{
		var newItemCodeButtonPanel = Instantiate(_Panel_ItemCodeButtonPrefab, _Panel_ItemCodes);
		newItemCodeButtonPanel.m_ItemEditor = this;
		_ItemCodePanels.Add(newItemCodeButtonPanel);

		newItemCodeButtonPanel.m_ItemInfo = (newItemInfo.HasValue) ? newItemInfo : null;
	}

	public void AllItemCodeButtonSelectCancel()
	{
		foreach(var btn in _ItemCodePanels)
			btn.OnButtonUnSelected();
	}

	public void DeleteItemCodeButtonPanel(ItemCodeButtonPanel itemCodeButtonPanel)
	{
		_ItemCodePanels.Remove(itemCodeButtonPanel);
		Destroy(itemCodeButtonPanel.gameObject);
	}

}
