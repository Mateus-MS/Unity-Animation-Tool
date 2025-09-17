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

    [HideInInspector] public List<ITimelineElement> elements = new List<ITimelineElement>();

    public void Register(ITimelineElement element)
    {
        float prevStart = this.elements.Count > 0 ? this.elements[^1].start : 0f;
        float prevEnd = this.elements.Count > 0 ? this.elements[^1].end : 0f;
        float prevDuration = this.elements.Count > 0 ? this.elements[^1].duration : 0f;

        switch (element.playMode)
        {
            case PlayMode.Instant:
                element.start = prevStart;
                break;
            case PlayMode.Queue:
                element.start = prevEnd;
                break;
            case PlayMode.Percentage:
                element.start = prevStart + prevDuration * element.triggerPercentage;
                break;
        }
        element.end = element.start + element.duration;

        this.elements.Add(element);

        this.CalculateQueueEndTime();
        this.CalculateElementNormalizedEnds();
    }

    private void CalculateElementNormalizedEnds()
    {
        foreach (ITimelineElement e in this.elements)
        {
            e.normalizedStart = e.start / this.queueEndTime;
            e.normalizedEnd = e.end / this.queueEndTime;
        }
    }
    private void CalculateQueueEndTime()
    {
        this.queueEndTime = 0f;
        foreach (ITimelineElement e in this.elements)
        {
            switch (e.playMode)
            {
                case PlayMode.Instant:
                    float lastDuration = this.elements.Count > 0 ? this.elements[^1].duration : 0f;
                    float diff = e.duration - lastDuration;
                    if (diff > 0)
                    {
                        this.queueEndTime += diff;
                    }
                    else
                    {
                        this.queueEndTime += e.duration;
                    }
                    break;
                case PlayMode.Queue:
                    this.queueEndTime += e.duration;
                    break;
                case PlayMode.Percentage:
                    this.queueEndTime += e.duration * e.triggerPercentage;
                    break;
            }
        }
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
            foreach (ITimelineElement element in this.elements)
            {
                float t = Mathf.InverseLerp(element.normalizedStart, element.normalizedEnd, this.progress);
                t = Mathf.Clamp01(t);

                if (t == 0) return;

                element.render?.Invoke(t);
            }
        }
    }

    // When the play mode start, reset the animation to the start
    public void Awake()
    {
        this.progress = 0f;
    }

    public enum PlayMode
    {
        Instant,       // Will begin to render at the same time as the last element
        Queue,         // Will begin to render only when the last element finishes
        Percentage     // Will begin to render only when the last element reach a given percentage of it's animation
    }

    public enum VisibillityMode
    {
        AppearOnPlay,  // Will begin to render only when the animation begin and stay rendering
        HideOnFinish,  // Will be rendered always, till the animation finshes
        WhileAnimating // Will be renderer only while animating
    }

}