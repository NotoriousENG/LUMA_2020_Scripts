using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPLandCreation : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class ExtraLand
    {
        public GameObject[] extraLandPrefabs;
        public GameObject extraLandNodeParent;
    }
    public ExtraLand extraLand;
    private Transform[] extraLandNodes;

    void Start()
    {
        extraLandNodes = extraLand.extraLandNodeParent.GetComponentsInChildren<Transform>();
        for (int i = 1; i < extraLandNodes.Length; i++)
        { 
            GameObject obj = Instantiate(extraLand.extraLandPrefabs[Random.Range(0, extraLand.extraLandPrefabs.Length)]);
            obj.transform.parent = extraLandNodes[i].transform;
            obj.transform.position = obj.transform.parent.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
