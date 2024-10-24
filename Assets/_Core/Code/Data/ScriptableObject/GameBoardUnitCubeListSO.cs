using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    [CreateAssetMenu(fileName = "GameBoardUnitCubeListSO", menuName = "ScriptableObject/GameBoardUnitCubeListSO")]
    public class GameBoardUnitCubeListSO : ScriptableObject
    {
        public GameBoardUnitCube gameBoardUnitCubeHalfHorizontalDown;
        public GameBoardUnitCube gameBoardUnitCubeHalfHorizontalUp;
        public GameBoardUnitCube gameBoardUnitCubeHalfVerticalLeft;
        public GameBoardUnitCube gameBoardUnitCubeHalfVerticalRight;
        public GameBoardUnitCube gameBoardUnitCubeQuarterLeftDown;
        public GameBoardUnitCube gameBoardUnitCubeQuarterLeftUp;
        public GameBoardUnitCube gameBoardUnitCubeQuarterRightDown;
        public GameBoardUnitCube gameBoardUnitCubeQuarterRightUp;
        public GameBoardUnitCube gameBoardUnitCubeWhole;
    }
}
