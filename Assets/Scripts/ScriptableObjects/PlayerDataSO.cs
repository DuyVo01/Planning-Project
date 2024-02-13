using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [HideInInspector] public float speedAccelAmount;
    [HideInInspector] public float speedDecelAmount;
    [Header("Movement parameters")]
    public float maxRunSpeed;
    public float speedAcceleration;
    public float speedDeceleration;

    [Header("Rotation parameters")]
    public float rotationDuration;

    [Header("Gravity")]
    [HideInInspector] public float gravityScale;
    private float gravityStrength;

    [Header("Jump parameters")]
    public float jumpTimeToApex;
    public float jumpHeight;
    public float jumpForce;

    private void OnValidate()
    {
        //Gravity and Jump calculations
        gravityStrength = (-2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics.gravity.y;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        //Accel calculations
        speedAccelAmount = (50 * speedAcceleration) / maxRunSpeed;
        speedDecelAmount = (50 * speedDeceleration) / maxRunSpeed;

        speedAcceleration = Mathf.Clamp(speedAcceleration, 0.01f, maxRunSpeed);
        speedDeceleration = Mathf.Clamp(speedDeceleration, 0.01f, maxRunSpeed);

    }
}
