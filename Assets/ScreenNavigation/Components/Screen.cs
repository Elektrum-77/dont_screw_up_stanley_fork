using UnityEngine;
using UnityEngine.EventSystems;

namespace ScreenNavigation.Components
{
    public class Screen : MonoBehaviour
    {
        public bool isOverlay;
        [SerializeField] private GameObject firstSelected;

        public void Show()
        {
            gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}