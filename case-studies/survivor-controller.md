# Survivor — movement, crouch and independent camera

[Portfolio](../README.md) · [Independent model](../samples/independent-models/SurvivorControlPolicy.cs)

## Problem

A third-person testing character must move relative to camera facing while the camera remains freely orbitable. Crouching cannot allow standing into overhead obstructions.

## Source-inspected responsibilities

`SurvivorMovementController` uses the Unity Input System, camera-relative ground-plane directions, CharacterController movement, gravity and animator state. It interpolates crouch height and collider center and uses a standing capsule overlap check before allowing stand-up. `SurvivorCameraOrbitTarget` owns a separate follow pivot, mouse yaw/pitch, pitch limits and conditional delayed recentering for Cinemachine.

## Independent model

`SurvivorControlPolicy.cs` contains only normalized movement direction, yaw rotation, crouch state transitions and a pitch clamp. It does **not** reproduce input devices, physics, animations or Cinemachine.

## Trade-offs

The game implementation remains a keyboard/mouse testing prototype. Layer masks, animation continuity, overlap buffer capacity and camera feel require actual scene review; networking and device-wide behavior are not proven.

**Verification:** [Source-inspected claims ENG-10/11](../docs/coverage.md); [gameplay evidence pending](../media/videos/README.md).
