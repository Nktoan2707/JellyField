using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    public class GameBoardUnitCube : MonoBehaviour
    {
        public enum CubeType
        {
            Whole,
            HalfHorizontalUp,
            HalfHorizontalDown,
            HalfVerticalLeft,
            HalfVerticalRight,
            QuarterLeftUp,
            QuarterRightUp,
            QuarterLeftDown,
            QuarterRightDown,
        }

        public enum CubeColor
        {
            Red,
            Blue,
            Green,
        }

        public CubeType GameBoardCubeType { get; set; }


        private CubeColor cubeColor;
        public CubeColor GameBoardCubeColor
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
    }
}
