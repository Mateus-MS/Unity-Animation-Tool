public class Element : ITimelineElement
{
    public float Start { get; set; }
    public float End { get; set; }

    public float Duration { get; set; }

    public float NormalizedStart { get; set; }
    public float NormalizedEnd { get; set; }

    public Element(float duration)
    {
        this.Duration = duration;
    }
}