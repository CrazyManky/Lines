using Project.Screpts.Screens;
using UnityEngine;

namespace _Project.Scripts.Screens
{
    public class MenuScreen : BaseScreen
    {
        public void OpenSetting()
        {
            AudioManager.PlayButtonClick();
            Dialog.ShowSettingsScreen();
        }

        public void ShowGameScreen()
        {
            AudioManager.PlayButtonClick();
            Dialog.ShowGameScreen();
        }

        public void ExitApp()
        {
            Application.Quit();
        }
    }
}