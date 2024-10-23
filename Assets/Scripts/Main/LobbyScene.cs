using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button guestLogin;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private DownloadPopup downloadPanel;

    #region Initialize

    private void Awake()
    {
        guestLogin.onClick.AddListener(OnGuestLogin);
    }


    #endregion

    #region Login Button Events

    private void OnGuestLogin()
    {
        NextDownloadPanel();
    }

    private void NextDownloadPanel()
    {
        loginPanel.SetActive(false);
        downloadPanel.gameObject.SetActive(true);

        StartCoroutine(downloadPanel.DownloadRoutine());
    }

    #endregion
}