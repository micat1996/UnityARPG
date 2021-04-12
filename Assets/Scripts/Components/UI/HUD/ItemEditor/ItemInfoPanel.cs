using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ItemInfoPanel : MonoBehaviour
{


	[SerializeField] private InputField _InputField_ItemCode;
	[SerializeField] private Button _Button_SlotBackground;
	[SerializeField] private Image _Image_ItemImage;
	[SerializeField] private InputField _InputField_ItemName;
	[SerializeField] private InputField _InputField_ItemImagePath;
	[SerializeField] private Dropdown _Dropdown_ItemType;
	[SerializeField] private InputField _InputField_ItemDescription;
	[SerializeField] private InputField _InputField_ItemMaxCount;
	[SerializeField] private InputField _InputField_ItemPrice;

	private ItemCodeButtonPanel _ConnectedItemCodeButtonPanel;

	private void Awake()
	{
		_InputField_ItemCode.onValueChanged.AddListener((text) =>
		{
			if (string.IsNullOrEmpty(_InputField_ItemCode.text)) return;
			_InputField_ItemImagePath.text = $"Image/ItemImage/{_InputField_ItemCode.text}";
		});

		_Button_SlotBackground.onClick.AddListener(() =>
		{
			Texture2D loadedTexture = ResourceManager.Instance.LoadResource<Texture2D>(
				"", _InputField_ItemImagePath.text, false);

			if (loadedTexture == null) return;

			Rect rect = new Rect(0.0f, 0.0f, loadedTexture.width, loadedTexture.height);
			_Image_ItemImage.sprite = Sprite.Create(loadedTexture, rect, Vector2.one * 0.5f);
		});
	}


	// 아이템 정보 패널을 초기화합니다.
	public void InitializeItemInfoPanel(ItemCodeButtonPanel itemCodeButtonPanel, ItemInfo? itemInfo)
	{
		_ConnectedItemCodeButtonPanel = itemCodeButtonPanel;

		if (itemInfo.HasValue)
		{
			_InputField_ItemCode.text = itemInfo.Value.itemCode;
			_InputField_ItemName.text = itemInfo.Value.itemName;
			_InputField_ItemImagePath.text = itemInfo.Value.itemImagePath;
			_InputField_ItemDescription.text = itemInfo.Value.itemDescription;
			_InputField_ItemMaxCount.text = itemInfo.Value.maxSlotCount.ToString();
			_InputField_ItemPrice.text = itemInfo.Value.price.ToString();
		}
		else
		{
			_InputField_ItemCode.text =
				_InputField_ItemName.text =
				_InputField_ItemImagePath.text =
				_InputField_ItemDescription.text =
				_InputField_ItemMaxCount.text =
				_InputField_ItemPrice.text = "";
		}
	}

	public void ApplyInfo()
	{
		if (!_ConnectedItemCodeButtonPanel.m_ItemInfo.HasValue) return;
		if (_ConnectedItemCodeButtonPanel.m_ItemInfo.Value.isEmpty) return;

		_ConnectedItemCodeButtonPanel.m_ItemInfo = new ItemInfo(
			_InputField_ItemCode.text,
			(ItemType)_Dropdown_ItemType.value,
			_InputField_ItemName.text,
			_InputField_ItemDescription.text,
			_InputField_ItemImagePath.text,
			int.Parse(_InputField_ItemMaxCount.text),
			int.Parse(_InputField_ItemPrice.text));
	}




}
