using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEditorScreenInstance : ScreenInstance
{
	protected override void Awake()
	{
		base.Awake();

		FloatingItemEditor();
	}

	// 아이템 에디터를 띄웁니다.
	private void FloatingItemEditor()
	{
		var itemEditorHUD = CreateChildHUD(
			ResourceManager.Instance.LoadResource<GameObject>(
				"", "Prefabs/ScreenInstance/ItemEditor/Panel_ItemEditor", false).GetComponent<ItemEditor>());

		RectTransform itemEditorRectTransform = (itemEditorHUD.transform as RectTransform);
		itemEditorRectTransform.offsetMin = itemEditorRectTransform.offsetMax = Vector2.zero;
	}
}
