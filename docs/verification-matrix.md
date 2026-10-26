# Verification matrix

[Portfolio](../README.md) · [Evidence claims](coverage.md) · [Video gallery](../media/videos/README.md)

| Scope | Method | Status | Valid interpretation |
| --- | --- | --- | --- |
| Harbinger and survivor prototype | Inspection of selected private first-party Unity scripts at a recorded revision | **Source inspected** | Relevant systems are implemented in source, not freshly run for this showcase |
| Earlier game foundation | Owner-recorded Unity 6000.2.14f1 EditMode/PlayMode smoke validation | **Historical, owner verified** | Foundation checkpoint only; not gameplay proof for new systems |
| Independent .NET examples | Executable assertions in `Program.cs` | **See this repository's GitHub Actions results** | Passes apply only to portfolio-model behavior |
| Portfolio structure | Local file/path/link audit | **See GitHub Actions results** | Basic working-tree checks; does not audit all refs or copyright |
| Unity runtime gameplay clips | Source-labeled demonstrations from actual testing scene | **Pending; intended after public release** | No runtime evidence claimed yet |
| Networking, devices, profiling | Representative end-to-end tests | **Not claimed** | No multi-client, performance or hardware conclusion |

The model suite and audit are run by [this repository's workflow](../.github/workflows/showcase-model-checks.yml). An older private working archive's results must **not** be cited as successful runs of this clean repository.

Any later media should name its source revision, Unity version, scene, capture date, observed outcome and limits.
