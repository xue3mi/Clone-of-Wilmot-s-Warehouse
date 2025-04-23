using UnityEngine;

public class LightningTile : MonoBehaviour
{
        private SpriteRenderer spriteRenderer;
        private GridObject gridObject;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            //attach GridObject component to all blocks Prefab
            gridObject = GetComponent<GridObject>();
        }

        public void UpdateLight(Vector2Int playerGridPos)
        {
            int dx = Mathf.Abs(gridObject.gridPosition.x - playerGridPos.x);
            int dy = Mathf.Abs(gridObject.gridPosition.y - playerGridPos.y);

            float euclideanDistance = Vector2.Distance(gridObject.gridPosition, playerGridPos);

            // white sqaure radius=2
            if (dx <= 2 && dy <= 2)
            {
                spriteRenderer.color = Color.white;
            }
            // grey circle radius=3.5
            else if (euclideanDistance <= 3.5f)
            {
                spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
            }
            // black sqaure radius=3.5
            else
            {
                spriteRenderer.color = Color.black;
            }
        }

}
