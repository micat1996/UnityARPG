using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterEquipController: MonoBehaviour
{
    [SerializeField] private List<EquipSocket> _EquipSockets;

    public List<EquipItemInfo> equipInfos;


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
