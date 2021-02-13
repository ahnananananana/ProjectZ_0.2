using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HDV
{
    [Serializable]
    public enum SceneName
    {
        Mainmenu,
        Gameplay,
        Loading,
    }

    [CreateAssetMenu]
    public class SceneLoadManager : ScriptableObject
    {
        private SceneName nextSceneName;
        private float loadingProgress;
        private Scene previousScene;

        [SerializeField] private FloatEvent progressChangeEvent;

        //TODO: Enum으로 바꿔야

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void Load(int sceneBuildIndex)
        {
            loadingProgress = 0f;
            nextSceneName = (SceneName)sceneBuildIndex;
            SceneManager.LoadSceneAsync((int)SceneName.Loading, LoadSceneMode.Additive);
        }

       /* private void OnLoadLoadingScene(Scene scene, LoadSceneMode mode)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            CalculateProgress(ao, 0f, .5f);
        }

        private void OnUnLoadPreviousScene(Scene scene, LoadSceneMode mode)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync((int)nextSceneName, LoadSceneMode.Additive);
            CalculateProgress(ao, .5f, 1f);
        }

        private void OnNextSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.SetActiveScene(scene);
        }*/

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch((SceneName)scene.buildIndex)
            {
                /*case SceneName.Init:
                    {
                        var currentScene = SceneManager.GetActiveScene();
                        if (currentScene.buildIndex != scene.buildIndex)
                            break;
                        break;
                    }*/
                case SceneName.Loading:
                    {
                        previousScene = SceneManager.GetActiveScene();
                        AsyncOperation ao = SceneManager.UnloadSceneAsync(previousScene);
                        CalculateProgress(ao, 0f, .5f);
                        break;
                    }
                default:
                    {
                        var previousScene = SceneManager.GetActiveScene();
                        if (previousScene.buildIndex == scene.buildIndex)
                            break;
                        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                        SceneManager.SetActiveScene(scene);
                        break;
                    }
            }
        }

        private void OnSceneUnloaded(Scene scene)
        {
            if(scene.buildIndex != -1 && scene.buildIndex == previousScene.buildIndex)
            {
                AsyncOperation ao = SceneManager.LoadSceneAsync((int)nextSceneName, LoadSceneMode.Additive);
                CalculateProgress(ao, .5f, 1f);
            }
        }

        private async void CalculateProgress(AsyncOperation ao, float start, float end)
        {
            while(!ao.isDone)
            {
                await Task.Yield();
                loadingProgress = MathUtil.Remap(ao.progress, start, end);
                progressChangeEvent?.Invoke(loadingProgress);
            }
        }

    }
}
