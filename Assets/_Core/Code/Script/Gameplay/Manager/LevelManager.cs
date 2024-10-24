using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JellyField.GameBoardUnitCube;

namespace JellyField
{
    [System.Serializable]
    public struct WinCondition
    {
        public GameBoardUnitCube.CubeColor cubeColor;
        [Range(0f, 100f)]
        public int numberOfCube;
    }

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public List<WinCondition> ListWinCondition { get; private set; }

        public delegate void LevelClearedEventHandler(bool isFirstTime);

        public event LevelClearedEventHandler OnLevelCleared;
        public event EventHandler OnLevelFailed;
        public event EventHandler OnIsGamePausedChanged;

        [SerializeField]
        public GameLevelSO gameLevelSO;
        //[SerializeField]
        //public GameLevelSO gameLevelSO;

        private bool isLevelCleared;
        private bool isGamePaused;

        public bool IsGamePaused
        {
            get => isGamePaused;
            set
            {
                //if (spawnedGameObjectList == null || Player.Instance == null)
                //{
                //    Debug.LogError("Something went wrong when pause/unpause game!");
                //    return;
                //}


                //Player.Instance.IsEnabled = !value;
                isGamePaused = value;
                OnIsGamePausedChanged.Invoke(this, EventArgs.Empty);
            }
        }

        private List<GameObject> listSpawnedGameObject;

        //private int _collectedGems;

        //public int CollectedGems
        //{
        //    get => _collectedGems;
        //    set => _collectedGems = value;
        //}

        //private int _defeatedMonsters;

        //public int DefeatedMonsters
        //{
        //    get => _defeatedMonsters;
        //    set => _defeatedMonsters = value;
        //}


        public void HandleWinConditions()
        {
            if (isLevelCleared)
            {
                return;
            }

            //foreach (var winCondition in gameLevelSO.winConditionList)
            //{
            //    switch (winCondition)
            //    {
            //        case WinCondition.AllGemsCollected:
            //            if (CollectedGems != gameLevelSO.spawnDataGemList.Count)
            //            {
            //                InvokeEventOnLevelFailed();
            //                return;
            //            }

            //            break;

            //    }
            //}

            // All win conditions are satisfied

            InvokeEventOnLevelCleared();
        }

        public void InvokeEventOnLevelCleared()
        {
            //if (!isLevelCleared)
            //{
            //    OnLevelCleared?.Invoke(_isFirstTimePlay);
            //    Player.Instance?.ActionSOList.Clear();
            //    isLevelCleared = true;
            //}
        }

        public void InvokeEventOnLevelFailed()
        {
            //if (Player.Instance != null && Player.Instance.IsDoneExecuteAction())
            //{
            //    OnLevelFailed?.Invoke(this, EventArgs.Empty);
            //}
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            isLevelCleared = false;
            isGamePaused = false;

            ListWinCondition = new List<WinCondition>();
            listSpawnedGameObject = new List<GameObject>();



            //SceneLoader.LoadAdditive(SceneBeginner.Scene_Beginner_Level_Editor);
        }

        private void Start()
        {
            LoadLevel();
        }

        public void ReplayCurrentLevel()
        {
            SceneLoader.LoadSingle(gameLevelSO.sceneGameLevel);
        }

        public void LoadNextLevel()
        {

            SceneLoader.LoadSingle(gameLevelSO.nextGameLevelSO.sceneGameLevel);

        }

        private void LoadLevel()
        {
            GameBoardUnitSpawnManager.Instance.ListSpawnGameBoardUnitPosition = gameLevelSO.listSpawnGameBoardUnitPosition;

            GameBoard.Instance.InitializeGameBoardUnit2DMatrix(boardWidth: gameLevelSO.boardWidth, boardHeight: gameLevelSO.boardHeight);
            GameBoard.Instance.ListUnavailableBoardPosition = gameLevelSO.listUnavailableBoardPosition;

            var listSpawnDataGameBoardUnit = gameLevelSO.listSpawnDataGameBoardUnit;
            foreach (var spawnDataGameBoardUnit in listSpawnDataGameBoardUnit)
            {
                var gameBoardUnitGameObject = Instantiate(spawnDataGameBoardUnit.gameBoardUnitPrefab.gameObject, Vector3.zero, Quaternion.identity);
                gameBoardUnitGameObject.transform.position = spawnDataGameBoardUnit.position;
                var gameBoardUnit = gameBoardUnitGameObject.GetComponent<GameBoardUnit>();




                var listSpawnDataGameBoardUnitCube = spawnDataGameBoardUnit.listSpawnDataGameBoardUnitCube;
                foreach (var spawnDataGameBoardUnitCube in listSpawnDataGameBoardUnitCube)
                {
                    GameObject gameBoardUnitCubeGameObject = null;
                    switch (spawnDataGameBoardUnitCube.gameBoardCubeType)
                    {
                        case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfHorizontalDown.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfHorizontalUp.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfVerticalLeft.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.HalfVerticalRight:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfVerticalRight.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.QuarterLeftDown:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterLeftDown.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.QuarterLeftUp:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterLeftUp.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.QuarterRightDown:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterRightDown.gameObject, gameBoardUnitGameObject.transform);


                            break;

                        case GameBoardUnitCube.CubeType.QuarterRightUp:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterRightUp.gameObject, gameBoardUnitGameObject.transform);

                            break;

                        case GameBoardUnitCube.CubeType.Whole:
                            gameBoardUnitCubeGameObject = Instantiate(gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeWhole.gameObject, gameBoardUnitGameObject.transform);
                            break;

                        default:
                            Debug.LogWarning("Unexpected CubeType: " + spawnDataGameBoardUnitCube.gameBoardCubeType); // Log a warning for unexpected types
                            break;
                    }
                    var gameBoardUnitCube = gameBoardUnitCubeGameObject.GetComponent<GameBoardUnitCube>();
                    gameBoardUnitCube.GameBoardCubeType = spawnDataGameBoardUnitCube.gameBoardCubeType;
                    gameBoardUnitCube.GameBoardCubeColor = spawnDataGameBoardUnitCube.color;

                    listSpawnedGameObject.Add(gameBoardUnitCubeGameObject);
                }

                listSpawnedGameObject.Add(gameBoardUnitGameObject);
                GameBoard.Instance.GetGameBoardUnit2DMatrix()[(int)spawnDataGameBoardUnit.position.x][(int)spawnDataGameBoardUnit.position.y] = gameBoardUnit;
            }

        }

        private void CleanUp()
        {
            //Player.Instance.OnExecuteActionListCompleted -= Player_OnExecuteActionListCompleted;
            //foreach (var spawnedObject in spawnedGameObjectList)
            //{
            //    Destroy(spawnedObject);
            //}

            //spawnedGameObjectList.Clear();
            //Player.Instance = null;

            //CollectedGems = 0;
            //DefeatedMonsters = 0;

            //puzzleArrayFilteringList.Clear();
            //puzzleArrayMatchingList.Clear();
            //puzzleArraySortingList.Clear();

            //puzzleFileOpeningList.Clear();
            //puzzleFileReadList.Clear();
            //puzzleFileWriteList.Clear();
        }

        //public void ResetLevel()
        //{
        //    CleanUp();
        //    LoadLevel();
        //}

        //public void Pause()
        //{
        //    Player.Instance.IsEnabled = false;
        //}



        //public void StartGame(List<ActionSO> actionSoList)
        //{
        //    CleanUp();
        //    LoadLevel();

        //    Player.Instance.ActionSOList.Clear();
        //    Player.Instance.ActionSOList = actionSoList;
        //}
    }
}

