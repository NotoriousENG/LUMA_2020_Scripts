using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraZoom
{
    //static float minFov = 15f;
    //static float maxFov = 90f;
    static float zoomOutAmount = 2f;
    // Start is called before the first frame update

    // Update is called once per frame

    public static void CameraZoomOut(MonoBehaviour mono, float amount)
    {
        mono.StartCoroutine(ZoomOut(amount));
    }

    public static void CameraMoveToPlot(MonoBehaviour mono, GameObject gmObj)
    {
        // mono.StartCoroutine(goToNewPlot(gmObj));
        
        Camera.main.transform.position = new Vector3 (gmObj.transform.position.x, Camera.main.transform.position.y /* gmObj.transform.position.y */,Camera.main.transform.position.z);
        Camera.main.transform.LookAt(gmObj.transform.position, new Vector3 (0,1,-1));
    }

    static IEnumerator ZoomOut(float amount)
    {
        //float fov = Camera.main.fieldOfView;
        //float minCameraZ = Camera.main.transform.localPosition.z;
        //float maxCameraZ = minCameraZ + zoomOutAmount;
        //float cameraZ = minCameraZ;

        //float minCameraY = Camera.main.transform.localPosition.y;
        //float maxCameraY = minCameraY + zoomOutAmount;
        //float cameraY = minCameraY;
        float counter = 0.0f;
        while (counter < zoomOutAmount)
        {
            counter += 0.1f;
            float R = amount * 15;                                                   //The radius from current camera
            float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
            float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right
            PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
            PosY = PosY / 180 * Mathf.PI;                                       //^
            float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
            float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
            float Y = R * Mathf.Cos(PosX);                                      //^
            float CamX = Camera.main.transform.position.x;                      //Get current camera postition for the offset
            float CamY = Camera.main.transform.position.y;                      //^
            float CamZ = Camera.main.transform.position.z;                      //^
            Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
            yield return new WaitForSeconds(0.01f);
        }
       
    }
    static IEnumerator goToNewPlot(GameObject plot)
    {
        float currCameraX = Camera.main.transform.position.x;
        float plotCameraX = plot.transform.position.x;
        float cameraX = Camera.main.transform.position.x;

        float currCameraY = Camera.main.transform.position.y;
        float plotCameraY = plot.transform.position.y;
        float cameraY = Camera.main.transform.position.y;

        float currCameraZ = Camera.main.transform.position.z;
        float plotCameraZ = plot.transform.position.z;
        float cameraZ = Camera.main.transform.position.z;

        float zOffset = 0;

        if (currCameraZ > 0 && plotCameraZ > 0)
        {
            zOffset = Mathf.Abs(currCameraZ - plotCameraZ);
        }
        else if (currCameraZ < 0 && plotCameraZ > 0)
        {
            zOffset = Mathf.Abs(Mathf.Abs(currCameraZ) - Mathf.Abs( plotCameraZ));
        }
        else if (currCameraZ > 0 && plotCameraZ < 0)
        {
            zOffset =(-1) * Mathf.Abs(Mathf.Abs(currCameraZ) - Mathf.Abs(plotCameraZ));
        }
        else if (currCameraZ < 0 && plotCameraZ < 0)
        {
            zOffset = (-1) * Mathf.Abs(currCameraZ - plotCameraZ);
        }

        Vector3 cameraVector = new Vector3(currCameraX, currCameraY, currCameraZ);
        if (plotCameraZ < 0)
        {
            plotCameraZ = plotCameraZ * -1;
        }
        Vector3 plotVector = new Vector3(plotCameraX, currCameraY, (currCameraZ-plotCameraZ)/2);

        while (cameraVector != plotVector)
        {
            /*
            if (currCameraX < plotCameraX) cameraX += 0.2f;
            if (currCameraX > plotCameraX) cameraX -= 0.2f;
            if (currCameraY < plotCameraY) cameraY += 0.2f;
            if (currCameraY > plotCameraY) cameraY -= 0.2f;
            if (currCameraX < plotCameraZ) cameraZ += 0.2f;
            if (currCameraX > plotCameraZ) cameraZ -= 0.2f;
            */
            cameraVector = Vector3.MoveTowards(cameraVector, plotVector, 10f * Time.deltaTime);
            Camera.main.transform.position = cameraVector;
            //idkMan += 0.2f;
            yield return new WaitForSeconds(0.01f);
        }

       // yield return new WaitForSeconds(0.01f);
    }
}
