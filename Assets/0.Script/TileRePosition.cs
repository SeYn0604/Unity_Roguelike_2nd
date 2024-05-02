using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRePosition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        /*float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.lastMoveDirection;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;*/
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffx = Mathf.Abs(dirX);
        float diffy = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;
        switch (transform.tag)
        {
            case "Ground":
                if (Mathf.Abs(diffx - diffy) <= 1f)
                {
                    transform.Translate(Vector3.up * dirY * 60);
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffx < diffy) 
                {
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
        }
    }
}
