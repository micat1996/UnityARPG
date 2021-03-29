using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWnd : ClosableWndBase
{

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("spavce");
			m_ScreenInstance.CloseWnd(false, this);
		}
	}
}
