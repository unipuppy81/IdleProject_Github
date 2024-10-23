using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPopupEquipSlots : MonoBehaviour
{

    #region Value Fields
    private int _needCount;
    private ItemTier _rarity;
    #endregion

    #region Object Fields

    public Action SetReinforceUI;

    [SerializeField] private TextMeshProUGUI _lvTxt;
    [SerializeField] private GameObject _equippdText;

    [SerializeField] private Image reinforceProgress;
    [SerializeField] private TextMeshProUGUI reinforceText;

    [SerializeField] private Image itemSprite;

    [SerializeField] private GameObject lockCover;
    [SerializeField] private GameObject lockIcon;

    [SerializeField] private GameObject ReinforceIcon;

    private UserItemData _itemData;
    public UserItemData ItemData => _itemData;

    #endregion

    #region Unity Flow

    private void Awake()
    {
        SetReinforceUI += SetReinforceData;
        SetReinforceUI += SetReinforceProgress;
        SetReinforceUI += SetReinforceIcon;
    }

    private void OnDestroy()
    {
        SetReinforceUI -= SetReinforceData;
        SetReinforceUI -= SetReinforceProgress;
        SetReinforceUI -= SetReinforceIcon;
    }

    #endregion

    #region Other Method

    public void InitSlotInfo(UserItemData itemData)
    {
        _itemData = itemData;

        _rarity = Manager.Inventory.ItemDataDictionary[itemData.itemID].Rarity;
        GetComponent<Image>().color = Utilities.SetSlotTierColor(_rarity);

        _lvTxt.text = $"Lv. {_itemData.level}";
    }

    public void InitSlotUI()
    {
        _lvTxt.text = $"Lv : {_itemData.level}";

        itemSprite.sprite = Manager.Inventory.ItemDataDictionary[_itemData.itemID].Sprite;

        SetLockState();
        gameObject.GetComponent<Button>().onClick.AddListener(SendItemData);
    }

    public void CheckEquipState()
    {
        if (_itemData.equipped == false)
        {
            _equippdText.SetActive(false);
        }
        else
        {
            _equippdText.SetActive(true);
        }
    }

    private void SetReinforceData()
    {
        _lvTxt.text = $"Lv. {_itemData.level}";

        if (_itemData.level < 15)
        {
            _needCount = _itemData.level + 1;
        }
        else
        {
            _needCount = 15;
        }
    }

    private void SetReinforceProgress()
    {
        reinforceProgress.fillAmount = (float)_itemData.hasCount / _needCount;
        reinforceText.text = $"{_itemData.hasCount} / {_needCount}";
    }

    private void SetReinforceIcon()
    {
        if (_itemData.itemID[0] == 'W')
        {
            if (_itemData.itemID == Manager.Data.WeaponInvenList.Last().itemID & _itemData.level >= 100)
            {
                ReinforceIcon.SetActive(false);
            }
            else if (_itemData.hasCount < _needCount)
            {
                ReinforceIcon.SetActive(false);
            }
            else
            {
                ReinforceIcon.SetActive(true);
            }
        }
        else if (_itemData.itemID[0] == 'A')
        {
            if (_itemData.itemID == Manager.Data.ArmorInvenList.Last().itemID & _itemData.level >= 100)
            {
                ReinforceIcon.SetActive(false);
            }
            else if (_itemData.hasCount < _needCount)
            {
                ReinforceIcon.SetActive(false);
            }
            else
            {
                ReinforceIcon.SetActive(true);
            }
        }
    }

    public void SetLockState()
    {
        if (_itemData.level > 1 || _itemData.hasCount > 0)
        {
            lockCover.SetActive(false);
            lockIcon.SetActive(false);
            return;
        }

        lockCover.SetActive(true);
        lockIcon.SetActive(true);
    }

    private void SendItemData()
    {
        (Manager.UI.CurrentPopup as UIPopupEquipment).SetSelectItemInfo(_itemData);
    }

    #endregion
}
