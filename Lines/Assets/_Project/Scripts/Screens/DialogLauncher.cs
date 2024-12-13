using Project.Screpts.Screens;
using Services;
using UnityEngine;

namespace _Project.Scripts.Screens
{
    public class DialogLauncher : MonoBehaviour, IService
    {
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private AudioManager _audioManager;


        private BaseScreen _activeScreen;

        private void Awake()
        {
            ServiceLocator.Init();
            ServiceLocator.Instance.AddService(this);
            ServiceLocator.Instance.AddService(_audioManager);
        }

        private void Start() => ShowMenuScreen();

        public void ShowMenuScreen()
        {
            _audioManager.PlayMenuMusick();
            ShowScreen(_menuScreen);
        }

        public void ShowSettingsScreen() => ShowScreen(_settingsScreen);

        public void ShowGameScreen()
        {
            _audioManager.PlayGame();
            ShowScreen(_gameScreen);
        }

        private void ShowScreen(BaseScreen screen)
        {
            _activeScreen?.Сlose();
            var instanceScreen = Instantiate(screen, transform);
            instanceScreen.Init();
            _activeScreen = instanceScreen;
        }
    }
}