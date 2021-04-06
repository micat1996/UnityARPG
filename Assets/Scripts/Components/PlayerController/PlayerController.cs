using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerControllerBase
{
	private PlayerCharacterInfo _PlayerCharacterInfo;
	public ref PlayerCharacterInfo playerCharacterInfo => ref _PlayerCharacterInfo;

	protected override void Awake()
	{
		_PlayerCharacterInfo = new PlayerCharacterInfo();
		_PlayerCharacterInfo.Initialize();

		base.Awake();

		//_PlayerCharacterInfo = ResourceManager.Instance.LoadResource<PlayerCharacterInfo>("")
	}

}
