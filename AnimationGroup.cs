using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AnimationGroup : ITimelineItem
{
    public TimelineMode Mode { get; set; }
    public float TriggerPercentage { get; set; }
    public float Start { get; set; } = 0f;
    public float Duration { get; set; }
    public float End { get; set; } = 0f;
    public float NormalizedStart { get; set; } = 0f;
    public float NormalizedEnd { get; set; } = 0f;
    public Func<float, float> Easing { get; set; }

    public List<Animation> animations = new List<Animation>();

    public AnimationGroup(TimelineMode mode = TimelineMode.Instant, float triggerPercentage = .1f, Func<float, float> easing = null)
    {
        this.Mode = mode;
        this.TriggerPercentage = triggerPercentage;
        this.Easing = easing ?? (x => x);
    }

    public void Register(Animation animation)
    {
        // Prevent the first animation to be a "OnPercentage"
        if (this.animations.Count == 0)
        {
            animation.Mode = TimelineMode.Instant;
        }


        switch (animation.Mode)
        {
            case TimelineMode.Instant:
                if (animation.Duration > this.Duration)
                    this.Duration = animation.Duration;

                animation.Start = 0f;
                animation.End = animation.Start + animation.Duration;
                break;

            case TimelineMode.OnQueue:
                this.Duration += animation.Duration;

                float lastEnd = animations.Count > 0 ? animations[animations.Count - 1].End : 0f;
                animation.Start = lastEnd;
                animation.End = animation.Start + animation.Duration;
                break;

            case TimelineMode.OnPercentage:
                Animation previous = animations[animations.Count - 1];

                this.Duration += previous.Duration * animation.TriggerPercentage;

                animation.Start = previous.Start + previous.Duration * animation.TriggerPercentage;
                animation.End = animation.Start + animation.Duration;
                break;
        }

        this.animations.Add(animation);

        foreach (var a in animations)
        {
            a.NormalizedStart = a.Start / this.Duration;
            a.NormalizedEnd = a.End / this.Duration;
        }
    }

    public void Update(float timelineProgress)
    {
        // Apply group easing to the overall timeline progress first
        float easedTimelineProgress = this.Easing(Mathf.Clamp01(timelineProgress));

        foreach (var anim in animations)
        {
            // Calculate the per-animation progress using the eased timeline progress
            float t = Mathf.InverseLerp(anim.NormalizedStart, anim.NormalizedEnd, easedTimelineProgress);
            t = Mathf.Clamp01(t);

            // Call the animation update directly with its own easing (optional)
            float finalT = anim.Easing != null ? anim.Easing(t) : t;

            anim.onUpdate?.Invoke(finalT);
        }
    }

}