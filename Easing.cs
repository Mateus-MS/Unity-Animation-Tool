using UnityEngine;
using System;

public static class Easing
{
    // Linear
    public static float Linear(float t) => t;

    // Quadratic
    public static float QuadIn(float t) => t * t;
    public static float QuadOut(float t) => 1 - (1 - t) * (1 - t);
    public static float QuadInOut(float t) => t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2f;

    // Cubic
    public static float CubicIn(float t) => t * t * t;
    public static float CubicOut(float t) => 1 - Mathf.Pow(1 - t, 3);
    public static float CubicInOut(float t) => t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2f;

    // Quartic
    public static float QuartIn(float t) => t * t * t * t;
    public static float QuartOut(float t) => 1 - Mathf.Pow(1 - t, 4);
    public static float QuartInOut(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2f;

    // Quintic
    public static float QuintIn(float t) => t * t * t * t * t;
    public static float QuintOut(float t) => 1 - Mathf.Pow(1 - t, 5);
    public static float QuintInOut(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2f;

    // Sine
    public static float SineIn(float t) => 1 - Mathf.Cos((t * Mathf.PI) / 2f);
    public static float SineOut(float t) => Mathf.Sin((t * Mathf.PI) / 2f);
    public static float SineInOut(float t) => -(Mathf.Cos(Mathf.PI * t) - 1) / 2f;

    // Exponential
    public static float ExpoIn(float t) => t == 0f ? 0f : Mathf.Pow(2, 10 * t - 10);
    public static float ExpoOut(float t) => t == 1f ? 1f : 1 - Mathf.Pow(2, -10 * t);
    public static float ExpoInOut(float t)
        => t == 0f ? 0f : t == 1f ? 1f : t < 0.5f ? Mathf.Pow(2, 20 * t - 10) / 2f : (2 - Mathf.Pow(2, -20 * t + 10)) / 2f;

    // Circular
    public static float CircIn(float t) => 1 - Mathf.Sqrt(1 - t * t);
    public static float CircOut(float t) => Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
    public static float CircInOut(float t)
        => t < 0.5f ? (1 - Mathf.Sqrt(1 - 4 * t * t)) / 2f : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2f;

    // Elastic
    public static float ElasticIn(float t)
    {
        const float c4 = (2f * Mathf.PI) / 3f;

        if (t == 0f) return 0f;
        if (t == 1f) return 1f;

        return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c4);
    }
    public static float ElasticOut(float t)
    {
        const float c4 = (2f * Mathf.PI) / 3f;

        if (t == 0f) return 0f;
        if (t == 1f) return 1f;

        return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
    }
    public static float ElasticInOut(float t)
    {
        const float c5 = (2f * Mathf.PI) / 4.5f;

        if (t == 0f) return 0f;
        if (t == 1f) return 1f;

        return t < 0.5f
            ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f
            : (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f + 1f;
    }

}
