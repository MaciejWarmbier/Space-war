using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction shooting;
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 9f;
    [SerializeField] float yRange = 8f;
    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float xThrow;
    [SerializeField] float yThrow;



    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnEnable() {
       movement.Enable();
       shooting.Enable();
    }

    private void OnDisable() {
    movement.Disable();
    shooting.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    private void ProcessFiring(){
        if(shooting.ReadValue<float>()>0.5){
            SetLasersActive(true);
        }else {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers){
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    private void ProcessTranslation()
    {
        //float horizontalThrow = Input.GetAxis("Horizontal");
        //float verticalThrow = Input.GetAxis("Vertical");

        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;


        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float xRawPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(xRawPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float yRawPos = transform.localPosition.y + yOffset;

        float campedYPos = Mathf.Clamp(yRawPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos,
        campedYPos,
        transform.localPosition.z);
    }

    private void ProcessRotation(){

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        

        float pitch = pitchDueToPosition  + pitchDueToControlThrow;
        float yaw = transform.localPosition.z * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);

    }
}
