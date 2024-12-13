using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Screpts.Load
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void Start() => LoadNextScene();

        private async void LoadNextScene()
        {
            var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            var taskLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
            taskLoad.allowSceneActivation = false;
            while (taskLoad.progress < 0.9f)
            {
                _slider.value = taskLoad.progress;
                await UniTask.Yield();
            }

            _slider.value = 1f;
            taskLoad.allowSceneActivation = true;
        }
    }
}