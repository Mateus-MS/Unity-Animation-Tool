using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

public class Animation : ITimelineAnimation
{
    public float start { get; set; }
    public float normalizedStart { get; set; }
    public float normalizedEnd { get; set; }
    public float end { get; set; }
    public float duration { get; set; }

    public Action<float> render { get; set; }
    public Timeline.PlayMode playMode { get; set; }
    public float triggerPercentage { get; set; }

    // Animation related
    public Action<float, float> ease { get; set; }

    public Animation(Action<float> render, float duration = 2f, Timeline.PlayMode playMode = Timeline.PlayMode.Instant, float triggerPercentage = .5f)
    {
        this.render = render;
        this.duration = duration;

        this.playMode = playMode;
        this.triggerPercentage = triggerPercentage;
    }

    public void Play()
    {
        Scene.Instance.timeline.Register(this);
    }

}