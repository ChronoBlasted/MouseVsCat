using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] EndGamePanel _endGamePanel;

    [SerializeField] SettingPopup _settingPopup;
    [SerializeField] ShopPopup _shopPopup;
    [SerializeField] ChooseNewPawnPopup _chooseNewPawnPopup;
    [SerializeField] SelectTwoPawnPopup _selectTwoPawnPopup;

    [SerializeField] Image _blackShade;
    [SerializeField] Button _blackShadeBtn;

    public Canvas MainCanvas;

    Panel _currentPanel;
    Popup _currentPopup;

    public EndGamePanel EndGamePanel { get => _endGamePanel; }
    public Popup CurrentPopup { get => _currentPopup; set => _currentPopup = value; }
    public ShopPopup ShopPanel { get => _shopPopup; }
    public ChooseNewPawnPopup ChooseNewPawnPopup { get => _chooseNewPawnPopup; }
    public SelectTwoPawnPopup SelectTwoPawnPopup { get => _selectTwoPawnPopup; }

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;

        InitPanel();
    }

    public void InitPanel()
    {
        _endGamePanel.Init();

        _settingPopup.Init();
        _shopPopup.Init();
    }

    public void OpenPanel(Panel newPanel)
    {
        if (newPanel == _currentPanel) return;
        if (_currentPanel != null) ClosePanel(_currentPanel);

        _currentPanel = newPanel;

        _currentPanel.gameObject.SetActive(true);
        _currentPanel.OpenPanel();
    }

    void ClosePanel(Panel newPanel)
    {
        newPanel.ClosePanel();
    }

    public void AddPopup(Popup newPopup)
    {
        if (newPopup == _currentPopup) return;

        _currentPopup = newPopup;

        _currentPopup.gameObject.SetActive(true);
        _currentPopup.OpenPopup();
    }


    #region GameState

    void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.GAME:
                HandleGame();
                break;
            case GameState.END:
                HandleEnd();
                break;
            default:
                break;
        }

    }

    void HandleGame()
    {
        _currentPanel = null;

        ClosePanel(_endGamePanel);
    }
    void HandleEnd()
    {
        OpenPanel(_endGamePanel);
    }

    public void HandleOpenSettings()
    {
        AddPopup(_settingPopup);
    }

    public void HandleOpenShop()
    {
        AddPopup(_shopPopup);
    }
    #endregion
}
