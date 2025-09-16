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

    private static Timeline instance;
    public static Timeline Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Timeline>();

                // Auto-create if none exists
                if (instance == null)
                {
                    GameObject go = new GameObject("Timeline");
                    instance = go.AddComponent<Timeline>();
                }
            }
            return instance;
        }
    }

    public static void Register(ITimelineItem anim)
    {
        Timeline timeline = Instance;

        // Prevent the first animation to be a "OnPercentage"
        if (timeline.animations.Count == 0)
        {
            anim.Mode = TimelineMode.Instant;
        }

        switch (anim.Mode)
        {
            case TimelineMode.Instant:
                // Timeline duration calculation
                if (anim.Duration > timeline.queueEndTime)
                {
                    timeline.queueEndTime = anim.Duration;
                }

                anim.Start = 0f;
                anim.End = anim.Duration;
                break;

            case TimelineMode.OnQueue:
                // Timeline duration calculation
                timeline.queueEndTime += anim.Duration;

                // Animation start/end calculation
                float lastAnimationEnd = timeline.animations.Count > 0 ? timeline.animations[timeline.animations.Count - 1].End : 0f;

                anim.Start = lastAnimationEnd;
                anim.End = anim.Start + anim.Duration;
                break;

            case TimelineMode.OnPercentage:
                ITimelineItem previous = timeline.animations[timeline.animations.Count - 1];

                // Timeline duration calculation
                timeline.queueEndTime += previous.Duration * anim.TriggerPercentage;

                // Animation start/end calculation
                anim.Start = previous.Start + previous.Duration * anim.TriggerPercentage;
                anim.End = anim.Start + anim.Duration;
                break;
        }

        timeline.animations.Add(anim);

        foreach (var a in timeline.animations)
        {
            a.NormalizedStart = a.Start / timeline.queueEndTime;
            a.NormalizedEnd = a.End / timeline.queueEndTime;
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