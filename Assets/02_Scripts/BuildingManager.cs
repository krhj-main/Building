using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject pendingObject;
    Vector3 pos;

    RaycastHit hit;

    [SerializeField] LayerMask layerMask;

    public float gridSize;
    bool gridOn = true;
    [SerializeField] Toggle gridToggle;

    bool rotateOn = false;
    [SerializeField] float rotationAngle;

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
            pos.y = 0.5f;
        }
    }

    public void SelectObject(int _index)
    {
        pendingObject = Instantiate(objects[_index], pos, transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(RoundToNearestGrid(pos.x), RoundToNearestGrid(0.5f), RoundToNearestGrid(pos.z));
            }
            else
            {
                pendingObject.transform.position = pos;
            }
            if (rotateOn)
            {
                
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                pendingObject.transform.eulerAngles += new Vector3(0, rotationAngle, 0);
                rotateOn = !rotateOn;
            }
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float RoundToNearestGrid(float _pos)
    {
        float xDiff = _pos % gridSize;
        _pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            _pos += gridSize;
        }
        return _pos;
    }

    void PlaceObject()
    {
        pendingObject = null;
    }
}
