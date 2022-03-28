using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed2Float : MonoBehaviour {
    public LibPdInstance pdPatch;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        float speed = Mathf.Round(controller.velocity.magnitude * 1000f) / 1000f;

        speed /= 5.335f;
        speed = (speed / 5.0f) + 0.76f;

		pdPatch.SendFloat("speed", speed);
    }
}