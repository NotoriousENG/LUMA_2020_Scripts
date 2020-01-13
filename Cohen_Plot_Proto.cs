using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohen_Plot_Proto : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform plot;
    public GameObject building;
    private GameObject newBuilding = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            newBuilding = Instantiate(building, transform.position, Quaternion.identity);
            newBuilding.transform.position = new Vector3(newBuilding.transform.position.x, 
                newBuilding.transform.position.y + (newBuilding.transform.localScale.y * 0.5f) + (transform.localScale.y * 0.5f), 
                newBuilding.transform.position.z);
        }
    }
}
