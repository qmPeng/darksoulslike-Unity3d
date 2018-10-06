using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public playerInput pi;
    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20.0f;
    public float camerDampValue = 0.5f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerx;
    private GameObject model;
    private GameObject camera;

    private Vector3 camerDampVelocity;

	// Use this for initialization
	void Awake () {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerx = 20;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.deltaTime);
        //tempEulerx = cameraHandle.transform.eulerAngles.x;
        tempEulerx -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerx = Mathf.Clamp(tempEulerx, -40, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerx, 0, 0);

        model.transform.eulerAngles = tempModelEuler;

        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.2f);

        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref camerDampVelocity, camerDampValue);
        camera.transform.eulerAngles = transform.eulerAngles;
	}
}
