using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClosableWndTitlebar), true)]
public sealed class WndTitlebar_UpdateSizeButton:Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		// 재정의할 컴포넌트 인스턴스를 얻습니다.
		ClosableWndTitlebar component = target as ClosableWndTitlebar;

		// 버튼 추가
		if (GUILayout.Button("Fill Horizontal"))
			AttachToGround(component);
	}

	private void AttachToGround(ClosableWndTitlebar component) =>
		component.FillHorizontal();
}
