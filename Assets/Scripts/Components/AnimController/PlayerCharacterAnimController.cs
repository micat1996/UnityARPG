using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 캐릭터에 사용되는 Animator 컴포넌트를 제어하기 위한 컴포넌트입니다.
public sealed class PlayerCharacterAnimController : AnimController
{
	private PlayerableCharacter _PlayerableCharacter;

	private void Awake()
	{
		_PlayerableCharacter = GetComponent<PlayerableCharacter>();
	}

	private void Update()
	{
		if (!controlledAnimator) return;
		SetParam("_VelocityLength", _PlayerableCharacter.movement.velocity.magnitude);
		SetParam("_IsInAir", !_PlayerableCharacter.movement.isGrounded);
	}


}
