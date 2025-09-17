using System;

public interface ITimelineAnimation : ITimelineElement
{
    Action<float, float> ease { get; set; }    
}