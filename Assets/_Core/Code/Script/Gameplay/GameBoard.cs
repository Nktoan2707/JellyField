using System.Collections;
using System.Collections.Generic;
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

        public GameObject prefab1;
        public GameObject prefab2;

        private PlaceableObject placeableObject;


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
            if (Input.GetKeyDown(KeyCode.A))
            {
                InitializeWithObject(prefab1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                InitializeWithObject(prefab2);
            }
        }

        #endregion


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



        public void InitializeWithObject(GameObject prefab)
        {
            Vector3 position = GetSnapToGridPosition(new Vector3(2, -3, 0));
            GameObject obj = Instantiate(prefab, position, Quaternion.identity);

            placeableObject = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<DraggableGameBoardUnit>();
        }

        // Method to check if there is anything in the tile.
        public bool IsValidTile(Vector3 tileCenter)
        {
            if (ListUnavailableBoardPosition.Contains((Vector2)tileCenter))
            {
                Debug.Log("this tile is in unavailable list");
                return false;
            }

            if (tileCenter.x < 0 || tileCenter.x > boardWidth - 1 || tileCenter.y < 0 || tileCenter.y > boardHeight - 1)
            {
                Debug.Log("this tile is outside of the board");
                return false;
            }

            if (gameBoardUnit2DMatrix[(int)tileCenter.x][(int)tileCenter.y] == null)
            {
                Debug.Log("Tile is empty.");
                return true;
            }
            else
            {
                Debug.Log("Tile is occupied by: " + gameBoardUnit2DMatrix[(int)tileCenter.x][(int)tileCenter.y].name);
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
