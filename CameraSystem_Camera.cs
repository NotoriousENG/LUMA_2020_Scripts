using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CameraSystem
{
    public class CameraSystem_Camera : MonoBehaviour
    {
        //public Vector3 cameraRotation;

        public List<CameraSystem_Spot> cameraSpots = new List<CameraSystem_Spot>();
        public Queue<CameraSystem_Spot> camQ = new Queue<CameraSystem_Spot>();

        Transform camT;

        CameraSystem_Spot currentSpot;
        CameraSystem_Spot targetSpot;

        public AnimationCurve easyAnimCurve;

        [Range(.25f,4)]
        public float globalSpeedMultiplier = 1;

        void Start()
        {
            camT = transform;
            currentSpot = cameraSpots.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList()[0];
            foreach (var spot in cameraSpots)
            {
                camQ.Enqueue(spot);
            }
            //Move to the first queue spot
            SwitchCameraSpots(camQ.Dequeue());
        }

        void Update()
        {
            //camT.localEulerAngles = cameraRotation;
        }

        public void SwitchCameraSpots(CameraSystem_Spot targetSpot)
        {
            this.targetSpot = targetSpot;
            StartCoroutine(SwitchCameraPositions());
        }

        IEnumerator SwitchCameraPositions()
        {
            float animationTime = 0;
            while (animationTime < 1)
            {
                animationTime += Time.deltaTime * targetSpot.animateInSpeed * globalSpeedMultiplier;
                if (animationTime > 1)
                {
                    animationTime = 1;
                }
                camT.transform.position = Vector3.Lerp(currentSpot.transform.position, targetSpot.transform.position, easyAnimCurve.Evaluate(animationTime));
                camT.transform.rotation = Quaternion.Slerp(currentSpot.transform.rotation, targetSpot.transform.rotation, easyAnimCurve.Evaluate(animationTime));
                yield return new WaitForEndOfFrame();
            }

            camT.transform.position = targetSpot.transform.position;
            currentSpot = targetSpot;
        }
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(CameraSystem_Camera))]
    public class CameraSystem_Camera_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            int baseLabelSize = 12;
            GUI.skin.label.fontSize = 25;

            GUILayout.Space(5);
            GUILayout.Label("Camera System");
            GUILayout.Space(10);

            base.OnInspectorGUI();

            CameraSystem_Camera script = (CameraSystem_Camera)target;
            GUILayout.Space(20);

            GUI.skin.label.fontSize = baseLabelSize;

            GUILayout.Label("Press Gather Spots to collect all camera spots.");
            GUILayout.Space(5);

            if(GUILayout.Button("Gather Spots"))
            {
                script.cameraSpots = FindObjectsOfType<CameraSystem_Spot>().ToList();
                script.cameraSpots.Sort((x, y) => x.name.CompareTo(y.name));
                UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
            }
            GUILayout.Space(5);

            foreach (CameraSystem_Spot spot in script.cameraSpots)
            {
                if (GUILayout.Button("Switch Spot: " + spot.transform.name))
                {
                    script.SwitchCameraSpots(spot);
                }
            }
        }
    }

#endif
}