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

        private void SpawnGameBoardUnit(Vector3 position)
        {
            GameObject spawnedGameBoardUnitToBeGameObject = new GameObject("SpawnedGameBoardUnit");
            GameBoardUnit spawnedGameBoardUnitToBe = spawnedGameBoardUnitToBeGameObject.AddComponent<GameBoardUnit>();
            spawnedGameBoardUnitToBeGameObject.transform.position = position;
            spawnedGameBoardUnitToBeGameObject.AddComponent<DraggableGameBoardUnit>();
            BoxCollider boxCollider = spawnedGameBoardUnitToBeGameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.center = new Vector3(0.5f, 0.5f, -0.5f);

            int valueCount = GameBoardUnit.MAXIMUM_GAME_BOARD_UNIT_TOTAL_CUBE_VALUE;
            List<GameBoardUnitCube.CubeType> exceptionCubeTypeList = new List<GameBoardUnitCube.CubeType>();
            List<GameBoardUnitCube.CubeColor> exceptionCubeColorList = new List<GameBoardUnitCube.CubeColor>();

            while (valueCount > 0)
            {
                GameBoardUnitCube.CubeType randomizedCubeType = GetRandomCubeType(exceptionCubeTypeList);
                if (cubeTypeValues[randomizedCubeType] > valueCount)
                {
                    continue;
                }

                exceptionCubeTypeList.Add(randomizedCubeType);

                valueCount -= cubeTypeValues[randomizedCubeType];
                switch (randomizedCubeType)
                {
                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightDown);
                        break;

                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightUp);
                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterLeftUp);
                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.QuarterRightUp);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                        // Remove overlapping cube types
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        exceptionCubeTypeList.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        break;

                    case GameBoardUnitCube.CubeType.Whole:
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
                        Debug.LogWarning("Unexpected CubeType: " + randomizedCubeType); // Log a warning for unexpected types
                        break;
                }

                GameBoardUnitCube spawnedGameBoardUnitCube = GameBoardUnitCube.SpawnCubeByType(randomizedCubeType, spawnedGameBoardUnitToBe);
                spawnedGameBoardUnitCube.CubeTypeValue = randomizedCubeType;
                spawnedGameBoardUnitCube.CubeColorValue = GetRandomCubeColor(exceptionCubeColorList);
                exceptionCubeColorList.Add(spawnedGameBoardUnitCube.CubeColorValue);

                spawnedGameBoardUnitToBe.GetListGameBoardUnitCube().Add(spawnedGameBoardUnitCube);
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
            //return CubeType.Whole;
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
            //return CubeColor.Green;
        }

    }
}
