﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class exists to lock the player in the engine room during the generator-checking sequence,
/// to prevent them from getting distracted by the opening door when they come near.
/// The script is applied to a dedicated GameObject or the task on which it depends.
/// </summary>

public class LockEngineRoom : MonoBehaviour
{
    [SerializeField, Tooltip("After what event do you want the door to lock?")]
    private Task task;

    [SerializeField, Tooltip("Cargo Hold or Engine Room Door - the actual L_Door Object.")]
    private Door door1;

    [SerializeField, Tooltip("Cargo Hold or engine room door, whatever isn't above.")]
    private Door door2;

    private void OnEnable()
    {
        task.OnTaskCompleted += LockDoors;
    }
    private void OnDisable()
    {
        task.OnTaskCompleted -= LockDoors;
    }

    /// <summary>
    /// Currently, these doors will stay locked for the rest of the game.
    /// </summary>
    private void LockDoors()
    {
        door1.LockDoor();
        door2.LockDoor();
    }
}
