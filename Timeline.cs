using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Timeline : MonoBehaviour
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

    public void Update(Camera cam)
    {
        foreach (var anim in this.animations)
        {
            if (anim is Animation animation)
            {
                float t = Mathf.InverseLerp(animation.NormalizedStart, animation.NormalizedEnd, this.progress);
                t = Mathf.Clamp01(t);
                animation.onUpdateWithCam?.Invoke(animation.Easing(t), cam);
            }
            else if (anim is AnimationGroup group)
            {
                float t = Mathf.InverseLerp(group.NormalizedStart, group.NormalizedEnd, this.progress);
                t = Mathf.Clamp01(t);

                group.Update(t, cam);
            }
        }
    }

}