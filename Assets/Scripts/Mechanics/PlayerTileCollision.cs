using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileCollision : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool isCollided;
    [SerializeField] private bool inputSpace;
    // Start is called before the first frame update
    void Update()
    {
        if (/*Input.GetKeyDown(KeyCode.Space)*/Input.GetMouseButtonDown(0)) inputSpace = true;
        if (/*Input.GetKeyUp(KeyCode.Space)*/Input.GetMouseButtonUp(0)) inputSpace = false;
        if ((/*Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)*/ Input.GetMouseButton(0) || (inputSpace ^ !isCollided)) && isCollided) player.transform.parent = null;
        else if(isCollided)
            player.transform.SetParent(this.transform);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollided = true;
        player.transform.SetParent(this.transform);
        /*ContactPoint2D[] points = new ContactPoint2D[10];
        collision.GetContacts(points);
        foreach(ContactPoint2D point in points)
        {
            if(point.point.y >= this.gameObject.transform.position.y + this.gameObject.transform.localScale.y/2)
            {
                isCollided = true;
                player.transform.SetParent(this.transform);
                break;
            }
        }*/
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollided = false;
        inputSpace = false; 
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isCollided = true;
    }
}
