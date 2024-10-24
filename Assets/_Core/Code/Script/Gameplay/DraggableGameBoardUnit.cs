using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace JellyField
{
    public class DraggableGameBoardUnit : MonoBehaviour
    {
        private Vector3 draggingOffset = new Vector3(-0.5f, 0.5f, 0);
        private Vector3 liftUpOffset = new Vector3(0, 0, -2f);

        private Vector3 lastestPosition;
        private Vector3 placingPosition;

        private void OnMouseDown()
        {
            lastestPosition = transform.position;
        }

        private void OnMouseDrag()
        {
            Vector3 newDisplayPosition = GameBoard.GetMouseWorldPosition() + draggingOffset + liftUpOffset;
            placingPosition = newDisplayPosition - liftUpOffset;
            transform.position = newDisplayPosition;
        }

        private void OnMouseUp()
        {
            Vector3 centerOffset = new Vector3(0.5f, 0.5f, 0); // this will make cube's position becomes the center of the tile to snap more easily

            // Center of the tile you want to check.
            Vector3 tilePositionOnBoard = GameBoard.Instance.GetSnapToGridPosition(placingPosition + centerOffset);

            //Debug.Log(tilePositionOnBoard);
            if (!GameBoard.Instance.IsValidTile(tilePositionOnBoard))
            {
                transform.position = lastestPosition;
                return;
            }

            transform.position = tilePositionOnBoard;
            GameBoard.Instance.GetGameBoardUnit2DMatrix()[(int)transform.position.x][(int)transform.position.y] = gameObject.GetComponent<GameBoardUnit>();
            GameBoardUnitSpawnManager.Instance.GetSpawnGameBoardUnitPositionValues()[lastestPosition] = false;
            Destroy(this);
        }

    }
}
