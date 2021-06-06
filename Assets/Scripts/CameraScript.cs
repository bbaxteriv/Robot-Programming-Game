//SOURCE: https://forum.unity.com/threads/click-drag-camera-movement.39513/

using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool panning = false;
    /*private void Start()
    {
      //transform.position = new Vector3(0,0,0);
    }
*/
    private void Update()
    {
        
        // When RMB clicked get mouse click position and set panning to true
        if (Input.GetMouseButtonDown(1) && !panning)
        {
                mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                panning = true;
        }
        // If RMB is already clicked, move the camera following the mouse position update
        if (panning)
        {
            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = mouseCurrentPos - mouseClickPos;
            transform.position += new Vector3(-distance.x, -distance.y, 0);
        }

        // If RMB is released, stop moving the camera
        if (Input.GetMouseButtonUp(1))
            panning = false;
    }
}
