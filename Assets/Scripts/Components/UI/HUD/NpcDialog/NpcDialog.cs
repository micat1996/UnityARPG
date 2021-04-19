using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public sealed class NpcDialog : MonoBehaviour
{
	[Header("Npc Name")]
	[SerializeField] private TextMeshProUGUI _TMP_NpcName;
	[SerializeField] private Image _Image_NpcNameBackgournd;

	[Header("Menu Buttons")]
	[SerializeField] private Button _Button_GoToFirst;
	[SerializeField] private Button _Button_Shop;
	[SerializeField] private Button _Button_Close;
	[SerializeField] private Button _Button_NextDialog;

	[Header("Dialog")]
	[SerializeField] private TextMeshProUGUI _TMP_DialogText;

	private static NpcShopWnd _NpcShopWndPrefab;

	// 이 Dialog 와 연결된 Npc 를 나타냅니다.
	private Npc _ConnectedNpc;

	// 이름 텍스트와 같이 사용되는 ContentSizeFitter 컴포넌트를 나타냅니다.
	/// - 이름 길이에 따라 이름이 표시되는 UI 요소의 너비를 설정하기 위해 사용됩니다.
	private ContentSizeFitter _TMP_NpcNameContentSizeFitter;

	// 현재 사용되는 ScreenInstance 를 나타냅니다.
	private GameScreenInstance _GameScreenInstance;

	#region Fields used in the dialog...
	// 표시되는 대화 정보를 나타냅니다.
	private NpcDialogInfo _DialogInfos;

	// 현재 몇 번째 대화를 표시하는 지 나타냅니다.
	private int _CurrentDialogIndex;

	// 마지막 대화임을 나타냅니다.
	private bool _IsLastDialog;
	#endregion

	#region WndInstances...
	// 생서된 상점 창을 나타냅니다.
	private NpcShopWnd _NpcShopWnd;
	#endregion

	// 이 HUD 의 RectTransform 을 나타냅니다.
	public RectTransform rectTransform => transform as RectTransform;

	// NPC 대화창이 닫힐 때 발생하는 이벤트
	//public System.Action onDlgClosed;
	public event System.Action onDlgClosedEvent;
	/// 이벤트란?
	/// 선언된 클래스 내에섭만 호출할 수 있는 Multicat Delegate 입니다.
	/// 이벤트의 접근자를 구성할 때에는, property 를 구성할 때와 조금 다르게 
	/// get; set; 대신 add, remove 를 사용합니다.
	/// 
	/// 대리자와 다른 점
	/// - 대리자는 public 으로 선언되어 있다면 클래스 외부에서도 호출할 수 있지만,
	///   이벤트는 public 으로 선언되어 있어도 클래스 외부에서는 호출할 수 없습니다.
	/// - 대리자는 인터페이스에도 선언될 수 있지만, 대리자는 인터페이스에 선언될 수 없습니다.
	/// - 이벤트와 대리자는 콜백 용도로 사용된다는 것은 동일하나,
	///   이벤트는 객체 자신의 상태 변화나 특정한 사건의 발생을 다른 객체에게 알리는 용도로 사용합니다.
	/// - 콜백 함수 : 다른 함수에 인수로 들어갈 수 있는 함수를 의미하며
	///   호출자가 피호출자를 호출하는 것이 아닌, 피호출자가 호출자를 호출하는 방식을 의미합니다.
	  


	private void Awake()
	{
		if (!_NpcShopWndPrefab)
		{
			_NpcShopWndPrefab = ResourceManager.Instance.LoadResource<GameObject>(
				"Wnd_NpcShop",
				"Prefabs/UI/Wnds/NpcShopWnd/NpcShopWnd").GetComponent<NpcShopWnd>();
		}

		// ContentSizeFitter 를 얻습니다.
		_TMP_NpcNameContentSizeFitter = _TMP_NpcName.GetComponent<ContentSizeFitter>();
		
		// ScreenInstance 를 얻습니다.
		_GameScreenInstance = (PlayerManager.Instance.playerController.screenInstance as GameScreenInstance);

		// 버튼 클릭 이벤트 바인딩
		BindButtonEvents();
	}

	// 버튼 클릭 이벤트를 바인딩합니다.
	private void BindButtonEvents()
	{
		// 처음으로 버튼을 눌렀을 때 호출할 메서드를 정의합니다.
		_Button_GoToFirst.onClick.AddListener(InitializeDialog);

		// 상점 버튼을 눌렀을 때 실행할 내용을 정의합니다.
		_Button_Shop.onClick.AddListener(
			() =>
			{
				// 이미 상점 창이 열려있다면 실행하지 않습니다.
				if (_NpcShopWnd != null) return;

				// FadeOut 효과
				_GameScreenInstance.effectController.PlayAnimation(ScreenEffectType.ScreenFadeOut);

				// 대화 상자를 초기화합니다.
				InitializeDialog();

				// 상점 창을 엽니다.
				var gameScreenInstance = (PlayerManager.Instance.playerController.screenInstance as GameScreenInstance);
				_NpcShopWnd = gameScreenInstance.CreateWnd(_NpcShopWndPrefab) as NpcShopWnd;

				// 상점 정보를 읽습니다.
				ShopInfo shopInfo = ResourceManager.Instance.LoadJson<ShopInfo>(
					"ShopInfos", $"{_ConnectedNpc.npcInfo.shopCode}.json");

				// 상점 창 초기화
				_NpcShopWnd.InitializeNpcShop(shopInfo);

				// 상점 창이 닫힐 때 실행할 내용을 정의합니다.
				_NpcShopWnd.onWndClosedEvent += () => _NpcShopWnd = null;
			});

		// 닫기 버튼을 눌렀을 때 실행할 내용을 정의합니다.
		_Button_Close.onClick.AddListener(
			() => 
			{
				// FadeOut 효과
				_GameScreenInstance.effectController.PlayAnimation(ScreenEffectType.ScreenFadeOut);

				// 대화 상자를 초기화합니다.
				InitializeDialog();

				// 대화 상자가 닫힘 이벤트 발생
				onDlgClosedEvent?.Invoke();
				PlayerManager.Instance.playerController.screenInstance.CloseChildHUD(rectTransform);
			});

		// 다음 버튼을 눌렀을 때 실행할 내용을 정의합니다.
		_Button_NextDialog.onClick.AddListener(NextDialog);
	}

	// NpcDialog 를 초기화합니다.
	public void InitializeNpcDialog(Npc ownerNpc)
	{
		_ConnectedNpc = ownerNpc;

		// Npc 이름 설정
		SetNpcName(_ConnectedNpc.npcInfo.npcName);

		// Dialog 초기화
		InitializeDialog();
	}

	// NpcDialog 를 초기 상태로 되돌립니다.
	private void InitializeDialog()
	{
		_IsLastDialog = false;

		// FadeOut 효과
		_GameScreenInstance.effectController.PlayAnimation(ScreenEffectType.ScreenFadeOut);

		// 열린 모든 창을 닫습니다.
		_GameScreenInstance.CloseWnd(true);

		// 기본 대화 내용을 표시합니다.
		_DialogInfos = _ConnectedNpc.npcInfo.defaultDialogInfo;

		// 대화 순서를 처음으로 되돌립니다.
		_CurrentDialogIndex = 0;

		// 대화 내용 표시
		ShowDialog(_CurrentDialogIndex);
	}

	// 표시되는 Npc 이름을 변경합니다.
	private void SetNpcName(string npcName)
	{
		_TMP_NpcName.text = npcName;

		// _TMP_NpcNameContentSizeFitter 의 너비를 갱신합니다.
		_TMP_NpcNameContentSizeFitter.SetLayoutHorizontal();

		// 배경 크기가 글자 사이즈와 딱 맞게 적용되지 않고, 좌우에 여백을 줍니다.
		_Image_NpcNameBackgournd.rectTransform.sizeDelta = 
			_TMP_NpcName.rectTransform.sizeDelta + (Vector2.right * 60.0f);

		// 글자를 배경 중앙으로 배치합니다.
		_TMP_NpcName.rectTransform.anchoredPosition += Vector2.right * 30.0f;
	}

	// 지정한 순서의 대화를 표시합니다.
	private void ShowDialog(int newDialogIndex)
	{
		// 대화 텍스트를 설정합니다.
		void SetDialogText(string dlgText)
		{
			_TMP_DialogText.text = dlgText;
			ContentSizeFitter dlgTextSizeFitter = _TMP_DialogText.GetComponent<ContentSizeFitter>();
			dlgTextSizeFitter.SetLayoutHorizontal();
			dlgTextSizeFitter.SetLayoutVertical();

			(_TMP_DialogText.transform.parent as RectTransform).sizeDelta = _TMP_DialogText.rectTransform.sizeDelta;
		}

		// 사용할 수 있는 대화가 존재하지 않는다면
		if (_DialogInfos.dialogText.Count == 0)
		{
#if UNITY_EDITOR
			Debug.LogError("Usable Dialog Count is Zero!");
#endif
			return;
		}

		if (_DialogInfos.dialogText.Count <= newDialogIndex)
		{
			Debug.LogError($"Out Of Range! newDialogIndex is Changed. ({newDialogIndex} -> {_DialogInfos.dialogText.Count - 1})");
			newDialogIndex = _DialogInfos.dialogText.Count - 1;
		}

		// 대화 텍스트 설정
		SetDialogText(_DialogInfos.dialogText[newDialogIndex]);

		// 마지막 대화 설정
		_IsLastDialog = (_DialogInfos.dialogText.Count - 1) == newDialogIndex;

		// 마지막 대화라면 다음 대화 버튼 숨깁니다.
		SetDialogButtonVisibility(!_IsLastDialog);
	}

	// 다음 대화를 표시합니다.
	private void NextDialog()
	{
		if ((_DialogInfos.dialogText.Count - 1) <= _CurrentDialogIndex)
			return;

		ShowDialog(++_CurrentDialogIndex);
	}

	// 다음 대화 버튼의 가시성을 설정합니다.
	private void SetDialogButtonVisibility(bool bVisible) =>
		_Button_NextDialog.gameObject.SetActive(bVisible);

}
