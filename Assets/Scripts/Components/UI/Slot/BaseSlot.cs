using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private TextMeshProUGUI _TMP_Count;
	[SerializeField] private Image _Image_Slot;

	private ScreenInstance _ScreenInstance;

	protected Button m_Button_Slot;

	// 슬롯 타입을 나타냅니다.
	protected SlotType m_SlotType;

	// 아이템 슬롯과 함께 사용되는 코드를 나타냅니다.
	protected string m_InCode;

	public Image slotImage => _Image_Slot;

	// 드래깅을 사용할 것인지를 나타냅니다.
	protected bool m_UseDragDrop = false;

	// 드래깅이 시작되었을 때 발생하는 이벤트입니다.
	public event System.Action<DragDropOperation, SlotDragVisual> onSlotBeginDragEvent;
	/// - DragDropOperation : 드래그 드랍 작업 객체가 전달됩니다.
	/// - SlotDragVisual : 드래그 비쥬얼 객체가 전달됩니다.


	// 투명한 이미지를 나타냅니다.
	protected static Texture2D m_T_NULL;

	// 드래깅 비쥬얼 프리팹을 나타냅니다.
	private static SlotDragVisual _SlotDragVisualPrefab;



	protected virtual private void Awake()
	{
		if (!m_T_NULL)
		{
			m_T_NULL = ResourceManager.Instance.LoadResource<Texture2D>(
				"m_T_Null", "Image/Slot/T_NULL");
		}

		if (!_SlotDragVisualPrefab)
		{
			_SlotDragVisualPrefab = ResourceManager.Instance.LoadResource<GameObject>(
				"SlotDragVisual",
				"Prefabs/UI/Slot/SlotDragVisual").GetComponent<SlotDragVisual>();
		}

		_ScreenInstance = PlayerManager.Instance.playerController.screenInstance;

		m_Button_Slot = GetComponent<Button>();

		SetSlotItemCount(0);
	}

	// 슬롯을 초기화합니다.
	public virtual void InitializeSlot(SlotType slotType, string inCode)
	{
		m_SlotType = slotType;
		m_InCode = inCode;
		
	}

	// 슬롯에 표시되는 숫자를 설정합니다.
	/// - itemCount : 표시시킬 아이템 개수를 전달합니다.
	/// - visibleLessThan2 : 2 개 미만일 경우에도 숫자를 표시할 것인지를 전달합니다.
	public void SetSlotItemCount(int itemCount, bool visibleLessThan2 = false) =>
		_TMP_Count.text = (itemCount >= 2 || visibleLessThan2) ? itemCount.ToString() : "";

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		// 드래그 드랍을 사용하지 않는다면 실행하지 않습니다.
		if (!m_UseDragDrop) return;

		// 드래깅 작업을 끝냅니다.
		_ScreenInstance.FinishDragDropOperation();
	}

	void IDragHandler.OnDrag(PointerEventData eventData) { }
	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		// 드래그 드랍을 사용하는 경우에만 실행합니다.
		if (m_UseDragDrop)
		{
			// 드래그 비쥬얼을 생성합니다.
			SlotDragVisual dragVisual = Instantiate(_SlotDragVisualPrefab, _ScreenInstance.rectTransform);

			// 드래그 드랍 작업을 시작합니다.
			_ScreenInstance.StartDragDropOperation(new DragDropOperation(this, dragVisual.rectTransform));

			// 드래그 시작을 알립니다.
			onSlotBeginDragEvent?.Invoke(_ScreenInstance.dragDropOperation, dragVisual);
		}
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		// 드래그 드랍을 사용하지 않는다면 실행하지 않습니다.
		if (!m_UseDragDrop) return;
			// UI 영역과 겹침 끝남
			_ScreenInstance.dragDropOperation?.OnPointerExit(this);
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		// 드래그 드랍을 사용하지 않는다면 실행하지 않습니다.
		if (!m_UseDragDrop) return;
			// UI 영역과 겹침
			_ScreenInstance.dragDropOperation?.OnPointerEnter(this);
	}
}
