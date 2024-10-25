using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JellyField
{
    public class GameBoardUnit : MonoBehaviour
    {
        public static int MAXIMUM_GAME_BOARD_UNIT_TOTAL_CUBE_VALUE = 4;
        List<GameBoardUnitCube> listGameBoardUnitCube = new List<GameBoardUnitCube>();
        public List<GameBoardUnitCube> GetListGameBoardUnitCube()
        {
            return listGameBoardUnitCube;
        }

        public void DestroyMatchedCube(GameBoardUnitCube.CubeType cubeType)
        {
            for (int i = 0; i < listGameBoardUnitCube.Count; i++)
            {
                GameBoardUnitCube gameBoardUnitCube = listGameBoardUnitCube[i];
                if (gameBoardUnitCube.CubeTypeValue == cubeType)
                {
                    listGameBoardUnitCube.Remove(gameBoardUnitCube);
                    Destroy(gameBoardUnitCube.gameObject);
                    i--;
                }
            }
        }

        public void ResizeCubes(Vector2 pullingDirection)
        {
            if (pullingDirection != Vector2.left && pullingDirection != Vector2.right && pullingDirection != Vector2.down && pullingDirection != Vector2.up)
            {
                throw new System.Exception("Invalid pulling direction!");
            }

            //here we will have at most 2 cubes that need to be resized => store their references and new corresponding cubes replacing them.
            List<GameBoardUnitCube> listToBeDestroyedCube = new List<GameBoardUnitCube>();
            List<GameBoardUnitCube.CubeType> listNewCubeType = new List<GameBoardUnitCube.CubeType>();


            int remainingCubeValue = 0;
            foreach (var i in listGameBoardUnitCube)
            {
                print(i);
                remainingCubeValue += GameBoardUnitCube.GetCubeTypeValue(i.CubeTypeValue);
            }
            print(remainingCubeValue);
            if (remainingCubeValue == 0)
            {
                GameBoard.Instance.GetGameBoardUnit2DMatrix()[(int)transform.position.x][(int)transform.position.y] = null;
                Destroy(gameObject);
                return;
            }
            else if (remainingCubeValue == 1)
            {
                // only 1 quarter cube left
                listToBeDestroyedCube.Add(listGameBoardUnitCube[0]);
                listNewCubeType.Add(GameBoardUnitCube.CubeType.Whole);
            }
            else if (remainingCubeValue == 2)
            {
                var halfCube = listGameBoardUnitCube.Find(item => GameBoardUnitCube.GetCubeTypeValue(item.CubeTypeValue) == GameBoardUnitCube.GetCubeTypeValue(GameBoardUnitCube.CubeType.HalfHorizontalDown));
                if (halfCube != null)
                {
                    // if the only one left is halfCube

                    listToBeDestroyedCube.Add(listGameBoardUnitCube[0]);
                    listNewCubeType.Add(GameBoardUnitCube.CubeType.Whole);
                }
                else
                {
                    // there are 2 quarter cubes left, we will examine their positions, there are total 4 * 3 = 12 cases
                    // REMEMBER THAT the order of adding listNewCubeType is important
                    if (listGameBoardUnitCube.Count != 2)
                    {
                        throw new System.Exception();
                    }

                    var quarterCubeFirst = listGameBoardUnitCube[0];
                    var quarterCubeSecond = listGameBoardUnitCube[1];

                    listToBeDestroyedCube.Add(quarterCubeFirst);
                    listToBeDestroyedCube.Add(quarterCubeSecond);

                    //4 Horizontal cases
                    if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                    }


                    //4 Vertical cases
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                    }

                    //2 main diagonal cases
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp)
                    {
                        if (pullingDirection == Vector2.left || pullingDirection == Vector2.right)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        }
                        else if (pullingDirection == Vector2.down || pullingDirection == Vector2.up)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        }
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown)
                    {
                        if (pullingDirection == Vector2.left || pullingDirection == Vector2.right)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        }
                        else if (pullingDirection == Vector2.down || pullingDirection == Vector2.up)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        }
                    }

                    //2 sub-diagonal cases
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown)
                    {
                        if (pullingDirection == Vector2.left || pullingDirection == Vector2.right)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                        }
                        else if (pullingDirection == Vector2.down || pullingDirection == Vector2.up)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                        }
                    }
                    else if (quarterCubeFirst.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown
                        && quarterCubeSecond.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp)
                    {
                        if (pullingDirection == Vector2.left || pullingDirection == Vector2.right)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                        }
                        else if (pullingDirection == Vector2.down || pullingDirection == Vector2.up)
                        {
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                            listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                        }
                    }

                }

            }
            else if (remainingCubeValue == 3)
            {
                var halfCube = listGameBoardUnitCube.Find(item => GameBoardUnitCube.GetCubeTypeValue(item.CubeTypeValue) == GameBoardUnitCube.GetCubeTypeValue(GameBoardUnitCube.CubeType.HalfHorizontalDown));
                if (halfCube != null)
                {
                    // if there is a half cube in the unit, then there is only 1 quarter cube
                    var quarterCube = listGameBoardUnitCube.Find(item => item != halfCube);
                    if (quarterCube == null)
                    {
                        throw new System.Exception("Something went wrong, a quarter cube must be here, not null");
                    }

                    listToBeDestroyedCube.Add(quarterCube);

                    if (halfCube.CubeTypeValue == GameBoardUnitCube.CubeType.HalfHorizontalDown)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                    }
                    else if (halfCube.CubeTypeValue == GameBoardUnitCube.CubeType.HalfHorizontalUp)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                    }
                    else if (halfCube.CubeTypeValue == GameBoardUnitCube.CubeType.HalfVerticalLeft)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                    }
                    else if (halfCube.CubeTypeValue == GameBoardUnitCube.CubeType.HalfVerticalRight)
                    {
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                    }
                }
                else
                {
                    // there are 3 quarter cubes left
                    if (pullingDirection == Vector2.down)
                    {
                        // if the match counterpart unit is below this unit => the disappeared one is a down quarter cube => find it to resize it to a half horizontal down
                        var quarterCubeDown = listGameBoardUnitCube.Find(item => item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown ||
                        item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown);

                        if (quarterCubeDown == null)
                        {
                            throw new System.Exception("Something went wrong, a quarter cube must be here, not null");
                        }

                        listToBeDestroyedCube.Add(quarterCubeDown);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalDown);
                    }
                    else if (pullingDirection == Vector2.up)
                    {
                        // if the match counterpart unit is above this unit => the disappeared one is a up quarter cube => find it to resize it to a half horizontal up
                        var quarterCubeUp = listGameBoardUnitCube.Find(item => item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp ||
                        item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp);

                        if (quarterCubeUp == null)
                        {
                            throw new System.Exception("Something went wrong, a quarter cube must be here, not null");
                        }

                        listToBeDestroyedCube.Add(quarterCubeUp);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfHorizontalUp);
                    }
                    else if (pullingDirection == Vector2.left)
                    {
                        // if the match counterpart unit is on the left of this unit => the disappeared one is a left quarter cube => find it to resize it to a half vertical left
                        var quarterCubeLeft = listGameBoardUnitCube.Find(item => item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftUp ||
                        item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterLeftDown);

                        if (quarterCubeLeft == null)
                        {
                            throw new System.Exception("Something went wrong, a quarter cube must be here, not null");
                        }

                        listToBeDestroyedCube.Add(quarterCubeLeft);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalLeft);
                    }
                    else if (pullingDirection == Vector2.left)
                    {
                        // if the match counterpart unit is on the right of this unit => the disappeared one is a right quarter cube => find it to resize it to a half vertical right
                        var quarterCubeRight = listGameBoardUnitCube.Find(item => item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightUp ||
                        item.CubeTypeValue == GameBoardUnitCube.CubeType.QuarterRightDown);

                        if (quarterCubeRight == null)
                        {
                            throw new System.Exception("Something went wrong, a quarter cube must be here, not null");
                        }

                        listToBeDestroyedCube.Add(quarterCubeRight);
                        listNewCubeType.Add(GameBoardUnitCube.CubeType.HalfVerticalRight);
                    }
                }
            }
            else
            {
                throw new System.Exception("Something went wrong, after a unit matching, it should be less then 4");
            }


            //Destroy old cubes and replace with new cubes
            if (listToBeDestroyedCube.Count != listNewCubeType.Count)
            {
                throw new System.Exception($"this must be equal! listToBeDestroyedCube: {listToBeDestroyedCube.Count} - listNewCubeType: {listNewCubeType.Count}");
            }

            for (int i = 0; i < listToBeDestroyedCube.Count; i++)
            {
                var newGameBoardUnitCube = GameBoardUnitCube.SpawnCubeByType(listNewCubeType[i], this);
                newGameBoardUnitCube.CubeColorValue = listToBeDestroyedCube[i].CubeColorValue;
                Destroy(listToBeDestroyedCube[i].gameObject);
                listGameBoardUnitCube.Remove(listToBeDestroyedCube[i]);
                listGameBoardUnitCube.Add(newGameBoardUnitCube);
            }
        }
    }
}
