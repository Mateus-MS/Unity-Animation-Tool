using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

public class Element : ITimelineItem
{
    public TimelineMode TimelineMode { get; set; }
    public VisibillityMode VisibillityMode { get; set; }
    public float TriggerPercentage { get; set; }
    public float Start { get; set; }
    public float End { get; set; }
    public float NormalizedStart { get; set; }
    public float NormalizedEnd { get; set; }
    public float Duration { get; set; }
    public Action<float> Render;

    public Element(
        Action<float> render,
        
        TimelineMode timelineMode = TimelineMode.Instant,
        VisibillityMode visibillityMode = VisibillityMode.Always,

        float duration = 2f
    )
    {
        this.Render = render;

        this.TimelineMode = timelineMode;
        this.VisibillityMode = visibillityMode;

        this.Duration = duration;

    }

}