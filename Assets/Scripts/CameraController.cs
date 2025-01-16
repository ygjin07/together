using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float ScrollSpeed, MoveSpeed;
    public float MaxZoom, MinZoom;
    public float MaxX, MaxY;
    public GameObject OutLine;

    private Camera thisCamera;
    private Vector2 defaultOutLineSize = new Vector2(0.456f, 0.256f);

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraScroll();
        CameraMove();
    }

    void CameraScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        Vector3 cameraViewPortRU = thisCamera.ViewportToWorldPoint(new Vector2(1, 1));
        float x = thisCamera.transform.position.x, y = thisCamera.transform.position.y;
        float cameraSizeX, cameraSizeY;
        float mx, my;

        thisCamera.orthographicSize = Mathf.Clamp(thisCamera.orthographicSize - scroll, MinZoom, MaxZoom);
        UIManager.UM.SetZoomGauge((thisCamera.orthographicSize - MinZoom) / (MaxZoom - MinZoom));
        OutLine.transform.localScale = defaultOutLineSize * thisCamera.orthographicSize;
        x = thisCamera.transform.position.x;
        y = thisCamera.transform.position.y;
        cameraSizeX = cameraViewPortRU.x - x;
        cameraSizeY = cameraViewPortRU.y - y;
        mx = MaxX - cameraSizeX;
        my = MaxY - cameraSizeY;
        x = Mathf.Clamp(x, -mx, mx);
        y = Mathf.Clamp(y, -my, my);
        thisCamera.transform.position = new Vector3(x, y, -10);
    }

    void CameraMove()
    {
        Vector3 mousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition), cameraViewPortRU = thisCamera.ViewportToWorldPoint(new Vector2(1, 1)), cameraViewPortLD = thisCamera.ViewportToWorldPoint(new Vector2(0, 0));
        float viewR = cameraViewPortRU.x, viewU = cameraViewPortRU.y, viewL = cameraViewPortLD.x, viewD = cameraViewPortLD.y, x = thisCamera.transform.position.x, y = thisCamera.transform.position.y;
        if(mousePos.x >= viewR - 0.05)
        {
            x = Mathf.Clamp(viewR + MoveSpeed * Time.deltaTime, -MaxX, MaxX) - (viewR - x);
        }
        else if (mousePos.x <= viewL + 0.05)
        {
            x = Mathf.Clamp(viewL - MoveSpeed * Time.deltaTime, -MaxX, MaxX) - (viewL - x);
        }
        if (mousePos.y >= viewU - 0.05)
        {
            y = Mathf.Clamp(viewU + MoveSpeed * Time.deltaTime, -MaxY, MaxY) - (viewU - y);
        }
        else if (mousePos.y <= viewD + 0.05)
        {
            y = Mathf.Clamp(viewD - MoveSpeed * Time.deltaTime, -MaxY, MaxY) - (viewD - y);
        }
        thisCamera.transform.position = new Vector3(x, y, -10);
    }
}
