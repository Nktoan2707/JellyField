using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace JellyField
{
    public class GameBoard : MonoBehaviour
    {
        public static GameBoard Instance { get; private set; }

        [SerializeField] private GridLayout gridLayout;
        private Grid grid;

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase tileBase;

        private int boardWidth;
        private int boardHeight;
        private List<List<GameBoardUnit>> gameBoardUnit2DMatrix = new List<List<GameBoardUnit>>();

        public void InitializeGameBoardUnit2DMatrix(int boardHeight, int boardWidth)
        {
            this.boardHeight = boardHeight;
            this.boardWidth = boardWidth;

            // Initialize the outer list with capacity for boardWidth
            gameBoardUnit2DMatrix = new List<List<GameBoardUnit>>(boardWidth);

            // Loop to create inner lists
            for (int i = 0; i < boardWidth; i++)
            {
                // Create a new inner list and add it to the outer list
                gameBoardUnit2DMatrix.Add(new List<GameBoardUnit>(boardHeight));
                for (int j = 0; j < boardHeight; j++)
                {
                    gameBoardUnit2DMatrix[i].Add(null);
                }
            }
        }

        public List<List<GameBoardUnit>> GetGameBoardUnit2DMatrix()
        {
            return gameBoardUnit2DMatrix;
        }



        public List<Vector2> ListUnavailableBoardPosition { get; set; }
        public List<GameBoardUnit> listGameBoardUnit;

        #region Unity methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;

            grid = gridLayout.gameObject.GetComponent<Grid>();
        }

        private void Update()
        {

        }

        #endregion


        public struct GameBoardUnitMatchData
        {
            public GameBoardUnit centerUnit;
            public GameBoardUnit theOtherUnit;
            public Vector2 directionFromCenterToTheOther;
            public GameBoardUnitCube.CubeType matchedCubeTypeCenterUnit;
            public GameBoardUnitCube.CubeType matchedCubeTypeTheOtherUnit;
        }

        public List<GameBoardUnitMatchData> GetUnitMatchDataAtPosition(Vector2 boardPosition)
        {
            List<GameBoardUnitMatchData> listGameBoardUnitMatchData = new List<GameBoardUnitMatchData>();


            int xAxis = (int)boardPosition.x;
            int yAxis = (int)boardPosition.y;
            if (xAxis < 0 || yAxis < 0 || xAxis > boardWidth - 1 || yAxis > boardHeight - 1)
            {
                //Debug.Log("Invalid boardPosition: " + boardPosition);
                return listGameBoardUnitMatchData;
            }

            GameBoardUnit centerUnit = gameBoardUnit2DMatrix[xAxis][yAxis];

            if (centerUnit == null)
            {
                //Debug.Log("there is no unit on this tile: " + xAxis + "-" + yAxis);
                return listGameBoardUnitMatchData;
            }
            //Debug.Log("thasd: " + xAxis + "-" + yAxis);

            GameBoardUnit downUnitOfCenterUnit = yAxis - 1 >= 0 ? gameBoardUnit2DMatrix[xAxis][yAxis - 1] : null;
            GameBoardUnit upUnitOfCenterUnit = yAxis + 1 < boardHeight ? gameBoardUnit2DMatrix[xAxis][yAxis + 1] : null;
            GameBoardUnit leftUnitOfCenterUnit = xAxis - 1 >= 0 ? gameBoardUnit2DMatrix[xAxis - 1][yAxis] : null;
            GameBoardUnit rightUnitOfCenterUnit = xAxis + 1 < boardWidth ? gameBoardUnit2DMatrix[xAxis + 1][yAxis] : null;

            //Debug.Log(downUnitOfCenterUnit);
            //Debug.Log(upUnitOfCenterUnit);
            //Debug.Log(leftUnitOfCenterUnit);
            //Debug.Log(rightUnitOfCenterUnit);

            //iterate through each cube in center unit
            List<GameBoardUnitCube> listCenterGameBoardUnitCube = centerUnit.GetListGameBoardUnitCube();
            //Debug.Log("iteration: " + listCenterGameBoardUnitCube.Count);

            for (int i = 0; i < listCenterGameBoardUnitCube.Count; i++)
            {
                GameBoardUnitCube centerUnitCube = listCenterGameBoardUnitCube[i];
                switch (centerUnitCube.CubeTypeValue)
                {
                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:

                        break;

                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:

                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:

                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalRight:

                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftDown:

                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftUp:

                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightDown:


                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightUp:

                        break;

                    case GameBoardUnitCube.CubeType.Whole:
                        // check match for Down Unit
                        if (downUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube downUnitCube in downUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (downUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (downUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + downUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = downUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.down,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = downUnitCube.CubeTypeValue
                                });

                            }
                        }


                        // check match for Up Unit
                        if (upUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube upUnitCube in upUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (upUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (upUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + upUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = upUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.up,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = upUnitCube.CubeTypeValue
                                });

                            }
                        }


                        //check match for Left Unit
                        if (leftUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube leftUnitCube in leftUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (leftUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (leftUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + leftUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = leftUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.left,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = leftUnitCube.CubeTypeValue
                                });

                            }
                        }


                        //check match for Right Unit
                        if (rightUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube rightUnitCube in rightUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (rightUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (rightUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + rightUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = rightUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.right,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = rightUnitCube.CubeTypeValue
                                });

                            }
                        }

                        break;
                    default:
                        Debug.LogWarning("Unexpected CubeType: " + listCenterGameBoardUnitCube[i].CubeTypeValue); // Log a warning for unexpected types
                        break;
                }
            }
            return listGameBoardUnitMatchData;
        }

        public void HandleMatchingAtBoardPosition(Vector2 boardPosition)
        {
            List<GameBoardUnitMatchData> listGameBoardUnitMatchData = new List<GameBoardUnitMatchData>();


            int xAxis = (int)boardPosition.x;
            int yAxis = (int)boardPosition.y;
            if (xAxis < 0 || yAxis < 0 || xAxis > boardWidth - 1 || yAxis > boardHeight - 1)
            {
                //Debug.Log("Invalid boardPosition: " + boardPosition);
                return;
            }

            GameBoardUnit centerUnit = gameBoardUnit2DMatrix[xAxis][yAxis];

            if (centerUnit == null)
            {
                //Debug.Log("there is no unit on this tile: " + xAxis + "-" + yAxis);
                return;
            }
            //Debug.Log("thasd: " + xAxis + "-" + yAxis);

            GameBoardUnit downUnitOfCenterUnit = yAxis - 1 >= 0 ? gameBoardUnit2DMatrix[xAxis][yAxis - 1] : null;
            GameBoardUnit upUnitOfCenterUnit = yAxis + 1 < boardHeight ? gameBoardUnit2DMatrix[xAxis][yAxis + 1] : null;
            GameBoardUnit leftUnitOfCenterUnit = xAxis - 1 >= 0 ? gameBoardUnit2DMatrix[xAxis - 1][yAxis] : null;
            GameBoardUnit rightUnitOfCenterUnit = xAxis + 1 < boardWidth ? gameBoardUnit2DMatrix[xAxis + 1][yAxis] : null;

            //Debug.Log(downUnitOfCenterUnit);
            //Debug.Log(upUnitOfCenterUnit);
            //Debug.Log(leftUnitOfCenterUnit);
            //Debug.Log(rightUnitOfCenterUnit);

            //iterate through each cube in center unit
            List<GameBoardUnitCube> listCenterGameBoardUnitCube = centerUnit.GetListGameBoardUnitCube();
            //Debug.Log("iteration: " + listCenterGameBoardUnitCube.Count);

            for (int i = 0; i < listCenterGameBoardUnitCube.Count; i++)
            {
                GameBoardUnitCube centerUnitCube = listCenterGameBoardUnitCube[i];
                switch (centerUnitCube.CubeTypeValue)
                {
                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:

                        break;

                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:

                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:

                        break;

                    case GameBoardUnitCube.CubeType.HalfVerticalRight:

                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftDown:

                        break;

                    case GameBoardUnitCube.CubeType.QuarterLeftUp:

                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightDown:


                        break;

                    case GameBoardUnitCube.CubeType.QuarterRightUp:

                        break;

                    case GameBoardUnitCube.CubeType.Whole:
                        // check match for Down Unit
                        if (downUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube downUnitCube in downUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (downUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (downUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + downUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = downUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.down,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = downUnitCube.CubeTypeValue
                                });

                            }
                        }


                        // check match for Up Unit
                        if (upUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube upUnitCube in upUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (upUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (upUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + upUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = upUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.up,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = upUnitCube.CubeTypeValue
                                });

                            }
                        }


                        //check match for Left Unit
                        if (leftUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube leftUnitCube in leftUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (leftUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (leftUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + leftUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = leftUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.left,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = leftUnitCube.CubeTypeValue
                                });

                            }
                        }


                        //check match for Right Unit
                        if (rightUnitOfCenterUnit != null)
                        {
                            foreach (GameBoardUnitCube rightUnitCube in rightUnitOfCenterUnit.GetListGameBoardUnitCube())
                            {
                                if (rightUnitCube.CubeColorValue != centerUnitCube.CubeColorValue)
                                {
                                    // color not match -> not matched cubes
                                    continue;
                                }

                                bool isMatched = false;
                                switch (rightUnitCube.CubeTypeValue)
                                {
                                    case GameBoardUnitCube.CubeType.HalfHorizontalDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfHorizontalUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalLeft:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.HalfVerticalRight:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftDown:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterLeftUp:
                                        isMatched = true;
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightDown:
                                        break;
                                    case GameBoardUnitCube.CubeType.QuarterRightUp:
                                        break;
                                    case GameBoardUnitCube.CubeType.Whole:
                                        isMatched = true;
                                        break;
                                    default:
                                        Debug.LogWarning("Unexpected CubeType: " + rightUnitCube.CubeTypeValue); // Log a warning for unexpected types
                                        break;
                                }
                                if (isMatched == false)
                                {
                                    // there is no case of matching
                                    continue;
                                }

                                listGameBoardUnitMatchData.Add(new GameBoardUnitMatchData
                                {
                                    centerUnit = centerUnit,
                                    theOtherUnit = rightUnitOfCenterUnit,
                                    directionFromCenterToTheOther = Vector2.right,
                                    matchedCubeTypeCenterUnit = centerUnitCube.CubeTypeValue,
                                    matchedCubeTypeTheOtherUnit = rightUnitCube.CubeTypeValue
                                });

                            }
                        }

                        break;
                    default:
                        Debug.LogWarning("Unexpected CubeType: " + listCenterGameBoardUnitCube[i].CubeTypeValue); // Log a warning for unexpected types
                        break;
                }
            }

            //Done iteration, handle matching data list
            // first, we destroy all matched cubes
            foreach (var gameBoardUnitMatchData in listGameBoardUnitMatchData)
            {
                gameBoardUnitMatchData.centerUnit.DestroyMatchedCube(gameBoardUnitMatchData.matchedCubeTypeCenterUnit);
                gameBoardUnitMatchData.theOtherUnit.DestroyMatchedCube(gameBoardUnitMatchData.matchedCubeTypeTheOtherUnit);
            }

            //Debug.Log(listGameBoardUnitMatchData.Count);
            if (listGameBoardUnitMatchData.Count > 0)
            {
                //if there is at least a match happened
                //then we TRY TO resize all 5 cubes: center, down, up, left, right
                // then we store if a Resize Happened

                //pullingDirectionOfCenterUnit is kind of special as it is related to 4 diagonal cases when there are 2 quarter cubes left
                Vector2 pullingDirectionOfCenterUnit = Vector2.down; //default value
                centerUnit?.ResizeCubes(pullingDirectionOfCenterUnit);
                downUnitOfCenterUnit?.ResizeCubes(Vector2.up);
                upUnitOfCenterUnit?.ResizeCubes(Vector2.down);
                leftUnitOfCenterUnit?.ResizeCubes(Vector2.right);
                rightUnitOfCenterUnit?.ResizeCubes(Vector2.left);

                //after done handling for this match turn, we need to check again for all 5 cubes center, down, up, left, right as they may have changed
                HandleMatchingAtBoardPosition(boardPosition);
                HandleMatchingAtBoardPosition(boardPosition + Vector2.down);
                HandleMatchingAtBoardPosition(boardPosition + Vector2.up);
                HandleMatchingAtBoardPosition(boardPosition + Vector2.left);
                HandleMatchingAtBoardPosition(boardPosition + Vector2.right);
            }
        }

        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 result = Vector3.zero;

            Vector3 mousePosition = Input.mousePosition;
            float targetZ = 0f; // Change this value depending on the Z-coordinate of your target.
            float distanceFromCamera = Mathf.Abs(targetZ - Camera.main.transform.position.z);
            mousePosition.z = distanceFromCamera;

            result = Camera.main.ScreenToWorldPoint(mousePosition);

            return result;

        }

        public Vector3 GetSnapToGridPosition(Vector3 position)
        {
            Vector3Int cellPostion = gridLayout.WorldToCell(position);
            cellPostion.z = 0;
            return cellPostion;
        }

        // Method to check if there is anything in the tile.
        public bool IsValidTile(Vector3 tilePosition)
        {
            if (ListUnavailableBoardPosition.Contains((Vector2)tilePosition))
            {
                Debug.Log("this tile is in unavailable list");
                return false;
            }

            if (tilePosition.x < 0 || tilePosition.x > boardWidth - 1 || tilePosition.y < 0 || tilePosition.y > boardHeight - 1)
            {
                Debug.Log("this tile is outside of the board");
                return false;
            }

            if (gameBoardUnit2DMatrix[(int)tilePosition.x][(int)tilePosition.y] == null)
            {
                //Debug.Log("Tile is empty.");
                return true;
            }
            else
            {
                Debug.Log("Tile is occupied by: " + gameBoardUnit2DMatrix[(int)tilePosition.x][(int)tilePosition.y].name);
                return false;
            }
        }

        //// Method to check if there is anything in the tile.
        //public bool IsTileOccupied(Vector3 tileCenter)
        //{
        //    // Size of one grid tile (adjust to match your grid setup)
        //    Vector3 tileSize = new Vector3(0.1f, 0.1f, 0.1f);

        //    Vector3 halfExtents = tileSize / 2f;

        //    // Get all colliders that overlap with the box area.
        //    Collider[] colliders = Physics.OverlapBox(tileCenter, halfExtents, Quaternion.identity);

        //    // Check if any colliders are detected.
        //    if (colliders.Length > 0)
        //    {
        //        foreach (var collider in colliders)
        //        {
        //            Debug.Log("Tile is occupied by: " + collider.name);
        //        }
        //        return true; // There is at least one collider in the tile. 
        //    }
        //    else
        //    {
        //        Debug.Log("Tile is empty.");
        //        return false; // No colliders detected.
        //    }
        //}
    }
}
