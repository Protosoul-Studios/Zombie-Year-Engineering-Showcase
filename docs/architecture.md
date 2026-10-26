# Architecture — Zombie Year technical vertical slice

[Portfolio](../README.md) · [Decisions](decisions.md) · [Evidence](coverage.md)

## Responsibilities reviewed in Unity source

| Component | Responsibility | Boundary |
| --- | --- | --- |
| `HarbingerBrain` | Roam, investigate, pursue and search decisions | Does not own path physics or raw sensing |
| `HarbingerNavigation` | NavMesh destinations, roam anchors, path fallback | Does not establish target legitimacy |
| `HarbingerVision` | Head-origin field-of-view and obstruction test | Does not retain knowledge |
| `HarbingerTargetMemory` | Last-seen prey position/time | Does not follow hidden prey |
| `SoundStimulusSystem` | Semantic acoustic event broadcast | Not audio playback or network replication |
| `HarbingerHearing` | Listener-distance and freshness evidence scoring | Not AI navigation |
| `HarbingerAuditoryAttention` | Accept, decay and redirect investigation focus | Not omniscient prey tracking |
| `SurvivorMovementController` | Camera-relative locomotion and safe stance | Not camera rotation |
| `SurvivorMovementSoundEmitter` | Discrete stance/surface-aware footstep evidence | Not locomotion speed authority |
| `SurvivorCameraOrbitTarget` | Independent orbit pivot and recenter | Not CharacterController physics |

## Information flow

```text
Survivor movement ─> distance / stance / surface ─> sound event snapshot
                                                   |
                                                   v
Vision ─> legitimate observation ─> target memory   hearing / attention
    \                  |                               /
     +-----------------+----------> Harbinger brain <---+
                                    |
                               navigation intent
                                    |
                                  NavMesh
```

The critical design distinction is **knowledge versus world truth**: vision can update memory; historical sound can suggest an investigation coordinate but does not reveal live actor location. Path execution is delegated to the navigation component.

The [independent models](../samples/independent-models/README.md) show simplified decisions using pure C#. They intentionally have no Unity, Cinemachine or NavMesh dependencies. Read [verification](verification-matrix.md) for what each kind of evidence supports.
