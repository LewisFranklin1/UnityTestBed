using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float moveVelcoity;
    public static float mouseSensitivity;
    public static float upLimit;
    public static float downLimit;

    private static RaycastHit raycastHit;
    private static GameObject localPlayerGameObject;
    private static Camera localPlayerCamera;
    private static CharacterController localPlayerCharacterController;

    private static Transform cameraTransform;
    private static Vector3 movement;
    private static Vector2 rotation;

    private static Vector3 currentCameraRotation;

    // Start is called before the first frame update
    public static void Init(GameObject player)
    {
        localPlayerGameObject = player;
        localPlayerCamera = player.GetComponent<Camera>();
        localPlayerCharacterController = player.GetComponent<CharacterController>();
        cameraTransform = localPlayerCamera.transform;
        movement = Vector3.zero;
        mouseSensitivity = 50.0f;
        moveVelcoity = 1.0f;
        upLimit = -50.0f;
        downLimit = 50.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public static void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(FireRaycast())
            {
                BaseEventManager.toggleLightSwitch(raycastHit.collider);
            }
        }
        Rotate();
        Move();
    }

    private static void Rotate()
    {
        rotation.x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotation.y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentCameraRotation.x -= rotation.y;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, upLimit, downLimit);
        currentCameraRotation.y += rotation.x;

        cameraTransform.transform.localRotation = Quaternion.Euler(currentCameraRotation);
    }

    private static void Move()
    {

        movement = new Vector3(Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal"));
        movement *= moveVelcoity;
        localPlayerGameObject.GetComponent<Transform>().position +=  movement * Time.deltaTime;
    }

    private static bool FireRaycast()
    {
        return Physics.Raycast(localPlayerCamera.transform.position, localPlayerCamera.transform.forward, out raycastHit, Mathf.Infinity);
    }

    public static Collider RaycastCollider()
    {
        RaycastHit[] hits = Physics.RaycastAll(localPlayerCamera.transform.position, localPlayerCamera.transform.forward,10.0f);
        //if length is 1 there will be no position in array at 1 as array starts at 0 
        if(hits.Length > 1)
        {
            Debug.Log(hits.Length);
            return hits[1].collider;
        }
        else
        {
            return null;
        }
    }
}
