using System;

public interface ITimelineElement
{
    float start { get; set; }
    float normalizedStart { get; set; }
    float end { get; set; }
    float normalizedEnd { get; set; }

    public float duration { get; set; }

    Action<float> render { get; set; }
    public Timeline.PlayMode playMode { get; set; }
    public float triggerPercentage { get; set; }
}