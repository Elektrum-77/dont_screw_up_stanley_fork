using ScreenNavigation.Class;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenNavigation.Components
{
    public class TabNavigator : MonoBehaviour
    {
        [SerializeField] private TabScreen[] tabNavigator;
        [SerializeField] private ColorBlock colors;
        private int _tabIndex;

        private int Index
        {
            get => _tabIndex;
            set
            {
                _tabIndex = value;
                RefreshActiveTab();
            }
        }

        private void OnEnable()
        {
            for (var index = 0; index < tabNavigator.Length; index++)
            {
                var i = index;
                tabNavigator[index].button.onClick.AddListener(() => Index = i);
                tabNavigator[index].button.colors = colors;
            }
        }

        private void OnDisable()
        {
            foreach (var tabScreen in tabNavigator)
            {
                tabScreen.button.onClick.RemoveAllListeners();
            }
        }

        private void NextTab() => Index = (Index + 1) % tabNavigator.Length;

        private void PreviousTab() => Index = (Index + tabNavigator.Length - 1) % tabNavigator.Length;

        private void RefreshActiveTab()
        {
            foreach (var tab in tabNavigator)
            {
                tab.screen.Hide();
                tab.button.image.color = colors.normalColor;
            }

            tabNavigator[Index].screen.Show();
            tabNavigator[Index].button.image.color = colors.selectedColor;
        }
    }
}