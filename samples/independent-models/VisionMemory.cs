// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent 2D vision/observation model; Unity physics and occlusion are not simulated.
namespace ZombieYear.Showcase.Models;

public readonly record struct Point(double X, double Z)
{
    public bool IsFinite => double.IsFinite(X) && double.IsFinite(Z);
}

public sealed class VisionMemory
{
    private (Point Position, double At)? last;
    public bool Visible { get; private set; }

    public static bool CanSee(Point observer, Point forward, Point candidate,
        double maxRange, double fovDegrees, bool clear)
    {
        if (!observer.IsFinite || !forward.IsFinite || !candidate.IsFinite ||
            !double.IsFinite(maxRange) || maxRange <= 0 ||
            !double.IsFinite(fovDegrees) || fovDegrees <= 0 || fovDegrees >= 180)
            throw new ArgumentOutOfRangeException(nameof(maxRange));

        double dx = candidate.X - observer.X;
        double dz = candidate.Z - observer.Z;
        double d = Math.Sqrt(dx * dx + dz * dz);
        double f = Math.Sqrt(forward.X * forward.X + forward.Z * forward.Z);
        if (!clear || d == 0 || d > maxRange || f == 0) return false;
        double dot = (dx * forward.X + dz * forward.Z) / (d * f);
        return dot >= Math.Cos(fovDegrees * Math.PI / 360);
    }

    public void Observe(Point where, double at)
    {
        if (!where.IsFinite || !double.IsFinite(at) ||
            (last is { } old && at < old.At))
            throw new ArgumentOutOfRangeException(nameof(at));
        last = (where, at);
        Visible = true;
    }

    public void LoseSight() => Visible = false;

    public Point? LastKnown(double now, double lifetime)
    {
        if (!double.IsFinite(now) || !double.IsFinite(lifetime) || lifetime < 0)
            throw new ArgumentOutOfRangeException(nameof(lifetime));
        return last is { } x && now >= x.At && now - x.At <= lifetime
            ? x.Position : null;
    }

    public void Clear()
    {
        last = null;
        Visible = false;
    }
}
