using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour
{
    public GameObject selectedObject;

    public GameObject selectPanel;

    public Text objNameText;
    BuildingManager buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        selectPanel.SetActive(false);
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Selectable"))
                {
                    Select(hit.collider.gameObject);
                }
                else if (!hit.collider.gameObject.CompareTag("Selectable"))
                {
                    DeSelect();
                }
            }
            
        }
        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            DeSelect();
        }
    }

    

    void Select(GameObject _select)
    {
        if (_select == selectedObject)
            return;
        if (selectedObject != null)
            DeSelect();
        //selectedObject = _select;
        Outline outline = _select.GetComponent<Outline>();

        if (outline == null)
        {
            _select.AddComponent<Outline>();
        }
        else
        {
            outline.enabled = true;
        }
        selectedObject = _select;
        objNameText.text = selectedObject.name;
        CreatePanel();
    }
    void CreatePanel()
    {
        selectPanel.SetActive(true);
    }
    void OffPanel()
    {
        selectPanel.SetActive(false);
    }
    void DeSelect()
    {
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
        OffPanel();
    }

    public void MoveableObject()
    {
        BuildingManager bm = GetComponent<BuildingManager>();
        bm.pendingObject = selectedObject;
    }
    public void DestoryObject()
    {
        GameObject deGo = selectedObject;
        Destroy(deGo);
        OffPanel();
    }
}
