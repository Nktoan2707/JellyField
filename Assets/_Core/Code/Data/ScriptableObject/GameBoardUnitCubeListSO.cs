using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    [CreateAssetMenu(fileName = "GameBoardUnitCubeListSO", menuName = "ScriptableObject/GameBoardUnitCubeListSO")]
    public class GameBoardUnitCubeListSO : ScriptableObject
    {
        public GameBoardUnitCube gameBoardUnitCubeWhole;
        public GameBoardUnitCube gameBoardUnitCubeHalfHorizontalUp;
        public GameBoardUnitCube gameBoardUnitCubeHalfHorizontalDown;
        public GameBoardUnitCube gameBoardUnitCubeHalfVerticalLeft;
        public GameBoardUnitCube gameBoardUnitCubeHalfVerticalRight;
        public GameBoardUnitCube gameBoardUnitCubeQuarterLeftUp;
        public GameBoardUnitCube gameBoardUnitCubeQuarterRightUp;
        public GameBoardUnitCube gameBoardUnitCubeQuarterLeftDown;
        public GameBoardUnitCube gameBoardUnitCubeQuarterRightDown;
    }
}
