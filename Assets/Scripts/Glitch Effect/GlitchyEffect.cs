﻿using System;
using UnityEngine;

/// <summary>
/// Attach to the Player.
/// Creates a scan line jitter effect over top the games normal rendering.
/// Takes in a value between 0-1 from GlitchValueGenerators through out the scene.
/// </summary>
public class GlitchyEffect : MonoBehaviour
{
    #region Public Properties

    // Scan line jitter
    
    [SerializeField, Range(0, 1)]
    float _scanLineJitter = 0;

    public bool FullGlitch;                     //When true, glitch is fully on

    public float ScanLineJitter
    {
        get { return _scanLineJitter; }
        set { _scanLineJitter = value; }
    }

    #endregion

    #region Private Properties

    [SerializeField] Shader _shader;

    private Material _material;

    #endregion

    #region MonoBehaviour Functions

    /// <summary>
    /// Called after each frame is rendered and ready to be displayed
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        //Acts as an on/off switch for 100% glitch effect
        if (FullGlitch)
        {
            _scanLineJitter = 1;
        }
#if UNITY_EDITOR
        Debug.Log("On Render " + _scanLineJitter);
#endif
        //Creates scan line jitter - DO NOT TOUCH
        var sl_thresh = Mathf.Clamp01(1.0f - _scanLineJitter * 1.2f);
        var sl_disp = /*0.002f*/ + Mathf.Pow(_scanLineJitter, 3) * 0.05f;
        _material.SetVector("_ScanLineJitter", new Vector2(sl_disp, sl_thresh));

        Graphics.Blit(source, destination, _material);

        _scanLineJitter = 0;
    }

#endregion
    private void Update()
    {
#if UNITY_EDITOR
        Debug.Log("scan line " + _scanLineJitter);
#endif
    }

    /// <summary>
    /// Subscribe the event to the event handler
    /// </summary>
    private void OnEnable()
    {
        GlitchValueGenerator.ValueAboveZero += OnValueAboveZero;
    }

    /// <summary>
    /// Unsubscribe the event from the event handler
    /// </summary>
    private void OnDisable()
    {
        GlitchValueGenerator.ValueAboveZero -= OnValueAboveZero;
    }

    /// <summary>
    /// Event handler sets the value of _scanLineJitter to the value passed by the event
    /// </summary>
    /// <param name="i"></param>
    private void OnValueAboveZero(float i)
    {
        _scanLineJitter = i;
    }
}