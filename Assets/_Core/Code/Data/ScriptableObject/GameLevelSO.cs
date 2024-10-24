using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace JellyField
{
    [CreateAssetMenu(fileName = "GameLevelSO", menuName = "ScriptableObject/GameLevelSO")]
    public class GameLevelSO : ScriptableObject
    {
        public SceneGameLevel sceneGameLevel;
        [Range(0, 100)]
        public int boardHeight;
        [Range(0, 100)]
        public int boardWidth;
        public List<Vector2> listUnavailableBoardPosition;
        public List<SpawnDataGameBoardUnit> listSpawnDataGameBoardUnit;
        public List<WinCondition> listWinCondition;

        public List<Vector2> listSpawnGameBoardUnitPosition;


        public GameLevelSO nextGameLevelSO;
        
    }
}
