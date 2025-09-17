using System;

public interface ITimelineAnimation : ITimelineItem
{
    Func<float, float> Easing { get; set; }
}