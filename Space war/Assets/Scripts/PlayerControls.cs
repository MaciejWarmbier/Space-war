using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 7f;
    [SerializeField] float yRange = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
       movement.Enable();
    }

    private void OnDisable() {
    movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalThrow = Input.GetAxis("Horizontal");
        //float verticalThrow = Input.GetAxis("Vertical");

        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float xRawPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(xRawPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float yRawPos = transform.localPosition.y + yOffset;

        float campedYPos = Mathf.Clamp(yRawPos, -yRange, yRange);

        transform.localPosition = new Vector3 (clampedXPos, 
        campedYPos, 
        transform.localPosition.z);

        Debug.Log(xThrow);
        
    }
}
