using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Shapes;

public class Circle
{
    public Vector3 position;
    public float duration;
    public float radius;
    public Color color;

    public Animator Animate { get; }

    public Circle(Vector3 position, float duration = 2f, float radius = 1f, Color color = default, Timeline.PlayMode playMode = Timeline.PlayMode.Instant, float triggerPercentage = 0f)
    {
        this.position = position;
        this.duration = duration;
        this.radius = radius;
        this.color = color == default ? Color.red : color;

        this.Animate = new Animator(this);

        Scene.Instance.timeline.Register(
            new Element(
                (t) =>
                {
                    Draw.Disc(
                        this.position,
                        this.radius,
                        this.color
                    );
                },
                duration: this.duration,
                playMode: playMode,
                triggerPercentage: triggerPercentage
            )
        );
    }

    public class Animator
    {
        private readonly Circle circle;
        public Animator(Circle circle)
        {
            this.circle = circle;
        }

        public Animation Focus(Color color = default)
        {
            if (color == default) color = Color.black;

            return new Animation(
                render: (t) =>
                {
                    Draw.Ring(
                        this.circle.position,
                        Mathf.Lerp(this.circle.radius, this.circle.radius + 1f, t), // radius
                        Mathf.Lerp(.5f, 0f, t),  // line thickness
                        color
                    );
                },
                duration: 2f,
                playMode: Timeline.PlayMode.Percentage,
                triggerPercentage: .5f
            );
        }

    }

}