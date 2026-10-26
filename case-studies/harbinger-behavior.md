# Harbinger — autonomous behavior arbitration

[Portfolio](../README.md) · [Independent model](../samples/independent-models/HunterStatePolicy.cs)

## Problem

An autonomous hunter must make purposeful decisions without knowing the current location of a survivor it cannot see.

## Source-inspected design

The Unity prototype's Harbinger brain coordinates **Roam → Investigate → Pursue → Search** decisions. Vision supplies legitimate target observations; target memory stores last-seen position/time; hearing and auditory attention provide historical sound evidence. A separate navigation component owns NavMesh sampling, roam anchors, arrival and path-failure handling. Recent visual confirmation normally takes priority over incidental sound.

Perception and pursuit replanning are interval-gated, while investigation and search have bounded windows. This is a source-level finding, not a new PlayMode result.

## Independent demonstration

`HunterStatePolicy.cs` is a small *portfolio policy*, not an extraction or replica of the production brain. It illustrates prioritizing current sight and transitioning to search using last-known facts.

## Trade-offs and limits

Explicit states are readable for a first vertical slice; they do not constitute a complete planner or combat AI. Timing constants, NavMesh behavior, transitions under stress, and client/server authority need real runtime validation. No kill, conversion, or multiplayer gameplay is claimed.

**Verification:** [Source-inspected claim ENG-01/02](../docs/coverage.md); independent model checks separate from Unity. [Video pending](../media/videos/README.md).
