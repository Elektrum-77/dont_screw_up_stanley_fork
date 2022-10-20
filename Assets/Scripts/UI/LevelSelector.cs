using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    [SerializeField] Button nextPageButton;
    [SerializeField] Button previousPageButton;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] GameObject buttonPrefab;
    int pageStartingLevelCount = 0;
    [SerializeField] int levelPerPage;
    [SerializeField] int minLevel;
    [SerializeField] int maxLevel;

    LevelButton[] getButtons()
    {
        return grid.GetComponentsInChildren<LevelButton>(true);
    }

    void generateButtons()
    {
        for (int i = 0; i < levelPerPage; i++)
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(grid.transform, false);
        }

    }

    void SetButtonsActive()
    {
        var buttons = getButtons();
        int usedButton = maxLevel - pageStartingLevelCount;
        for (int i = 0; i <= buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive((i + pageStartingLevelCount) < usedButton);
        }
    }

    void setButtonsLevel()
    {
        int level = pageStartingLevelCount;
        foreach (var button in getButtons())
        {
            button.Level = level++;
            Debug.Log(level);
        }
    }

    void updatePageButtons()
    {
        nextPageButton.interactable = !(pageStartingLevelCount + levelPerPage > maxLevel);
        previousPageButton.interactable = !(pageStartingLevelCount - levelPerPage < minLevel);
        setButtonsLevel();
        SetButtonsActive();
    }

    void nextPage()
    {
        pageStartingLevelCount += levelPerPage;
        updatePageButtons();
    }

    void previousPage()
    {
        pageStartingLevelCount -= levelPerPage;
        updatePageButtons();
    }

    void Start()
    {
        generateButtons();
        updatePageButtons();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("HUD");
        SceneManager.LoadScene("Level_1");
    }
}
