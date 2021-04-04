using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 파츠 장착을 제어하는 컴포넌트입니다.
/// - 모든 파츠는 특정한 부모 오브젝트 하위로 추가되어야 합니다.
/*
+ Head
얼굴, 헤어, 머리, 헬멧

null
바디

+ Back
망토, 가방

+ L Hand
보조 무기

+ R Hand
주 무기
 */

public sealed class PlayerCharacterEquipController: MonoBehaviour
{
    // 파츠가 장착될 부모 트랜스폼을 나타냅니다.
    /// - key : 장착될 파츠를 나타냅니다.
    /// - value : 파츠의 부모 트랜스폼을 나타냅니다.
    private Dictionary<PartsType, Transform> PartsSockets;

    [SerializeField] private List<EquipSocket> _EquipSockets;

    public List<EquipItemInfo> equipInfos;

    private void Awake()
    {
        InitializeSockets();
    }

    private void InitializeSockets()
    {
        // 각 파츠의 부모 오브젝트로 사용될 오브젝트의 이름을 저장합니다.
        /// - "Owner" 는 Playerable Character 오브젝트 하위로 추가될 오브젝트이며, 파츠가 장착될 때
        ///   이 오브젝트는 사라지도록 합니다.
        List<(PartsType partsType, string name)> partsDataToFind = new List<(PartsType, string)>();
        partsDataToFind.Add((PartsType.Body,        "Owner"));

        partsDataToFind.Add((PartsType.Face,        "+ Head"));
        partsDataToFind.Add((PartsType.Hair,        "+ Head"));
        partsDataToFind.Add((PartsType.Head,        "+ Head"));
        partsDataToFind.Add((PartsType.Helmet,      "+ Head"));

        partsDataToFind.Add((PartsType.Cape,        "+ Back"));
        partsDataToFind.Add((PartsType.Backpack,    "+ Back"));

        partsDataToFind.Add((PartsType.LeftWeapon,  "+ L Hand"));
        partsDataToFind.Add((PartsType.RightWeapon, "+ R Hand"));

        // 모든 파츠의 부모 오브젝트를 찾습니다.
        foreach(var partsData in partsDataToFind)
            FindParentTransform(transform, partsData.partsType, partsData.name);
    }

    // 파츠가 장착될 때 부모 오브젝트로 사용될 오브젝트를 찾습니다.
    /// - parent : 탐색의 기준이 되는 부모 오브젝트를 전달합니다.
    ///   ex) parent
    ///         child1
    ///         child2
    ///       에서 child1 을 찾을 때 parent 가 전달되어야 합니다.
    /// - partsType : 부모 오브젝트로 사용될 파츠 타입을 전달합니다.
    /// - name : 찾을 부모 오브젝트를 전달합니다.
    private void FindParentTransform(Transform parent, PartsType partsType, string name)
    {
        // 탐색의 기준이 되는 부모 오브젝트의 모든 자식 오브젝트를 확인합니다.
        for (int i = 0; i < parent.childCount; ++i)
        /// - transform.childCount : transform 의 자식 오브젝트 개수를 나타냅니다.
        ///   이 때 transform 의 자식 오브젝트가 갖는 자식 오브젝트를 포함되지 않습니다.
        ///   ex) child1
        ///        child_1
        ///          child_1_1
        ///          child_1_2
        ///        child_2
        ///          child_2_1
        ///          child_2_2
        ///       위와 같은 경우 child1 의 자식 오브젝트 개수 : 2
        {
            Transform childTransform = parent.GetChild(i);
            /// - transform.GetChild(index) : transform 의 index 번째 자식 오브젝트를 얻습니다.

            // 자식 오브젝트의 이름이 찾고자 하는 부모 오브젝트와 일치하는지 확인합니다.
            if (childTransform.gameObject.name == name)
            {
                PartsSockets.Add(partsType, childTransform);
                break;
            }

            // 찾고자 하는 오브젝트를 찾지 못했을 경우 자식 오브젝트에 찾고자 하는 오브젝트가 존재하는지 확인합니다.
            else FindParentTransform(parent.GetChild(i), partsType, name);
        }
    }

    private void Update()
    {

    }

    // Start is called before the first frame update
    void OnDestroy()
    {
        //foreach (var info in equipInfos)
        //    ResourceManager.Instance.SaveJson(info, $"{info.itemCode}.json");
    }
}
