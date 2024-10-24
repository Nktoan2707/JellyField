using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{

    [CreateAssetMenu(fileName = "GameBoardUnitCubeMaterialListSO", menuName = "ScriptableObject/GameBoardUnitCubeMaterialListSO")]

    public class GameBoardUnitCubeMaterialListSO : ScriptableObject
    {
        public Material gameBoardUnitCubeMaterialRed;
        public Material gameBoardUnitCubeMaterialBlue;
    }
}
