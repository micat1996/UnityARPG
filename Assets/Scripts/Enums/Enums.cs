
// 캐릭터 클래스 타입을 나타낼 때 사용되는 열거 형식입니다.
public enum CharacterClassType { ClassAll, Warrior, Wizard }

// 입력 모드를 나타낼 때 사용되는 열거 형식입니다.
public enum InputMode { GameOnly, UIOnly, GameAndUI }

public enum ScreenEffectType
{
	ScreenFadeOut
}

// 슬롯의 타입을 나타내기 위해 사용되는 열거 형식입니다.
public enum SlotType
{ 
	ShopItemSlot,
	InventorySlot
}

// 아이템 구매 / 판매 시 사용되는 열거 형식입니다.
public enum TradeSeller
{
	ShopKeeper,
	Player
}

// 아이템 타입을 나타낼 때 사용되는 열거 형식입니다.
public enum ItemType
{
	EtCetera,
	Consumption,
	Equipment
}

// 아이템 등급을 나타낼 때 사용되는 열거 타입입니다.
public enum ItemLevel
{
	Normal,
	Rare,
	Epic,
	Unique,
	Master,
	Legendary
}

// 장비 아이템 장착 부위를 나타내기 위한 열거 형식입니다.
public enum PartsType
{ 
	Face,
	Hair,
	Head,
	Body,

	Helmet,
	Backpack,

	LeftWeapon,
	RightWeapon,
}


