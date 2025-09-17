public interface ITimelineElement
{
    float Start { get; set; }
    float End { get; set; }

    public float Duration { get; set; }

    float NormalizedStart { get; set; }
    float NormalizedEnd { get; set; }
}