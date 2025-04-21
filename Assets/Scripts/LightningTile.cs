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
            int distance = Mathf.Abs(playerGridPos.x - gridObject.gridPosition.x) + Mathf.Abs(playerGridPos.y - gridObject.gridPosition.y);

            //illuminate distance, white
            if (distance <= 5)
            {
                spriteRenderer.color = Color.white;
            }
            // within 7 tiles, grey
            else if (distance <= 7)
            {
                spriteRenderer.color = new Color(0.6f, 0.6f, 0.6f);
            }
            // outside 7 tiles, black
            else
            {
                spriteRenderer.color = Color.black;
            }
        }
    }
