# Survivor — movement and surface-sensitive footstep evidence

[Portfolio](../README.md) · [Independent model](../samples/independent-models/FootstepComposer.cs)

## The engineering challenge

Walking, sprinting and crouching should produce distinct signals for autonomous enemies. Surface acoustics should be tunable without coupling terrain content to the survivor's movement authority.

## Source-inspected design

The survivor's movement sound emitter accumulates horizontal travel distance and emits semantic footsteps at configured step spacing. Movement class, crouch state and a downward surface query combine with authored sound profiles. The producer publishes an event-position snapshot for AI hearing. The movement controller owns stance and locomotion; the surface provides radius and intensity multipliers.

## Independent model

`FootstepComposer.cs` demonstrates distance-triggered events and independent stance/surface factors. It is deliberately separate from Unity animation events, raycasts, ScriptableObjects and actual audio playback. It should not be copied into the game as a replacement controller.

## Trade-offs

Distance cadence is straightforward but not frame-perfect animation contact. Double-emission when using animation events, teleports and rapid movement bursts require careful integration tests. Equipment acoustics and final balancing are not established.

**Verification:** [Source-inspected claims ENG-08/09](../docs/coverage.md); actual runtime [video pending](../media/videos/README.md).
