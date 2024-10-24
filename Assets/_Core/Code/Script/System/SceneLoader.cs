using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JellyField
{

    public enum SceneGeneral
    {
        Scene_MainMenu,
        Scene_Option,
    }

    public enum SceneGameLevel
    {
        Scene_Game_Level_1,
        Scene_Game_Level_2,
        Scene_Game_Level_3,
    }

    public enum SceneTargetType
    {
        SceneGeneral,
        SceneGameLevel
    }


    public enum SceneLoading
    {
        Scene_Loading,
        None
    }

    public static class SceneLoader
    {
        private static SceneGeneral targetSceneGeneral;
        private static SceneGameLevel targetSceneGameLevel;
        private static SceneTargetType sceneTargetType;

        public static void LoadAdditive<T>(T targetScene) where T : Enum
        {
            SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Additive);
        }

        public static void LoadSingle<T>(T targetScene, SceneLoading loadingScene = SceneLoading.Scene_Loading)
            where T : Enum
        {
            if (loadingScene == SceneLoading.None)
            {
                SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
                return;
            }

            SetTargetScene(targetScene);

            SceneManager.LoadScene(loadingScene.ToString(), LoadSceneMode.Single);
        }

        public static void LoadSingleInstant<T>(T targetScene) where T : Enum
        {
            LoadSingle(targetScene, SceneLoading.None);
        }

        public static AsyncOperation LoadTargetSceneSingleAsync()
        {
            switch (sceneTargetType)
            {
                case SceneTargetType.SceneGeneral:
                    return SceneManager.LoadSceneAsync(targetSceneGeneral.ToString(), LoadSceneMode.Single);

                case SceneTargetType.SceneGameLevel:
                    return SceneManager.LoadSceneAsync(targetSceneGameLevel.ToString(), LoadSceneMode.Single);

                default:
                    throw new Exception("Error when loading target scene");
            }
        }

        private static void SetTargetScene<T>(T sceneEnum) where T : Enum
        {
            if (sceneEnum is SceneGeneral sceneGeneral)
            {
                targetSceneGeneral = sceneGeneral;
                sceneTargetType = SceneTargetType.SceneGeneral;
            }
            else if (sceneEnum is SceneGameLevel sceneGameLevel)
            {
                targetSceneGameLevel = sceneGameLevel;
                sceneTargetType = SceneTargetType.SceneGameLevel;
            }
            else
            {
                throw new ArgumentException($"Unsupported enum type: {typeof(T)}");
            }
        }
    }
}
