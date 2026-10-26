// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent controls policy; no Unity CharacterController or Cinemachine code.

namespace ZombieYear.Showcase.Models;

public readonly record struct PlanarMove(double X, double Z);

public sealed class SurvivorControlPolicy
{
    public bool Crouching { get; private set; }

    public bool Toggle(bool hasStandingClearance)
    {
        if (!Crouching)
            Crouching = true;
        else if (hasStandingClearance)
            Crouching = false;

        return Crouching;
    }

    public double Speed(
        double walkSpeed,
        double sprintSpeed,
        double crouchSpeed,
        bool sprintHeld)
    {
        if (!double.IsFinite(walkSpeed) || walkSpeed < 0 ||
            !double.IsFinite(sprintSpeed) || sprintSpeed < 0 ||
            !double.IsFinite(crouchSpeed) || crouchSpeed < 0)
            throw new ArgumentOutOfRangeException(nameof(walkSpeed));

        return Crouching
            ? crouchSpeed
            : sprintHeld ? sprintSpeed : walkSpeed;
    }

    public static PlanarMove Move(
        double right,
        double forward,
        double cameraYawDegrees)
    {
        if (!double.IsFinite(right) || !double.IsFinite(forward) ||
            !double.IsFinite(cameraYawDegrees))
            throw new ArgumentOutOfRangeException(nameof(right));

        double magnitude = Math.Sqrt(right * right + forward * forward);
        if (magnitude > 1)
        {
            right /= magnitude;
            forward /= magnitude;
        }

        double yaw = cameraYawDegrees * Math.PI / 180;
        return new PlanarMove(
            Math.Cos(yaw) * right + Math.Sin(yaw) * forward,
            -Math.Sin(yaw) * right + Math.Cos(yaw) * forward);
    }

    public static double Pitch(double value, double minimum, double maximum)
    {
        if (!double.IsFinite(value) || !double.IsFinite(minimum) ||
            !double.IsFinite(maximum) || minimum > maximum)
            throw new ArgumentOutOfRangeException(nameof(value));

        return Math.Clamp(value, minimum, maximum);
    }
}
