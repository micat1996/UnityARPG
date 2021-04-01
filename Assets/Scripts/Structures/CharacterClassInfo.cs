
// 캐릭터 클래스 정보를 나타냅니다.
using System;

[Serializable]
public struct CharacterClassInfo
{
	// 캐릭터 클래스를 나타냅니다.
	CharacterClassType classType;

	// 캐릭터 클래스명을 나타냅니다.
	string className;

	// 클래스 설명으로 사용될 문자열을 나타냅니다.
	string classDescription;

	// 디폴트 캐릭터 프리팹 경로를 나타냅니다.
	string defaultCharacterPrefabPath;

	// 사용 가능한 헤어 스타일 코드들을 나타냅니다.
	string[] usableHairStyleCodes;

	// 기본으로 장착되는 장비 아이템 코드들을 나타냅니다.
	string[] defaultEquipItemCodes;
}