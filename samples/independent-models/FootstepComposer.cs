// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent semantic footstep model, not a Unity animation or physics component.
namespace ZombieYear.Showcase.Models;

public enum Stance { Stand, Crouch }
public enum Pace { Walk, Sprint }
public readonly record struct SurfaceFactors(double Radius, double Intensity);
public readonly record struct Footstep(double Radius, double Intensity);

public sealed class FootstepComposer
{
    private const int MaximumEventsPerUpdate = 4096;
    private double distance;

    public IReadOnlyList<Footstep> Advance(
        double travel, Pace pace, Stance stance, SurfaceFactors surface)
    {
        if (!double.IsFinite(travel) || travel < 0 ||
            !double.IsFinite(surface.Radius) || surface.Radius < 0 ||
            !double.IsFinite(surface.Intensity) || surface.Intensity < 0)
            throw new ArgumentOutOfRangeException(nameof(travel));

        if (travel == 0)
        {
            distance = 0;
            return Array.Empty<Footstep>();
        }

        bool crouch = stance == Stance.Crouch;
        bool sprint = !crouch && pace == Pace.Sprint;
        double stride = sprint ? 1.9 : 1.65;

        // Reject unrealistic single updates instead of allocating arbitrarily many events.
        double count = Math.Floor((distance + travel) / stride);
        if (!double.IsFinite(count) || count > MaximumEventsPerUpdate)
            throw new ArgumentOutOfRangeException(nameof(travel));

        double radius = 12 * (crouch ? .35 : sprint ? 1.65 : 1) * surface.Radius;
        double intensity = Math.Clamp(
            .5 * (crouch ? .45 : sprint ? 1.35 : 1) * surface.Intensity, 0, 1);
        distance += travel;
        var events = new List<Footstep>();
        while (distance >= stride)
        {
            distance -= stride;
            if (radius > 0 && intensity > 0)
                events.Add(new Footstep(radius, intensity));
        }
        return events;
    }

    public void Reset() => distance = 0;
}
