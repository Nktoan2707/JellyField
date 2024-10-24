using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace JellyField
{
    public class DraggableObject : MonoBehaviour
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
            // Center of the tile you want to check.
            Vector3 tileCenter = GameBoard.Instance.GetSnapToGridPosition(placingPosition) + new Vector3(0.5f, 0.5f, -0.5f);

            print(tileCenter);
            if (GameBoard.Instance.IsTileOccupied(tileCenter))
            {
                transform.position = lastestPosition;
                return;
            }

            Vector3 centerOffset = new Vector3(0.5f, 0.5f, 0); // this will make cube's position becomes the center of the tile to snap more easily
            transform.position = GameBoard.Instance.GetSnapToGridPosition(placingPosition + centerOffset);
        }




    }
}
