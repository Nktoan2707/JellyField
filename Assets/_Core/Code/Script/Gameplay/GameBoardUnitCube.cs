using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    public class GameBoardUnitCube : MonoBehaviour
    {
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
    }
}
