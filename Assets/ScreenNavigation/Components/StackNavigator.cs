using System.Collections.Generic;
using ScreenNavigation.Class;
using UnityEngine;

namespace ScreenNavigation.Components
{
    public class StackNavigator : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Screen[] debugStack = { };
#endif
        [SerializeField] private StackScreen[] stackNavigator;
        private Dictionary<string, Screen> _nameScreens;
        private Stack<Screen> _stackNavigator;

        private void Awake()
        {
            _stackNavigator = new Stack<Screen>();
            RegisterAllScreen();
        }

        private void Start()
        {
            Navigate(stackNavigator[0].screen);
        }

        private void RegisterAllScreen()
        {
            _nameScreens = new Dictionary<string, Screen>();
            foreach (var stackScreen in stackNavigator)
            {
                _nameScreens.Add(stackScreen.name, stackScreen.screen);
                stackScreen.screen.Hide();
            }
        }

        private void Push(Screen screen)
        {
            if (!screen.isOverlay && _stackNavigator.Count > 0)
                _stackNavigator.Peek().Hide();
            _stackNavigator.Push(screen);

            _stackNavigator.Peek().Show();
        }

        public void GoBack(int number)
        {
            for (var i = 0; i < number; i++)
            {
                if (_stackNavigator.Count <= 1) return;
                _stackNavigator.Peek().Hide();

                _stackNavigator.Pop();

                _stackNavigator.Peek().Show();
            }
        }

        public void Navigate(string screenName)
        {
            var screenExists = _nameScreens.TryGetValue(screenName, out var screen);
            if (!screenExists)
            {
                Debug.LogError($"{screenName} screen doesnt exists in the stack navigator !");
                return;
            }

            Navigate(screen);
        }

        public void Navigate(Screen screen)
        {
            if (_stackNavigator.Contains(screen))
            {
                while (_stackNavigator.Peek() != screen)
                {
                    GoBack(1);
                }
            }
            else
            {
                Push(screen);
            }
#if UNITY_EDITOR
            debugStack = _stackNavigator.ToArray();
#endif
        }
    }
}