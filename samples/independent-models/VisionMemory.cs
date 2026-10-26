// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent 2D vision and observation model; no Unity physics are executed.

namespace ZombieYear.Showcase.Models;

public readonly record struct Point(double X, double Z)
{
    public bool IsFinite => double.IsFinite(X) && double.IsFinite(Z);
}

public sealed class VisionMemory
{
    private (Point Position, double At)? last;

    public bool Visible { get; private set; }

    public static bool CanSee(
        Point observer,
        Point forward,
        Point candidate,
        double maxRange,
        double fovDegrees,
        bool clear)
    {
        if (!observer.IsFinite || !forward.IsFinite || !candidate.IsFinite ||
            !double.IsFinite(maxRange) || maxRange <= 0 ||
            !double.IsFinite(fovDegrees) || fovDegrees <= 0 || fovDegrees >= 180)
            throw new ArgumentOutOfRangeException(nameof(maxRange));

        double deltaX = candidate.X - observer.X;
        double deltaZ = candidate.Z - observer.Z;
        double distance = Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
        double facingMagnitude = Math.Sqrt(forward.X * forward.X + forward.Z * forward.Z);

        if (!clear || distance == 0 || distance > maxRange || facingMagnitude == 0)
            return false;

        double dot = (deltaX * forward.X + deltaZ * forward.Z) /
                     (distance * facingMagnitude);

        return dot >= Math.Cos(fovDegrees * Math.PI / 360);
    }

    public void Observe(Point position, double atSeconds)
    {
        if (!position.IsFinite || !double.IsFinite(atSeconds) ||
            (last is { } previous && atSeconds < previous.At))
            throw new ArgumentOutOfRangeException(nameof(atSeconds));

        last = (position, atSeconds);
        Visible = true;
    }

    // A failed line-of-sight check never rewrites the previous observation.
    public void LoseSight() => Visible = false;

    public Point? LastKnown(double nowSeconds, double lifetimeSeconds)
    {
        if (!double.IsFinite(nowSeconds) ||
            !double.IsFinite(lifetimeSeconds) ||
            lifetimeSeconds < 0)
            throw new ArgumentOutOfRangeException(nameof(lifetimeSeconds));

        return last is { } observation &&
               nowSeconds >= observation.At &&
               nowSeconds - observation.At <= lifetimeSeconds
            ? observation.Position
            : null;
    }

    public void Clear()
    {
        last = null;
        Visible = false;
    }
}
