using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static JellyField.GameBoardUnitCube;

namespace JellyField
{
    public class GameBoardUnitSpawnManager : MonoBehaviour
    {
        public static GameBoardUnitSpawnManager Instance { get; private set; }

        private List<Vector2> listSpawnGameBoardUnitPosition = new List<Vector2>();
        public List<Vector2> ListSpawnGameBoardUnitPosition
        {
            get { return listSpawnGameBoardUnitPosition; }
            set
            {
                listSpawnGameBoardUnitPosition = value;

                spawnGameBoardUnitPositionValues.Clear();
                foreach (var i in ListSpawnGameBoardUnitPosition)
                {
                    spawnGameBoardUnitPositionValues.Add(i, false);
                }
            }
        }


        private Dictionary<GameBoardUnitCube.CubeType, int> cubeTypeValues = new Dictionary<GameBoardUnitCube.CubeType, int>
        {
            { GameBoardUnitCube.CubeType.HalfHorizontalDown, 2 },
            { GameBoardUnitCube.CubeType.HalfHorizontalUp, 2 },
            { GameBoardUnitCube.CubeType.HalfVerticalLeft, 2 },
            { GameBoardUnitCube.CubeType.HalfVerticalRight, 2 },
            { GameBoardUnitCube.CubeType.QuarterLeftDown, 1 },
            { GameBoardUnitCube.CubeType.QuarterLeftUp, 1 },
            { GameBoardUnitCube.CubeType.QuarterRightDown, 1 },
            { GameBoardUnitCube.CubeType.QuarterRightUp, 1 },
            { GameBoardUnitCube.CubeType.Whole, 4 }
        };

        

        private Dictionary<Vector2, bool> spawnGameBoardUnitPositionValues = new Dictionary<Vector2, bool>();
        public Dictionary<Vector2, bool> GetSpawnGameBoardUnitPositionValues()
        {
            return spawnGameBoardUnitPositionValues;
        }

        private void Awake()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;

        }

        private void Update()
        {
            foreach (var item in spawnGameBoardUnitPositionValues)
            {
                if (item.Value == false)
                {
                    SpawnGameBoardUnit(item.Key);
                    spawnGameBoardUnitPositionValues[item.Key] = true;
                    break;
                }
            }
        }

        public void SpawnGameBoardUnit(Vector3 position)
        {
            GameBoardUnitCubeListSO gameBoardUnitCubeListSO = LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO;
            GameObject gameBoardUnitGameObject = new GameObject("GameBoardUnit");
            gameBoardUnitGameObject.AddComponent<GameBoardUnit>();
            gameBoardUnitGameObject.transform.position = position;
            gameBoardUnitGameObject.AddComponent<DraggableGameBoardUnit>();
            BoxCollider boxCollider = gameBoardUnitGameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.center = new Vector3(0.5f, 0.5f, -0.5f);

            int valueCount = 4;
            List<GameBoardUnitCube.CubeType> exceptionCubeTypeList = new List<GameBoardUnitCube.CubeType>();
            List<GameBoardUnitCube.CubeColor> exceptionCubeColorList = new List<GameBoardUnitCube.CubeColor>();

            while (valueCount > 0)
            {
                GameBoardUnitCube.CubeType cubeType = GetRandomCubeType(exceptionCubeTypeList);
                if (cubeTypeValues[cubeType] > valueCount)
                {
                    continue;
                }

                exceptionCubeTypeList.Add(cubeType);

                valueCount -= cubeTypeValues[cubeType];
                GameObject gameBoardUnitCubeGameObject = null;
                switch (cubeType)
                {
                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeHalfHorizontalDown.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightDown);
                        break;

                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeHalfHorizontalUp.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightUp);
                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeHalfVerticalLeft.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftUp);
                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeHalfVerticalRight.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightUp);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterLeftDown.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterLeftUp.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterRightDown.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterRightUp.gameObject, gameBoardUnitGameObject.transform);

                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        break;

                    case GameBoardUnitCube.CubeType.Whole:
                        gameBoardUnitCubeGameObject = Instantiate(gameBoardUnitCubeListSO.gameBoardUnitCubeWhole.gameObject, gameBoardUnitGameObject.transform);

                        // Remove all other cube types since 'Whole' overlaps with all
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightUp);
                        break;

                    default:
                        Debug.LogWarning("Unexpected CubeType: " + cubeType); // Log a warning for unexpected types
                        break;
                }


                GameBoardUnitCube gameBoardUnitCube = gameBoardUnitCubeGameObject.GetComponent<GameBoardUnitCube>();
                gameBoardUnitCube.GameBoardCubeColor = GetRandomCubeColor(exceptionCubeColorList);
                exceptionCubeColorList.Add(gameBoardUnitCube.GameBoardCubeColor);

            }
        }

        private GameBoardUnitCube.CubeType GetRandomCubeType(List<GameBoardUnitCube.CubeType> exceptionCubeTypeList)
        {
            // Get all enum values
            GameBoardUnitCube.CubeType[] allValues = (GameBoardUnitCube.CubeType[])System.Enum.GetValues(typeof(GameBoardUnitCube.CubeType));

            // Create a list to store valid CubeType values
            List<GameBoardUnitCube.CubeType> validCubeTypes = new List<GameBoardUnitCube.CubeType>();

            // Populate the validCubeTypes list, skipping any values in the exceptionCubeTypeList
            foreach (GameBoardUnitCube.CubeType cubeType in allValues)
            {
                if (!exceptionCubeTypeList.Contains(cubeType)) // Check if the current CubeType is in the exception list
                {
                    validCubeTypes.Add(cubeType);
                }
            }

            // Check if there are valid CubeTypes left
            if (validCubeTypes.Count == 0)
            {
                throw new System.InvalidOperationException("No valid CubeType available to select.");
            }

            // Get a random index from the validCubeTypes list
            int randomIndex = Random.Range(0, validCubeTypes.Count);

            // Return a random valid CubeType
            return validCubeTypes[randomIndex];
        }

        private CubeColor GetRandomCubeColor(List<CubeColor> exceptionColorList)
        {
            // Get all enum values
            CubeColor[] allColors = (CubeColor[])System.Enum.GetValues(typeof(CubeColor));

            // Create a list to store valid CubeColor values
            List<CubeColor> validColors = new List<CubeColor>();

            // Populate the validColors list, skipping any values in the exceptionColorList
            foreach (CubeColor color in allColors)
            {
                if (!exceptionColorList.Contains(color)) // Check if the current CubeColor is in the exception list
                {
                    validColors.Add(color);
                }
            }

            // Check if there are valid CubeColors left
            if (validColors.Count == 0)
            {
                throw new System.InvalidOperationException("No valid CubeColor available to select.");
            }

            // Get a random index from the validColors list
            int randomIndex = Random.Range(0, validColors.Count);

            // Return a random valid CubeColor
            return validColors[randomIndex];
        }

    }
}
