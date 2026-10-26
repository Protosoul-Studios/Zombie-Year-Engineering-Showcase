# Independent Zombie Year engineering models

Five **newly authored plain-C# examples** demonstrate concepts inspired by inspected game systems. These files are **not copied Unity production code**, do not link a private project and do not require proprietary assets.

| Example | Focus | Not evidence of |
| --- | --- | --- |
| [HunterStatePolicy.cs](HunterStatePolicy.cs) | Pursuit, recent sight, sound investigation and bounded search | NavMesh/Unity AI runtime behavior |
| [VisionMemory.cs](VisionMemory.cs) | FOV geometry and immutable last-known observations | Real scene occlusion accuracy |
| [SoundAttention.cs](SoundAttention.cs) | Event radius/freshness and switching inertia | Production audio and hearing balance |
| [FootstepComposer.cs](FootstepComposer.cs) | Discrete travel events and stance/surface acoustic factors | Animation-perfect foot contact |
| [SurvivorControlPolicy.cs](SurvivorControlPolicy.cs) | Camera-relative movement and stand-up clearance policy | CharacterController, Cinemachine or devices |

## Run on .NET 10

```bash
dotnet run --project samples/independent-models/IndependentModels.csproj --configuration Release
```

The self-contained [executable contract checks](Program.cs) are run by [GitHub Actions](../../.github/workflows/showcase-model-checks.yml) alongside the repository tree checker. **52/52 independent contract checks passed** in [GitHub Actions run #40255161464](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/runs/40255161464). The CI result verifies **these independent models only**. See [verification](../../docs/verification-matrix.md) and [review/reuse terms](../../docs/code-review-and-reuse.md).

**© 2026 Protosoul Studios. All rights reserved.**
