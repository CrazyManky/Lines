using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SettingConfig", menuName = "SO/SettingConfig")]
    public class SettingConfig : ScriptableObject
    {
        // [SerializeField] private bool _soundActive = true;
        //
        // public bool SoundActive => _soundActive;
        //
        // public void SwitchValue()
        // {
        //     var value = !_soundActive;
        //     _soundActive = value;
        // }
    }
}