// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent movement and stance policy; no CharacterController or Cinemachine code.
namespace ZombieYear.Showcase.Models;

public readonly record struct PlanarMove(double X, double Z);

public sealed class SurvivorControlPolicy
{
    public bool Crouching { get; private set; }

    public bool Toggle(bool canStand)
    {
        if (!Crouching) Crouching = true;
        else if (canStand) Crouching = false;
        return Crouching;
    }

    public double Speed(double walk, double sprint, double crouch, bool sprintHeld)
    {
        if (!double.IsFinite(walk) || walk < 0 ||
            !double.IsFinite(sprint) || sprint < 0 ||
            !double.IsFinite(crouch) || crouch < 0)
            throw new ArgumentOutOfRangeException(nameof(walk));
        return Crouching ? crouch : sprintHeld ? sprint : walk;
    }

    public static PlanarMove Move(double right, double forward, double cameraYaw)
    {
        if (!double.IsFinite(right) || !double.IsFinite(forward) ||
            !double.IsFinite(cameraYaw))
            throw new ArgumentOutOfRangeException(nameof(right));
        double size = Math.Sqrt(right * right + forward * forward);
        if (size > 1) { right /= size; forward /= size; }
        double angle = cameraYaw * Math.PI / 180;
        return new PlanarMove(
            Math.Cos(angle) * right + Math.Sin(angle) * forward,
            -Math.Sin(angle) * right + Math.Cos(angle) * forward);
    }

    public static double Pitch(double value, double min, double max)
    {
        if (!double.IsFinite(value) || !double.IsFinite(min) ||
            !double.IsFinite(max) || min > max)
            throw new ArgumentOutOfRangeException(nameof(value));
        return Math.Clamp(value, min, max);
    }
}
