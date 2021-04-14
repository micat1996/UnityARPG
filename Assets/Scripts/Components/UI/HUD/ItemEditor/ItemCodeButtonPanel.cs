using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ItemCodeButtonPanel: MonoBehaviour
{
	[HideInInspector] public ItemEditor m_ItemEditor;
	//[HideInInspector] 
	public ItemInfo? m_ItemInfo;

	[SerializeField] private Text _Text_ItemCode;
	[SerializeField] private Button _Button_ShowItemInfo;
	[SerializeField] private Button _Button_Delete;
	[SerializeField] private Image _Image_Selected;

	private void Awake()
	{
		_Image_Selected.enabled = false;

		BindButtonEvents();

	}

	private void Start()
	{
		_Button_ShowItemInfo.onClick?.Invoke();
	}

	private void BindButtonEvents()
	{
		_Button_ShowItemInfo.onClick.AddListener(() =>
		{
			m_ItemEditor.AllItemCodeButtonSelectCancel();
			OnButtonSelected();

			m_ItemEditor.itemInfoPanel.ApplyInfo();
			m_ItemEditor.itemInfoPanel.InitializeItemInfoPanel(this, m_ItemInfo);
		});

		_Button_Delete.onClick.AddListener(() => m_ItemEditor.DeleteItemCodeButtonPanel(this));
	}

	public void OnButtonSelected()
	{
		_Image_Selected.enabled = true;

	}

	public void OnButtonUnSelected()
	{
		_Image_Selected.enabled = false;
	}

	public void SetItemCode(string newCode) => _Text_ItemCode.text = newCode;




}
