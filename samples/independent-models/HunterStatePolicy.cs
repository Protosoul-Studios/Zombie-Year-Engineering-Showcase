// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent portfolio policy example — not a Unity production component.

namespace ZombieYear.Showcase.Models;

public enum HuntMode
{
    Roam,
    Investigate,
    Pursue,
    Search
}

public readonly record struct HuntEvidence(
    bool VisibleTarget,
    bool HasLastKnownPosition,
    bool RecentSighting,
    bool HasAuditoryEvent,
    bool SearchExpired);

public sealed class HunterStatePolicy
{
    public HuntMode Mode { get; private set; } = HuntMode.Roam;

    public HuntMode Step(HuntEvidence evidence)
    {
        // A current observation is more authoritative than remembered evidence.
        if (evidence.VisibleTarget)
            return Mode = HuntMode.Pursue;

        // Briefly preserve pursuit after contact is lost, using known information.
        if (Mode == HuntMode.Pursue &&
            evidence.RecentSighting &&
            evidence.HasLastKnownPosition)
            return Mode;

        // A historical sound suggests investigation, not live actor tracking.
        if (evidence.HasAuditoryEvent)
            return Mode = HuntMode.Investigate;

        if (Mode == HuntMode.Pursue && evidence.HasLastKnownPosition)
            return Mode = HuntMode.Search;

        if (Mode == HuntMode.Search &&
            evidence.HasLastKnownPosition &&
            !evidence.SearchExpired)
            return Mode;

        if (Mode == HuntMode.Investigate && !evidence.SearchExpired)
            return Mode;

        return Mode = HuntMode.Roam;
    }

    public void Reset() => Mode = HuntMode.Roam;
}
