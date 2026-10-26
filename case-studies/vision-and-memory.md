# Harbinger — directional vision and last-known memory

[Portfolio](../README.md) · [Independent model](../samples/independent-models/VisionMemory.cs)

## Problem

An enemy that tracks hidden actors through walls feels unfair. The hunter needs a clear boundary between *currently visible* and *previously observed*.

## Source-inspected design

The private Unity implementation has an optional head/eyes transform, a distance-and-FOV candidate query, a Physics raycast against obstruction layers, and a finite reusable overlap buffer. `HarbingerTargetMemory` stores the accepted prey's last-observed navigation position and timestamp. Searching after LOS loss uses the recorded observation rather than a newly sampled hidden position.

## Independent demonstration

`VisionMemory.cs` models a simple two-dimensional direction cone and immutable remembered coordinates. A caller supplies occlusion clearance. It does not execute Unity physics or claim source parity.

## Trade-offs

The non-alloc query has a finite candidate buffer; saturation, mask configuration and collider topology require test-scene verification. Memory expiry does not model uncertainty or prediction. No allocation benchmark is claimed.

**Verification:** [Source-inspected claim ENG-03/04](../docs/coverage.md). Actual wall-occlusion [footage is pending](../media/videos/README.md).
