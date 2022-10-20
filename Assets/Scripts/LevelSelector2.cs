using ScreenNavigation.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector2 : MonoBehaviour
{
    [SerializeField] private float[] pauseTimers;
    [SerializeField] private BuildingSelector _buildingSelector;
    [SerializeField] private StackNavigator _navigator;
    [SerializeField] private GameObject exotronLogo;
    [SerializeField] private Sprite winSp, loseSp;
    [SerializeField] private Image reportPanel;
    [TextArea] [SerializeField] private string winText, loseText;
    [SerializeField] private TextMeshProUGUI speechText;
    private int _currentLevelIndex;
    private City _currentCity;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (_currentCity)
        {
            _currentCity.isGameWin -= OnGameWin;
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 0)
        {
            _currentCity = FindObjectOfType<City>();
            _currentCity.isGameWin += OnGameWin;
            Invoke(nameof(PauseGameBeforeDialog), .1f);
            Invoke(nameof(UnPauseAfterDialog), pauseTimers[_currentLevelIndex - 1]);
        }
    }

    private void PauseGameBeforeDialog()
    {
        _currentCity.isPause = true;
        exotronLogo.SetActive(true);
    }

    private void UnPauseAfterDialog()
    {
        _currentCity.isPause = false;
        exotronLogo.SetActive(false);
    }

    private void OnGameWin(bool win)
    {
        _currentCity.isGameWin -= OnGameWin;
        _navigator.Navigate("GameOver");
        _buildingSelector.enabled = false;
        _buildingSelector.enabled = true;
        reportPanel.sprite = win ? winSp : loseSp;
        speechText.text = win ? winText : loseText;
        AudioManager.Instance.Stop("dialog");
        AudioManager.Instance.Stop("theme");
        AudioManager.Instance.Play(win ? "win" : "lose");
    }

    public void Restart()
    {
        LoadLevel(_currentLevelIndex);
    }

    public void Next()
    {
        LoadLevel(_currentLevelIndex + 1);
    }

    public void LoadLevel(int i)
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(_currentLevelIndex);
        }

        _currentLevelIndex = i;
        SceneManager.LoadScene(i, LoadSceneMode.Additive);
        _navigator.Navigate("HUD");
        AudioManager.Instance.Stop("dialog");
        AudioManager.Instance.Stop("win");
        AudioManager.Instance.Stop("lose");
        AudioManager.Instance.Play("theme");
        AudioManager.Instance.Play("dialog", _currentLevelIndex - 1);
    }

    public void GoToMenu()
    {
        AudioManager.Instance.Stop("theme");
        AudioManager.Instance.Stop("dialog");
        AudioManager.Instance.Stop("alert");
        _navigator.Navigate("Menu");
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Skip();
        }
    }

    public void Skip()
    {
        AudioManager.Instance.Stop("dialog");
        UnPauseAfterDialog();
        CancelInvoke(nameof(UnPauseAfterDialog));
    }
}