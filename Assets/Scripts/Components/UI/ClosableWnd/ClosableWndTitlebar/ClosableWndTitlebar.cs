using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class ClosableWndTitlebar : MonoBehaviour, IDragHandler, IBeginDragHandler
{
	// 타이틀 제목 텍스트를 나타냅니다.
	[SerializeField] private TextMeshProUGUI _TMP_Title;

	// 닫기 버튼을 나타냅니다.
	[SerializeField] private Button _Button_Close;

	// 이동시킬 ClosableWnd 를 나타냅니다.
	private ClosableWndBase _ClosableWnd;

	public RectTransform rectTransform => (transform as RectTransform);
	public Button closeButton => _Button_Close;

	// 이전 입력 위치를 저장할 변수
	private Vector2 _PrevInputPosition;
	/// - 다음 드래그 위치를 계산하기 위해 사용됩니다.

	private void Awake()
	{
		_ClosableWnd = transform.parent.GetComponent<ClosableWndBase>();

		FillHorizontal();
	}

	// 드래깅이 시작되었을 때 호출되는 콜백
	public void OnBeginDrag(PointerEventData eventData) =>
		// 입력된 위치를 저장합니다.
		_PrevInputPosition = eventData.position;

	// 드래깅중 호출되는 콜백
	public void OnDrag(PointerEventData eventData)
	{
		// 마우스 커서 위치가 화면 외부에 위치해 있는지 확인합니다.
		/// - 화면의 왼쪽 하단에 더하고, 오른쪽 상단에 서 빼 계산할 값.
		bool IsCursorOut(Vector2 margin)
		{
			// 화면 왼쪽 하단, 오른쪽 상단 위치
			Vector2 screenLB = margin;
			Vector2 screenRT = new Vector2(GameStatics.screenSize.width, GameStatics.screenSize.height) - margin;

			// 마우스 입력 위치
			Vector2 inputPosition = (eventData.position / GameStatics.screenRatio);

			return
				(screenRT.x < inputPosition.x || inputPosition.x < screenLB.x) ||
				(screenRT.y < inputPosition.y || inputPosition.y < screenLB.y);
		}

		// 커서가 화면 내부에 위치해 있지 않다면 실행하지 않습니다.
		if (IsCursorOut(Vector2.one * 10.0f)) return;

		// 현재 입력 위치 저장합니다.
		Vector2 currentInputPosition = eventData.position;


		// 이동시킬 UI 의 위치를 설정합니다.
		_ClosableWnd.rectTransform.anchoredPosition +=
			(currentInputPosition - _PrevInputPosition) / GameStatics.screenRatio;
		/// - 얼만큼 이동했는지를 확인하고(현재 위치 - 이전 위치) 화면비를 연산하여
		///   UI 위치에 더합니다.

		// 다음 연산을 위하여 현재 위치를 저장합니다.
		_PrevInputPosition = currentInputPosition;
	}

	// 타이틀 텍스트를 설정합니다.
	public void SetTitleText(string titleText) => _TMP_Title.SetText(titleText);

	public void FillHorizontal()
	{
		if (_ClosableWnd == null)
			_ClosableWnd = transform.parent.GetComponent<ClosableWndBase>();

		// 타이틀바 가로 크기를 창 가로 크기에 맞춥니다.
		Vector2 titlebarSize = rectTransform.sizeDelta;
		titlebarSize.x = _ClosableWnd.rectTransform.sizeDelta.x;
		rectTransform.sizeDelta = titlebarSize;
	}


}
