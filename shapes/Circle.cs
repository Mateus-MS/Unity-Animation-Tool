using UnityEngine;
using System;
using Shapes;

public class Circle
{
    public Vector3 position;
    public float radius;
    public Color fillColor;

    public Animator Animate { get; }

    public Circle(Vector3 position, float radius, Color fillColor)
    {
        this.position = position;
        this.radius = radius;
        this.fillColor = fillColor;

        Animate = new Animator(this);

        Timeline.Register(
            new Element(
                (t)=>
                {
                    Draw.Disc(
                        this.position,
                        this.radius,
                        this.fillColor
                    );
                }
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

        public Animation Focus(
            float duration = 2f,
            TimelineMode timelineMode = TimelineMode.Instant,
            float triggerPercentage = .15f,
            Func<float, float> easing = null,

            float finalRadius = 2f,
            float initialThickness = .5f,
            float finalThickness = 0f,
            Color color = default
        )
        {
            if (easing == null) easing = Easing.Linear;
            if (color == default) color = Color.red;

            return new Animation(
                (t) =>
                {
                    Draw.Ring(
                        this.circle.position,
                        Mathf.Lerp(this.circle.radius, finalRadius, t),
                        Mathf.Lerp(initialThickness, finalThickness, t),
                        color
                    );
                },
                duration: duration,
                timelineMode: timelineMode,
                visibillityMode: VisibillityMode.AppearOnPlay,
                triggerPercentage: triggerPercentage,
                easing: easing
            );
        }

    }

}