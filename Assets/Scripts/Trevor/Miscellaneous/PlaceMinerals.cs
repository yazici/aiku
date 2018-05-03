﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to place the minerals in the container. Activates necessary components for scene transition.
/// </summary>
public class PlaceMinerals : MonoBehaviour, IInteractable
{
    [Tooltip("The crystal object inside the container. This should be turned off at start.")]
    [SerializeField] private GameObject crystal;

    [Tooltip("The guiding arrow over the container.")]
    [SerializeField] private GameObject arrow;

    [Tooltip("The arrow that guides the player to the scene transition back to hub. This should be turned off at start.")]
	[SerializeField] private GameObject monitorArrow;

    [Tooltip("The box collider that allows the player to make the scene transition. This should be turned off at start.")]
    [SerializeField] private BoxCollider transitionCollider;

	public static event Action PlacedMinerals;

    /// <summary>
    /// When the player clicks on the container, activate the mineral object inside the container to 'place' it.
    /// Activate the necessary components to allow the player to change scenes.
    /// </summary>
    /// <param name="agent"></param>
    public void Interact(GameObject agent)
    {
        crystal.SetActive(true);
        arrow.SetActive(false);
		monitorArrow.SetActive (true);
        transitionCollider.enabled = true;
    }
}
