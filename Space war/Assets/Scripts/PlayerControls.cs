using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Input Variables")]
    [Tooltip("Controls for movement of the player")]
    [SerializeField] InputAction movement;
    [Tooltip("Controls for shooting of the player")]
    [SerializeField] InputAction shooting;
    [Tooltip("Array of player's laser particles to turn off/on")]
    
    [SerializeField] ParticleSystem laserLeft;
    [SerializeField] ParticleSystem laserRight;

    [Header("Angle Movement Settings")]
    [Tooltip("How far player can move on the x axis before they are blocked")]
    [SerializeField] float xRange = 16f;
    [Tooltip("How far player can move on the y axis before they are blocked")]
    [SerializeField] float yRange = 10f;
    [Tooltip("How fast player rotates on Pitch (x) angle based on position")]
    [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("How fast player rotates on Pitch (x) angle based on movement")]
    [SerializeField] float controlPitchFactor = -20f;
    [Tooltip("How fast player rotates on Yaw (y) angle based on position")]
    [SerializeField] float positionYawFactor = -2f;
    [Tooltip("How fast player rotates on Roll (z) angle based on movement")]
    [SerializeField] float controlRollFactor = -20f;

    [Header("Movement Settings")]
    [Tooltip("Players X position")]
    [SerializeField] float xThrow;
    [Tooltip("Players Y position")]
    [SerializeField] float yThrow;
    [Tooltip("How fast ship moves up and down based upon player input.")]
    [SerializeField] float controlSpeed = 30f;


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
        var emissionModuleLeft = laserLeft.GetComponent<ParticleSystem>().emission;
        var emissionModuleRight = laserRight.GetComponent<ParticleSystem>().emission;
        
        emissionModuleRight.enabled = isActive;
        emissionModuleLeft.enabled = isActive;
        /*
        foreach(GameObject laser in lasers){
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
        */
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
