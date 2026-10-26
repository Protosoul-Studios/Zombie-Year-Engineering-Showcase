# Engineering decisions and trade-offs

[Portfolio](../README.md) · [Architecture](architecture.md)

These are decisions identifiable in the inspected code or the project's established design records, not claims of measured improvements.

| Decision | Motivation | Trade-off |
| --- | --- | --- |
| Start with a Harbinger-first prototype | Exercise a defining gameplay loop before expansion | Full game and multiplayer features remain deferred |
| Eyes-forward range/FOV plus raycast occlusion | Make sensing readable and non-omniscient | Layers, query capacity and masks need runtime tests |
| Store an explicit last-observed location | Prevent hidden-target tracking during search | Limited memory; no probabilistic prediction |
| Use semantic, expiring sound snapshots | Decouple gameplay evidence from sound playback | No network event replication demonstrated |
| Decay auditory focus; require stronger competing evidence | Resist repeated destination thrashing | Tuning can be context dependent |
| Retain vision priority during recent pursuit | Avoid unrelated sound disrupting a valid chase | Requires scenario balancing |
| Compose footsteps from stance, movement and surface | Data-driven stealth impact | Animation-perfect foot contact remains unproven |
| Use a distinct camera orbit pivot | Keep view ownership separate from movement | Existing input remains desktop prototype |
| Defer networking provider choice | Avoid committing to unsupported architecture | Multiplayer correctness is not yet verified |

Alternatives such as behavior trees, larger event memories, fully animation-driven footsteps and network-authored actor control may become appropriate; their absence is not evidence they were benchmarked and rejected.
