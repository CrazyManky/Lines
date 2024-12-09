using UnityEngine;

namespace Project.Screpts.SO
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "SoundConfig")]
    public class SounConfig : ScriptableObject
    {
        [SerializeField] private float _valueSound;
        [SerializeField] private float _valueMusic;

        public float ValueSound => _valueSound;
        public float ValueMusick => _valueMusic;

        public void SetSoundValue(float value)
        {
            _valueSound = value;
        }

        public void SetMuscikValue(float value)
        {
            _valueMusic = value;
        }
    }
}