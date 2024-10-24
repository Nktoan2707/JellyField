using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    [System.Serializable]
    public struct SpawnDataGameBoardUnitCube
    {
        public GameBoardUnitCube gameBoardUnitCubePrefab;
        public GameBoardUnitCube.CubeType gameBoardCubeType;
        public GameBoardUnitCube.CubeColor color;
    }
}
