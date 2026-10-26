# Verification matrix

[Portfolio](../README.md) · [Evidence claims](coverage.md) · [Video gallery](../media/videos/README.md)

| Scope | Method | Status | Valid interpretation |
| --- | --- | --- | --- |
| Harbinger and survivor prototype | Inspection of selected private first-party Unity scripts at a recorded revision | **Source inspected** | Relevant systems are implemented in source, not freshly run for this showcase |
| Earlier game foundation | Owner-recorded Unity 6000.2.14f1 EditMode/PlayMode smoke validation | **Historical, owner verified** | Foundation checkpoint only; not gameplay proof for new systems |
| Independent .NET examples | Executable assertions in `Program.cs` | **52/52 passed — [CI evidence](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/runs/40256768258)** | Passes apply only to portfolio-model behavior |
| Portfolio structure | Local file/path/link audit | **Passed (27 files) — [CI evidence](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/runs/40256768258)** | Basic working-tree checks; does not audit all refs or copyright |
| Unity runtime gameplay clips | Source-labeled demonstrations from actual testing scene | **Pending; public gallery prepared** | No runtime evidence claimed yet |
| Networking, devices, profiling | Representative end-to-end tests | **Not claimed** | No multi-client, performance or hardware conclusion |

The model suite and audit are run by [this repository's workflow](../.github/workflows/showcase-model-checks.yml). The [successful clean-repository workflow run](https://github.com/Protosoul-Studios/Zombie-Year-Engineering-Showcase/actions/runs/40256768258) validates only the independent code and tracked-file audit. Older private-archive runs are not used as evidence here.

Any later media should name its source revision, Unity version, scene, capture date, observed outcome and limits.
