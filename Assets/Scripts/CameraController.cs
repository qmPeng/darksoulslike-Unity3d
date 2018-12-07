using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public IUserInput pi;
    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20.0f;
    public float camerDampValue = 0.5f;
    public Image lockDot;
    public bool lockState;
    public bool isAI = false;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerx;
    private GameObject model;
    private GameObject camera;
    private Vector3 camerDampVelocity;
    private LockTarget lockTarget;

	// Use this for initialization
	void Start () {
        
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerx = 20;

        //ac = playerHandle.GetComponent<ActorController>();
        model = playerHandle.GetComponent<ActorController>().model;
        pi = playerHandle.GetComponent<ActorController>().pi;

        if (!isAI)
        {
            camera = Camera.main.gameObject;
            lockDot.enabled = false;
        }

        
        lockState = false;
    }

    void Update()
    {
        if(lockTarget != null)
        {
            if (!isAI)
            {
                lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            }
            //print(lockTarget.halfHeight);
            
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position)>10.0f)
            {
                lockProcessA(null, false, false, isAI);
            }

            if (lockTarget.am != null && lockTarget.am.sm.isDie)
            {
                lockProcessA(null, false, false, isAI);
            }
        }
    }

    private void lockProcessA(LockTarget _lockTarget, bool _lockDotEnable, bool _lockState, bool _isAI)
    {
        lockTarget = _lockTarget;
        if (!_isAI)
        {
            lockDot.enabled = _lockDotEnable;
        }
        lockState = _lockState;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.deltaTime);
            //tempEulerx = cameraHandle.transform.eulerAngles.x;
            tempEulerx -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            tempEulerx = Mathf.Clamp(tempEulerx, -40, 30);

            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerx, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform);
        }



        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.2f);
        if (!isAI)
        {
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref camerDampVelocity, camerDampValue);
            //camera.transform.eulerAngles = transform.eulerAngles;

            camera.transform.LookAt(cameraHandle.transform);
        }
       
	}

    public void LockUnlock()
    {
        //print("lockunlock");
        //if(lockTarget == null)
        //{
            //try to lock
            Vector3 modelOrigin1 = model.transform.position;
            Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
            Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask(isAI?"Player":"Enemy"));

            if (cols.Length == 0)
            {
                lockProcessA(null, false, false, isAI);
            }
            else
            {
                foreach (var col in cols)
                {
                    if(lockTarget != null && lockTarget.obj == col.gameObject)
                    {
                        lockProcessA(null, false, false, isAI);
                        break;
                    }
                       lockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                
                       break;
                }
            }


            
        //}
        //else
        //{
            //release lock
            //lockTarget = null;
        //}
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public ActorManger am;

        public LockTarget(GameObject _obj, float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = _obj.GetComponent<ActorManger>();
        }
    }

}
