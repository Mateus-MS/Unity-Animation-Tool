using System;

public interface ITimelineItem
{
    TimelineMode TimelineMode { get; set; }
    public VisibillityMode VisibillityMode { get; set; }
    float TriggerPercentage { get; set; }
    float Start { get; set; }
    float End { get; set; }
    float NormalizedStart { get; set; }
    float NormalizedEnd { get; set; }
    public float Duration { get; set; }
}