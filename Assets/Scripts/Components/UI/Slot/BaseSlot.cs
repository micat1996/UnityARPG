using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseSlot : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _TMP_Count;
	[SerializeField] private Image _Image_Slot;

	protected Button m_Button_Slot;

	// 투명한 이미지를 나타냅니다.
	protected Texture2D m_T_NULL;

	// 슬롯 타입을 나타냅니다.
	protected SlotType m_SlotType;

	// 아이템 슬롯과 함께 사용되는 코드를 나타냅니다.
	protected string m_InCode;

	public Image slotImage => _Image_Slot;

	protected virtual private void Awake()
	{
		m_T_NULL = ResourceManager.Instance.LoadResource<Texture2D>(
			"m_T_Null", "Image/Slot/T_NULL");

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

}
