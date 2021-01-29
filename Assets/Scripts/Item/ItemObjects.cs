using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType {
    Item_Jump = 0,
    Item_Climb = 1,
    Item_left = 2,
    Item_Rush = 3,
    Item_Throw = 4,
    Item_SlotAdd = 5,
}
public class ItemObjects : MonoBehaviour {
    [SerializeField] ItemType Type;//itemType
    [SerializeField] Sprite[] itemSprite;//SettingSprites
    [SerializeField]SpriteRenderer SrChild;//getChildSprite

    void Start() {
        SrChild.sprite = itemSprite[(int)Type];
    }
    //アイテムに当たった場合、このメゾットを使ってアイテムの種類を渡すことができます！
    //If you hit an item, you can use this mezzot to pass on the type of item!
    public ItemType GetItemType() { return Type; }
}
