// Copyright © 2026 Protosoul Studios. All rights reserved.
// Standalone footstep event composition; not a Unity animation component.

namespace ZombieYear.Showcase.Models;

public enum Stance { Stand, Crouch }
public enum Pace { Walk, Sprint }

public readonly record struct SurfaceFactors(double Radius, double Intensity);
public readonly record struct Footstep(double Radius, double Intensity);

public sealed class FootstepComposer
{
    private const int MaximumEventsPerUpdate = 4096;
    private const double WalkStride = 1.65;
    private const double SprintStride = 1.9;

    private double accumulatedDistance;

    public IReadOnlyList<Footstep> Advance(
        double travel,
        Pace pace,
        Stance stance,
        SurfaceFactors surface)
    {
        if (!double.IsFinite(travel) || travel < 0 ||
            !double.IsFinite(surface.Radius) || surface.Radius < 0 ||
            !double.IsFinite(surface.Intensity) || surface.Intensity < 0)
            throw new ArgumentOutOfRangeException(nameof(travel));

        if (travel == 0)
        {
            accumulatedDistance = 0;
            return Array.Empty<Footstep>();
        }

        bool crouching = stance == Stance.Crouch;
        bool sprinting = !crouching && pace == Pace.Sprint;
        double stride = sprinting ? SprintStride : WalkStride;

        // Bound a single update so it cannot allocate an unbounded event list.
        double eventCount = Math.Floor((accumulatedDistance + travel) / stride);
        if (!double.IsFinite(eventCount) || eventCount > MaximumEventsPerUpdate)
            throw new ArgumentOutOfRangeException(nameof(travel));

        double radiusScale = crouching ? .35 : sprinting ? 1.65 : 1;
        double intensityScale = crouching ? .45 : sprinting ? 1.35 : 1;
        double radius = 12 * radiusScale * surface.Radius;
        double intensity = Math.Clamp(.5 * intensityScale * surface.Intensity, 0, 1);

        accumulatedDistance += travel;
        var events = new List<Footstep>();

        while (accumulatedDistance >= stride)
        {
            accumulatedDistance -= stride;
            if (radius > 0 && intensity > 0)
                events.Add(new Footstep(radius, intensity));
        }

        return events;
    }

    public void Reset() => accumulatedDistance = 0;
}
