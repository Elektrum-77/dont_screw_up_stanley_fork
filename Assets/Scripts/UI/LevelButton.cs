using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private int level;
    public int Level { get => level; set => setLevel(value); }

    void setLevel(int value)
    {
        text.text = value.ToString();
        level = value;
    }

    public void OpenLevel()
    {
        LevelSelector.OpenLevel(level);
    }

}