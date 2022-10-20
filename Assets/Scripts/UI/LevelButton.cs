using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelButton : MonoBehaviour
{
    [SerializeField] TextMesh text;
    [SerializeField] private int level;
    public int Level { get => level; set => setLevel(value); }
    void setLevel(int value)
    {
        Debug.Log("level set at :" + value.ToString());
        text.text = value.ToString();
        level = value;
    }
}