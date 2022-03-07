using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    public Animator switchAnimator;
    public Light dirLight;
    public SpriteRenderer notificationSprite;
    public uint updateLimit; //used to limit raycast calls to every 30 frames

    private bool nearPlayer = false;
    private byte frameCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        BaseEventManager.toggleLightSwitchEvent += toggleLightSwitch;
        switchAnimator.StopPlayback();
        updateLimit = 30;
    }
    private void Update()
    {
        if (frameCounter == updateLimit)
        {
            if (nearPlayer)
            {
                if (InputManager.RaycastCollider() != null)
                {
                    if (InputManager.RaycastCollider().name == this.GetComponentInChildren<BoxCollider>().name)
                    {
                        if (!notificationSprite.enabled)
                        {
                            notificationSprite.enabled = true;
                        }
                    }
                    Debug.Log(InputManager.RaycastCollider().name);
                }
                else
                {
                    notificationSprite.enabled = false;
                }
            }
            else if (!nearPlayer && notificationSprite.enabled)
            {
                notificationSprite.enabled = false;
            }
            Debug.Log(nearPlayer);
            frameCounter = 0;
        }
        frameCounter++;
    }

    private void toggleLightSwitch(Collider collider)
    {
        if (nearPlayer && notificationSprite.enabled)
        { 
            if (this.GetComponentInChildren<Collider>())
            {
                if (this.GetComponentInChildren<Collider>().name == collider.name)
                {
                    if (dirLight.enabled)
                    {
                        switchAnimator.SetTrigger("TurnOff");
                        dirLight.enabled = false;
                    }
                    else if (!dirLight.enabled)
                    {
                        //TODO: play animation in reverse
                        switchAnimator.SetTrigger("TurnOn");
                        dirLight.enabled = true;
                    }

                }
            }
            else
            {
                Debug.Log("No Collider was found in lightSwitch children");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "_Player")
        {
            nearPlayer = true;
        }
        Debug.Log(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "_Player")
        {
            nearPlayer = false;
        }
        Debug.Log(other.name);
    }
}
