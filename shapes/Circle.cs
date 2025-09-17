using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

public class Circle
{
    public Vector3 position;
    public float duration;

    public Animator Animate { get; }

    public Circle(Vector3 position, float duration = 2f, Timeline.PlayMode playMode = Timeline.PlayMode.Instant, float triggerPercentage = 0f)
    {
        this.position = position;
        this.duration = duration;

        Scene.Instance.timeline.Register(
            new Element(
                (t) =>
                {
                    Draw.Disc(
                        this.position,
                        2f,
                        Color.red
                    );
                },
                duration: this.duration,
                playMode: playMode,
                triggerPercentage: triggerPercentage
            )
        );
    }

    // public class Animator
    // {
    //     private readonly Circle circle;
    //     public Animator(Circle circle)
    //     {
    //         this.circle = circle;
    //     }

    //     public void Focus()
    //     {
            
    //     }

    // }

}