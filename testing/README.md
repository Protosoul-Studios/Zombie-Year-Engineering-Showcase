# Verification

This code-first showcase deliberately separates three kinds of evidence.

**Production source inspection.** Selected Harbinger and survivor scripts were read at a recorded private revision. This supports the implemented responsibilities discussed in the [coverage matrix](../docs/coverage.md), but is not a fresh Unity runtime test.

**Independent model checks.** [Program.cs](../samples/independent-models/Program.cs) contains executable checks of simplified .NET portfolio policies. The results of the [workflow](../.github/workflows/showcase-model-checks.yml) establish compilation and model contract behavior only.

**In-game demonstration.** Planned scenario videos will be captured and linked in the [public video gallery](../media/videos/README.md) when approved. Current gameplay evidence is **pending**; networking, device QA and performance benchmarks are not claimed.

A historical foundation test record for Unity 6000.2.14f1 includes owner-observed EditMode/PlayMode smoke checks. That historical checkpoint cannot verify the newer Harbinger gameplay systems. See the central [verification matrix](../docs/verification-matrix.md).
