# Zombie Year: Hunt Or Be Hunted

**Autonomous AI, gameplay perception and survivor stealth engineering — Protosoul Studios**

[![Independent C# model checks](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/workflows/showcase-model-checks.yml/badge.svg?branch=main)](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/workflows/showcase-model-checks.yml)

*CI validates standalone C# portfolio models and repository paths, **not** the Unity production game.*

**Zombie Year: Hunt Or Be Hunted** is an in-development survival/action project featuring human survivors and the **Harbinger**, an autonomous hunter. This portfolio explores a core engineering challenge: making a persistent threat respond to **legitimate visual and auditory evidence** instead of omniscient player tracking.

This is a **public-facing engineering showcase**, not a playable build, asset release or copy of the game repository. The five C# examples are **independently authored .NET models**, not extracted production scripts.

**For clients:** review subsystem boundaries, defensive behavior and runnable contract checks. **For collaborators:** follow the architecture and engineering trade-offs. **For players:** see the ideas behind the hunter, stealth and survivor controls without exposing the private game.

## Explore five engineering systems

| System | What it illustrates | Case study | Independent C# |
| --- | --- | --- | --- |
| **Harbinger behavior** | Roam, investigate, pursue, search and evidence priority | [Behavior](case-studies/harbinger-behavior.md) | [HunterStatePolicy](samples/independent-models/HunterStatePolicy.cs) |
| **Vision and memory** | Directional sight and frozen last-observed position | [Vision](case-studies/vision-and-memory.md) | [VisionMemory](samples/independent-models/VisionMemory.cs) |
| **Semantic hearing** | Historical sound events, age/distance weighting, attention switching | [Hearing](case-studies/semantic-hearing.md) | [SoundAttention](samples/independent-models/SoundAttention.cs) |
| **Survivor footsteps** | Discrete walk/sprint/crouch evidence with surface variation | [Footsteps](case-studies/survivor-footsteps.md) | [FootstepComposer](samples/independent-models/FootstepComposer.cs) |
| **Survivor control** | Camera-relative locomotion and obstruction-aware crouch decisions | [Controller](case-studies/survivor-controller.md) | [SurvivorControlPolicy](samples/independent-models/SurvivorControlPolicy.cs) |

## Run the examples

The models require **.NET 10**, with **no Unity or third-party packages**.

```bash
dotnet run --project samples/independent-models/IndependentModels.csproj --configuration Release
```

The [model guide](samples/independent-models/README.md) describes the examples. **All 52 independent C# checks passed** in [GitHub Actions run #40255161464](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/runs/40255161464). See the [verification matrix](docs/verification-matrix.md) before interpreting any result as production game evidence.

## Unity implementation context

The inspected game prototype uses **Unity 6000.2.14f1**, URP, Unity AI Navigation/NavMesh, Cinemachine and the Input System. The Harbinger-first vertical slice contains separately owned AI navigation, vision, target memory, semantic sound, hearing and auditory attention components. The survivor test controller contributes camera-relative movement, crouching, footsteps and an independent orbit target.

**Source inspected ≠ gameplay verified.** Combat/kill, infection conversion, networking, extraction and a complete match loop are *not* represented here as implemented and validated. There are no multiplayer scalability or performance benchmark claims.

## Videos will follow publication

This is intentionally a **code-first portfolio**. The [gameplay evidence gallery](media/videos/README.md) lists the clips to record and add **after the showcase becomes public**. None are presented as completed footage today.

## Documentation

[Case studies](case-studies/README.md) · [Architecture](docs/architecture.md) · [Decisions](docs/decisions.md) · [Claim coverage](docs/coverage.md) · [Verification](docs/verification-matrix.md) · [Testing](testing/README.md) · [Changelog](CHANGELOG.md)

## Viewing and reuse

**© 2026 Protosoul Studios and applicable rights holders. All rights reserved.** Published for portfolio review, **not as open-source software**. No general license is granted for reuse, adaptation, redistribution or incorporation into third-party products. See the [review and reuse policy](docs/code-review-and-reuse.md).

**Repository scope:** Curated standalone examples and engineering discussion only. No copied production game C#, packages, scenes, meshes, audio, private project history or developer handoff notes.
