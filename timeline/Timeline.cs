using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

[ExecuteAlways]
public class Timeline : ImmediateModeShapeDrawer
{
    [Range(0f, 1f)] public float progress;

    [HideInInspector] public float queueEndTime;

    public void Register()
    {
        Debug.Log("Registering a new item on timeline");
    }

    public override void DrawShapes(Camera cam)
    {
        // On play mode auto increment the progress
        if (Application.isPlaying)
        {
            this.progress += Time.deltaTime / this.queueEndTime;
        }

        using (Draw.Command(cam))
        {
            // Debug.Log(this.queueEndTime);
        }
    }

    // When the play mode start, reset the animation to the start
    public void Awake()
    {
        this.progress = 0f;
    }
}