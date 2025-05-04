using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class LightningTile : MonoBehaviour
{
        private SpriteRenderer spriteRenderer;
        private GridObject gridObject;
        
        //add black & white sprite
        public Sprite sprite_2;
        //save the initial sprite
        private Sprite originalSprite;

    void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            //attach GridObject component to all blocks Prefab
            gridObject = GetComponent<GridObject>();
            //save the original sprite
            originalSprite = spriteRenderer.sprite;
        }

        public void UpdateLight(Vector2Int playerGridPos)
        {
            int dx = Mathf.Abs(gridObject.gridPosition.x - playerGridPos.x);
            int dy = Mathf.Abs(gridObject.gridPosition.y - playerGridPos.y);

            float euclideanDistance = Vector2.Distance(gridObject.gridPosition, playerGridPos);

            bool isCenterWhiteSquare = (dx <= 2 && dy <= 2);
            bool isCrossWhiteLine =
                (dx == 0 && dy == 3) ||   // up & down
                (dy == 0 && dx == 3);     // left & right

            // white square + 1 more block on each side
            if (isCenterWhiteSquare || isCrossWhiteLine)
            {
                spriteRenderer.sprite = originalSprite;
                spriteRenderer.color = Color.white;
            }
            // grey circle radius=7
            else if (euclideanDistance <= 7f)
            {
                spriteRenderer.sprite = sprite_2;
                
            }
            // black sqaure radius=7
            else
            {
                spriteRenderer.sprite = sprite_2;
                spriteRenderer.color = Color.black;
            }
        }

}
