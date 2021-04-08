using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableWnd : ClosableWndBase
{

	[Header("Titlebar")]
	[SerializeField] private ClosableWndTitlebar _ClosableWndTitlebar;

	[Header("Wnd Title Text")]
	[SerializeField] private string _TitleText = "타이틀 제목";


	public ClosableWndTitlebar closableWndTitlebar => _ClosableWndTitlebar;

	private void Awake()
	{
		SetTitleText(_TitleText);

		// 닫기 버튼이 눌린 경우 이 창을 닫도록 합니다.
		_ClosableWndTitlebar.closeButton.onClick.AddListener(CloseThisWnd);
	}

	public void SetTitleText(string titleText) => _ClosableWndTitlebar.SetTitleText(titleText);



}
