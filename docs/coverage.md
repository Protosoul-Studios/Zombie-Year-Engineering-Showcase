# Claim and evidence coverage

[Portfolio](../README.md) · [Verification matrix](verification-matrix.md)

**Private Unity source revision reviewed:** `9b6e2688daec4300c21b589d7434ef3aaeca3b4e`. File/class names are provenance labels, **not private source links**. No production C# code is distributed.

| Claim | Reviewed implementation | Source-inspected components | Explanation |
| --- | --- | --- | --- |
| ENG-01 | Four-state Harbinger behavior and signal arbitration | `HarbingerBrain` | [Behavior](../case-studies/harbinger-behavior.md) |
| ENG-02 | NavMesh roaming, destination sampling, arrival/path handling | `HarbingerNavigation` | [Behavior](../case-studies/harbinger-behavior.md) |
| ENG-03 | Range, eyes-facing FOV and obstruction screening | `HarbingerVision` | [Vision](../case-studies/vision-and-memory.md) |
| ENG-04 | Frozen last-known target coordinates and timestamp | `HarbingerTargetMemory` | [Vision](../case-studies/vision-and-memory.md) |
| ENG-05 | Historical-position semantic sound events and lifetime | `SoundStimulus`, `SoundStimulusSystem` | [Hearing](../case-studies/semantic-hearing.md) |
| ENG-06 | Distance/freshness scoring and attention switching | `HarbingerHearing`, `HarbingerAuditoryAttention` | [Hearing](../case-studies/semantic-hearing.md) |
| ENG-07 | Recent visual pursuit is prioritized over competing audio | `HarbingerBrain` | [Hearing](../case-studies/semantic-hearing.md) |
| ENG-08 | Travel-distance-gated footstep emissions | `SurvivorMovementSoundEmitter` | [Footsteps](../case-studies/survivor-footsteps.md) |
| ENG-09 | Surface/stance movement sound multipliers | `SurfaceSoundProfile`, `SurvivorMovementSoundEmitter` | [Footsteps](../case-studies/survivor-footsteps.md) |
| ENG-10 | Camera-relative walking and obstruction-aware crouch | `SurvivorMovementController` | [Control](../case-studies/survivor-controller.md) |
| ENG-11 | Separate orbit pivot and optional recenter behavior | `SurvivorCameraOrbitTarget` | [Control](../case-studies/survivor-controller.md) |

**Every ENG claim above has status: Source inspected.** These observations do not establish that Unity runtime scenarios passed.

The independent C# models have separate [executable checks](../samples/independent-models/README.md), which validate simplified policy only. Video evidence for in-engine behavior is planned [after publication](../media/videos/README.md).
