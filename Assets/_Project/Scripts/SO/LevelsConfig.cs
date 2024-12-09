using System.Collections.Generic;
using _Project.Scripts.Drawing_Lines;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Levels", menuName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<LineDrawer> _gameLevelsPrefab;
        [SerializeField] private int _activeLevel;

        public int LevelIndex => _activeLevel;

        public LineDrawer GetLevel()
        {
            if (LevelIndex >= _gameLevelsPrefab.Count)
            {
                return _gameLevelsPrefab[Random.Range(0, _gameLevelsPrefab.Count)];
            }

            return _gameLevelsPrefab[_activeLevel];
        }

        public void LevelComplited()
        {
            _activeLevel++;
        }

        public void Reset()
        {
            _activeLevel = 0;
        }
    }
}