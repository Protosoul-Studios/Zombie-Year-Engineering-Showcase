// Copyright © 2026 Protosoul Studios. All rights reserved.
// Standalone acoustic evidence model, not Unity audio or physics.

namespace ZombieYear.Showcase.Models;

public readonly record struct SoundEvent(
    Point Position,
    double Radius,
    double Intensity,
    double At,
    double Lifetime);

public sealed class SoundAttention
{
    private const double DecayPerSecond = .08;
    private const double SwitchMargin = .12;
    private const double NearbyDistance = 1.25;

    private Point? destination;
    private double strength;
    private double acceptedAt;

    public Point? Destination => destination;

    public static double Score(SoundEvent sound, Point listener, double nowSeconds)
    {
        if (!sound.Position.IsFinite || !listener.IsFinite ||
            !double.IsFinite(sound.Radius) || sound.Radius <= 0 ||
            !double.IsFinite(sound.Intensity) || sound.Intensity < 0 || sound.Intensity > 1 ||
            !double.IsFinite(sound.At) || !double.IsFinite(sound.Lifetime) ||
            sound.Lifetime <= 0 || !double.IsFinite(nowSeconds))
            throw new ArgumentOutOfRangeException(nameof(sound));

        double age = nowSeconds - sound.At;
        double deltaX = sound.Position.X - listener.X;
        double deltaZ = sound.Position.Z - listener.Z;
        double distance = Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);

        if (age < 0 || age > sound.Lifetime || distance > sound.Radius)
            return 0;

        double distanceWeight = 1 - .8 * distance / sound.Radius;
        double freshness = 1 - age / sound.Lifetime;
        return Math.Clamp(sound.Intensity * distanceWeight * freshness, 0, 1);
    }

    public double Current(double nowSeconds)
    {
        if (!double.IsFinite(nowSeconds) ||
            (destination is not null && nowSeconds < acceptedAt))
            throw new ArgumentOutOfRangeException(nameof(nowSeconds));

        return destination is null
            ? 0
            : Math.Max(0, strength - DecayPerSecond * (nowSeconds - acceptedAt));
    }

    public bool Consider(Point where, double score, double nowSeconds)
    {
        if (!where.IsFinite || !double.IsFinite(score) || score < 0 || score > 1 ||
            !double.IsFinite(nowSeconds) ||
            (destination is not null && nowSeconds < acceptedAt))
            throw new ArgumentOutOfRangeException(nameof(score));

        if (score == 0)
            return false;

        if (destination is { } previous)
        {
            double deltaX = previous.X - where.X;
            double deltaZ = previous.Z - where.Z;
            double separationSquared = deltaX * deltaX + deltaZ * deltaZ;
            double currentScore = Current(nowSeconds);

            if (separationSquared < NearbyDistance * NearbyDistance)
            {
                // Refresh a similar observation without moving the destination.
                if (score > currentScore)
                {
                    strength = score;
                    acceptedAt = nowSeconds;
                }
                return false;
            }

            if (score < currentScore + SwitchMargin)
                return false;
        }

        destination = where;
        strength = score;
        acceptedAt = nowSeconds;
        return true;
    }

    public void Reset()
    {
        destination = null;
        strength = 0;
        acceptedAt = 0;
    }
}
