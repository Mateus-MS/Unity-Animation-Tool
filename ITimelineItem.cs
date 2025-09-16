using System;

public interface ITimelineItem
{
    TimelineMode Mode { get; set; }
    float TriggerPercentage { get; set; }
    float Start { get; set; }
    float End { get; set; }
    float NormalizedStart { get; set; }
    float NormalizedEnd { get; set; }
    public float Duration { get; set; }
    Func<float, float> Easing { get; set; }
}