using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenInstance : ScreenInstance
{



	protected override void Awake()
	{
		base.Awake();

		var testWnd = ResourceManager.Instance.LoadResource<GameObject>(
			"Wnd_Test",
			"Prefabs/Wnds/Wnd_Test").GetComponent<TestWnd>();

		CreateWnd(testWnd);
	}
}
