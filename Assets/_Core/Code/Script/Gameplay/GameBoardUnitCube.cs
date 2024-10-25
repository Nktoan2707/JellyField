using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    public class GameBoardUnitCube : MonoBehaviour
    {
        public static Dictionary<GameBoardUnitCube.CubeType, int> cubeTypeValues = new Dictionary<GameBoardUnitCube.CubeType, int>
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


        public static int GetCubeTypeValue(GameBoardUnitCube.CubeType cubeType)
        {
            return cubeTypeValues[cubeType];
        }

        public enum CubeType
        {
            HalfHorizontalDown,
            HalfHorizontalUp,
            HalfVerticalLeft,
            HalfVerticalRight,
            QuarterLeftDown,
            QuarterLeftUp,
            QuarterRightDown,
            QuarterRightUp,
            Whole,
        }

        public enum CubeColor
        {
            Red,          // Color.red
            Green,        // Color.green
            Blue,         // Color.blue
            Yellow,       // Color.yellow
            Cyan,         // Color.cyan
            Magenta,      // Color.magenta
            White,        // Color.white
            Black,        // Color.black
            Gray,         // Color.gray
        }

        public CubeType CubeTypeValue { get; set; }


        private CubeColor cubeColor;
        public CubeColor CubeColorValue
        {
            get
            { return cubeColor; }

            set
            {
                cubeColor = value;

                switch (cubeColor)
                {
                    case CubeColor.Red:
                        cubeMaterial.color = Color.red;
                        break;
                    case CubeColor.Blue:
                        cubeMaterial.color = Color.blue;
                        break;
                    case CubeColor.Green:
                        cubeMaterial.color = Color.green;
                        break;
                    case CubeColor.Yellow:
                        cubeMaterial.color = Color.yellow;
                        break;
                    case CubeColor.Cyan:
                        cubeMaterial.color = Color.cyan;
                        break;
                    case CubeColor.Magenta:
                        cubeMaterial.color = Color.magenta;
                        break;
                    case CubeColor.White:
                        cubeMaterial.color = Color.white;
                        break;
                    case CubeColor.Black:
                        cubeMaterial.color = Color.black;
                        break;
                    case CubeColor.Gray:
                        cubeMaterial.color = Color.gray;
                        break;
                    default:
                        cubeMaterial.color = Color.clear; // Default color if not matched
                        break;
                }
            }
        }


        [SerializeField] GameObject visualGameObject;
        private Material cubeMaterial;

        private void Awake()
        {
            Renderer renderer = visualGameObject.GetComponent<Renderer>();
            cubeMaterial = renderer.material;
        }

        public static GameBoardUnitCube SpawnCubeByType(CubeType cubeType, GameBoardUnit gameBoardUnit)
        {
            GameObject gameBoardUnitCubeGameObject = null;
            switch (cubeType)
            {
                case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfHorizontalDown.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfHorizontalUp.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfVerticalLeft.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.HalfVerticalRight:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeHalfVerticalRight.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.QuarterLeftDown:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterLeftDown.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.QuarterLeftUp:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterLeftUp.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.QuarterRightDown:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterRightDown.gameObject, gameBoardUnit.transform);


                    break;

                case GameBoardUnitCube.CubeType.QuarterRightUp:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeQuarterRightUp.gameObject, gameBoardUnit.transform);

                    break;

                case GameBoardUnitCube.CubeType.Whole:
                    gameBoardUnitCubeGameObject = Instantiate(LevelManager.Instance.gameLevelSO.gameBoardUnitCubeListSO.gameBoardUnitCubeWhole.gameObject, gameBoardUnit.transform);
                    break;

                default:
                    Debug.LogWarning("Unexpected CubeType: " + cubeType); // Log a warning for unexpected types
                    break;
            }
            var gameBoardUnitCube = gameBoardUnitCubeGameObject.GetComponent<GameBoardUnitCube>();
            return gameBoardUnitCube;
        }
    }
}
