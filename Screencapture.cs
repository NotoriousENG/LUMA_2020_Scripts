using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Screencapture : MonoBehaviour
{
    //Gotten from https://www.youtube.com/watch?v=d-56p770t0U

    //Twitter example https://stackoverflow.com/questions/37208990/unity3d-upload-a-image-to-twitter
    // VIDEO https://www.youtube.com/watch?v=cPmvZbMLq3A
    // Possible Asset Pack https://assetstore.unity.com/detail/tools/integration/cross-platform-native-plugins-lite-version-37272

    Camera snapCam;
    int resWidth = 500;
    int resHeight = 500;
    // Start is called before the first frame update
    void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            //NEED TO FIND OUT SCREENSIZE OF DEVICE TO MAKE THIS WORK PROPERLY
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    public void CallTakeSnapshot()
    {
        snapCam.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (snapCam.gameObject.activeInHierarchy)
        {
            Texture2D snapShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

            byte[] bytes = snapShot.EncodeToPNG();
            string fileName = SnapShotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log("Snapshot Taken");
            snapCam.gameObject.SetActive(false);
        }
    }

    string SnapShotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
                Application.dataPath,
                resWidth, 
                resHeight,
                System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
