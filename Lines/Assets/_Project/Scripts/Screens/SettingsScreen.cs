using Project.Screpts.Screens;
using SO;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Screens
{
    public class SettingsScreen : BaseScreen
    {
        [SerializeField] private PolicyScreen _policyScreen;
        [SerializeField] private Slider _sliderMusick;
        [SerializeField] private Slider _sliderSound;
        [SerializeField] private LevelsConfig _levelsConfig;

        private void OnEnable()
        {
            _sliderMusick.onValueChanged.AddListener(SetValueSound);
            _sliderSound.onValueChanged.AddListener(SetValueMusick);
        }

        public override void Init()
        {
            base.Init();
            _sliderMusick.value = AudioManager.MusickValue;
            _sliderSound.value = AudioManager.SoundValue;
        }

        public void ShowPolicyScreen()
        {
            AudioManager.PlayButtonClick();
           Instantiate(_policyScreen, transform);
        }

        public void SetValueSound(float value) => AudioManager.SetMusick(value);
        public void SetValueMusick(float value) => AudioManager.SetSound(value);

        public void BackMenu()
        {
            AudioManager.PlayButtonClick();
            Dialog.ShowMenuScreen();
        }


        private void OnDisable()
        {
            _sliderMusick.onValueChanged.RemoveListener(SetValueMusick);
            _sliderSound.onValueChanged.RemoveListener(SetValueSound);
        }

        public void Reset()
        {
            AudioManager.PlayButtonClick();
            _levelsConfig.Reset();
        }
    }
}