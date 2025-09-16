using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

[ExecuteAlways]
public class Timeline : ImmediateModeShapeDrawer
{
    [Range(0f, 1f)]
    public float progress = 0f;

    public List<ITimelineItem> animations = new List<ITimelineItem>();

    [HideInInspector]
    public float queueEndTime = 0f;

    public void Register(ITimelineItem anim)
    {
        // Prevent the first animation to be a "OnPercentage"
        if (this.animations.Count == 0)
        {
            anim.Mode = TimelineMode.Instant;
        }

        switch (anim.Mode)
        {
            case TimelineMode.Instant:
                // Timeline duration calculation
                if (anim.Duration > this.queueEndTime)
                {
                    this.queueEndTime = anim.Duration;
                }

                // Animation start/end calculation
                anim.Start = 0f;
                anim.End = anim.Start + anim.Duration;
                break;

            case TimelineMode.OnQueue:
                // Timeline duration calculation
                this.queueEndTime += anim.Duration;

                // Animation start/end calculation
                float lastAnimationEnd = animations.Count > 0 ? animations[animations.Count - 1].End : 0f;

                anim.Start = lastAnimationEnd;
                anim.End = anim.Start + anim.Duration;
                break;

            case TimelineMode.OnPercentage:
                ITimelineItem previous = this.animations[this.animations.Count - 1];

                // Timeline duration calculation
                this.queueEndTime += previous.Duration * anim.TriggerPercentage;

                // Animation start/end calculation
                anim.Start = previous.Start + previous.Duration * anim.TriggerPercentage;
                anim.End = anim.Start + anim.Duration;
                break;
        }

        this.animations.Add(anim);

        foreach (var a in animations)
        {
            a.NormalizedStart = a.Start / this.queueEndTime;
            a.NormalizedEnd = a.End / this.queueEndTime;
        }
    }
    public void Awake()
    {
        this.progress = 0f;
    }

    public override void DrawShapes(Camera cam)
    {
        if (Application.isPlaying)
        {
            this.progress += Time.deltaTime / this.queueEndTime;
        }

        using (Draw.Command(cam))
        {
            foreach (var anim in this.animations)
            {
                float t = Mathf.InverseLerp(anim.NormalizedStart, anim.NormalizedEnd, this.progress);
                t = Mathf.Clamp01(t);

                // TODO: For now this is to prevent all animations to render it's initial state since the beginning
                // But it can be a usefull feature to have
                if (t == 0) return;

                if (anim is Animation animation)
                {
                    animation.onUpdate?.Invoke(animation.Easing(t));
                }
                else if (anim is AnimationGroup group)
                {
                    group.Update(t);
                }
            }
        }
    }

}