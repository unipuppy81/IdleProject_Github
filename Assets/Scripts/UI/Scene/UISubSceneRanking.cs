using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISubSceneRanking : UIScene
{
    [SerializeField] private Transform contents;

    #region Initialize

    protected override void Init()
    {
        base.Init();
        SetButtonEvents();
    }

    private void SetButtonEvents()
    {
        SetUI<Button>();
        SetButtonEvent("DimScreen", UIEventType.Click, CloseSubScene);
    }

    #endregion


    #region Button Events

    private void CloseSubScene(PointerEventData eventData)
    {
        Manager.UI.CloseSubScene();
        Manager.UI.Top.SetCloseButton(false);
    }

    #endregion
}
