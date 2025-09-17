using System;

public class Element : ITimelineElement
{
    public float start { get; set; }
    public float end { get; set; }

    public float duration { get; set; }

    public float normalizedStart { get; set; }
    public float normalizedEnd { get; set; }

    public Action<float> render { get; set; }
    public Timeline.PlayMode playMode { get; set; }
    public float triggerPercentage { get; set; }

    public Element(Action<float> render, float duration, float triggerPercentage, Timeline.PlayMode playMode = Timeline.PlayMode.Instant)
    {
        this.render = render;
        this.duration = duration;

        this.triggerPercentage = triggerPercentage;
        this.playMode = playMode;
    }
}