# Harbinger — semantic hearing and attention

[Portfolio](../README.md) · [Independent model](../samples/independent-models/SoundAttention.cs)

## The engineering challenge

Sound should identify **where an event occurred**, not provide continuous tracking of the person who made it. Competing noises should not force the hunter to replan every frame.

## Source-inspected implementation

The prototype records semantic stimuli with event position, intensity, category, hearing radius, emission time and lifetime, separately from audio playback. Hearing scores candidates by listener distance, intensity and freshness. Attention decays over time; sufficiently stronger distant events can redirect investigation, while similar-position events may refresh evidence without changing the destination. The Harbinger brain arbitrates sound against recently confirmed visual pursuit.

## Independent model

`SoundAttention.cs` illustrates score decay, range checks and a minimum switching advantage. Its coefficients are sample values, not production tuning guarantees; it omits event subscriptions, physics, networking and game audio.

## Trade-offs

The current listener retains a strongest pending event rather than a comprehensive sound history. Balancing perceptual persistence against route thrashing requires PlayMode evaluation. No multiplayer event replication, hearing performance metric or "intelligent AI" benchmark is claimed.

**Verification:** [Source-inspected claims ENG-05–07](../docs/coverage.md); [gameplay video pending](../media/videos/README.md).
