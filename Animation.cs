using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

public class Animation : ITimelineItem
{
    public Action<float> onUpdate;
    public Func<float, float> Easing { get; set; }

    public TimelineMode Mode { get; set; }
    public float TriggerPercentage { get; set; }
    public float Start { get; set; } = 0f;
    public float Duration { get; set; }
    public float End { get; set; } = 0f;
    public float NormalizedStart { get; set; } = 0f;
    public float NormalizedEnd { get; set; } = 0f;

    public Animation(
        Action<float> onUpdate,
        float duration,
        TimelineMode mode = TimelineMode.Instant,
        float triggerPercentage = .1f,
        Func<float, float> easing = null)
    {
        this.onUpdate = onUpdate;
        Duration = duration;
        Mode = mode;
        this.TriggerPercentage = triggerPercentage;
        this.Easing = easing ?? (x => x);
    }

    public float GetProgress(float timelineProgress)
    {
        float t = Mathf.InverseLerp(Start, End, timelineProgress);
        return Mathf.Clamp01(Easing(t));
    }

    public static Action<float> RingFocus(Vector3 position, Color color, float initialRadius = 2f, float finalRadius = 3f, float initialThickness = 1f, float finalThickness = 0f)
    {
        return (t) =>
        {
            Draw.Ring(
                position,
                Mathf.Lerp(initialRadius, finalRadius, t),
                Mathf.Lerp(initialThickness, finalThickness, t),
                color
            );
        };
    }

}
