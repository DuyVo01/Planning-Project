using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement parameters")]
    public float moveSpeed;
    public float speedAccel;
    public float speedDecel;
    public float rotationDuration;

    [Header("Jump parameters")]
    public float jumpForce;
}
