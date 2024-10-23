using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIPopupSkillSlotsInven : MonoBehaviour
{
    #region Value Fields

    private int _needCount;
    private ItemTier _rarity;

    #endregion

    #region Object Fields

    public Action SetReinforceUI;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject equippdText;

    [SerializeField] private Image reinforceProgressSprite;
    [SerializeField] private TextMeshProUGUI reinforceProgressText;

    [SerializeField] private Image itemSprite;

    [SerializeField] private GameObject lockCover;
    [SerializeField] private GameObject lockIcon;

    [SerializeField] private GameObject reinforceIcon;


    private UserInvenSkillData _skillData;
    public UserInvenSkillData SkillData => _skillData;

    #endregion

    #region Unity Flow

    private void Awake()
    {
        SetReinforceUI += SetReinforceData;
        SetReinforceUI += SetUIReinforceIcon;
    }

    private void OnDestroy()
    {
        SetReinforceUI -= SetReinforceData;
        SetReinforceUI -= SetUIReinforceIcon;
    }

    #endregion

    #region Other Method

    //아이템 아이콘 세팅, 티어 세팅, 레벨 세팅,게이지 세팅, 언록 여부 
    public void InitSlotInfo(UserInvenSkillData skillData)
    {
        _skillData = skillData;

        _rarity = Manager.SkillData.SkillDataDictionary[skillData.itemID].Rarity;
        GetComponent<Image>().color = Utilities.SetSlotTierColor(_rarity);
    }

    public void InitSlotUI()
    {
        itemSprite.sprite = Manager.SkillData.SkillDataDictionary[_skillData.itemID].Sprite;
        gameObject.GetComponent<Button>().onClick.AddListener(ShowPopupSkillDetailInfo);

        SetUILockState();
    }

    public void SetReinforceData()
    {
        levelText.text = $"Lv. {_skillData.level}";
        _needCount = _skillData.level < 15 ? _skillData.level + 1 : 15;
        reinforceProgressText.text = $"{_skillData.hasCount} / {_needCount}";
        reinforceProgressSprite.fillAmount = (float)_skillData.hasCount / _needCount;
    }

    public void SetUIEquipState()
    {
        if (_skillData.equipped == false)
        {
            equippdText.SetActive(false);
        }
        else
        {
            equippdText.SetActive(true);
        }
    }

    public void SetUIReinforceIcon()
    {
        if (_skillData.itemID == Manager.Data.SkillInvenList.Last().itemID & _skillData.level >= 100)
        {
            reinforceIcon.SetActive(false);
        }
        else if (SkillData.hasCount < _needCount)
        {
            reinforceIcon.SetActive(false);
        }
        else
        {
            reinforceIcon.SetActive(true);
        }
    }

    public void SetUILockState()
    {
        if (_skillData.level > 1 || _skillData.hasCount > 0)
        {
            lockCover.SetActive(false);
            lockIcon.SetActive(false);
            return;
        }

        lockCover.SetActive(true);
        lockIcon.SetActive(true);
    }

    private void ShowPopupSkillDetailInfo()
    {
        var instancePopup =  Manager.UI.ShowPopup<UIPopupSkillDetail>();
        instancePopup.SetSkillData(_skillData);
    }

    #endregion
}
