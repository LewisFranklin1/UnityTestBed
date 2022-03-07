using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(player);
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.Update();
    }
}
