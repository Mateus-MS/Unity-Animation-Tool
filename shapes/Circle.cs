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

        // In order to show this shape in the animation
        // It gonna create an static animation
        // It is really confuse and should be analyed any better solution
        Timeline.Register(
            new Animation(
                (t) =>
                {
                    Draw.Disc(
                        this.position,
                        this.radius,
                        this.fillColor
                    );
                },
            0f,
            timelineMode: TimelineMode.OnQueue,
            visibillityMode: VisibillityMode.Always
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

            float finalRadius = 3f,
            float initialThickness = 1f,
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
                triggerPercentage: triggerPercentage,
                easing: easing
            );
        }

    }

}