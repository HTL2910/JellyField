using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCube : MonoBehaviour
{
    private Map maps;
    private Vector3 offset;  
    private float zCoord;
    private Vector3 currentPos;

    private void Awake()
    {
        maps=FindObjectOfType<Map>();
    }
    private void Start()
    {
        currentPos= transform.position;
        
    }
    void OnMouseDown()
    {
        if (gameObject.GetComponent<CubeInteract>().isMove)
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            offset = gameObject.transform.position - GetMouseWorldPos();
        }
       
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (gameObject.GetComponent<CubeInteract>().isMove)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        if (gameObject.GetComponent<CubeInteract>().isMove)
        {
            Vector3 currentPosition = transform.position;

            int snappedX = (int)Mathf.Max(0, Mathf.Round(currentPosition.x));
            int snappedY = (int)Mathf.Max(0, Mathf.Round(currentPosition.y));
            transform.position = currentPos;
            if (snappedX >= 0 && snappedX < maps.map.GetLength(0) && snappedY >= 0 && snappedY < maps.map.GetLength(1))
            {
                if (maps.map[snappedX, snappedY] == null)
                {
                    transform.position = new Vector3(snappedX, snappedY, currentPosition.z);
                    transform.parent = maps.transform.gameObject.transform;
                    gameObject.GetComponent<CubeInteract>().isMove = false;
                    maps.map[snappedX, snappedY] = this.gameObject;

                    maps.SpawnNewCube();
                }
            }
        }
        

    }
}
