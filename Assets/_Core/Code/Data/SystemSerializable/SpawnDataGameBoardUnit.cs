using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    [System.Serializable]
    public struct SpawnDataGameBoardUnit
    {
        public GameBoardUnit gameBoardUnitPrefab;
        public Vector2 position;
        [Header("at max 4 first elements are valid and used, duplicates are not valid.")]
        [Tooltip("at max 4 first elements are valid and used, duplicates are not valid.")]
        public List<SpawnDataGameBoardUnitCube> listSpawnDataGameBoardUnitCube;
    }
}
