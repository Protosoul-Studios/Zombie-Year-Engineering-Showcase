// Copyright © 2026 Protosoul Studios. All rights reserved.
// Independent portfolio policy example, not a Unity production script.
namespace ZombieYear.Showcase.Models;
public enum HuntMode { Roam, Investigate, Pursue, Search }
public readonly record struct HuntEvidence(bool VisibleTarget, bool HasLastKnownPosition,
 bool RecentSighting, bool HasAuditoryEvent, bool SearchExpired);
public sealed class HunterStatePolicy
{
 public HuntMode Mode { get; private set; } = HuntMode.Roam;
 public HuntMode Step(HuntEvidence e)
 {
  if(e.VisibleTarget) return Mode=HuntMode.Pursue;
  if(Mode==HuntMode.Pursue && e.RecentSighting && e.HasLastKnownPosition) return Mode;
  if(e.HasAuditoryEvent) return Mode=HuntMode.Investigate;
  if(Mode==HuntMode.Pursue && e.HasLastKnownPosition) return Mode=HuntMode.Search;
  if(Mode==HuntMode.Search && e.HasLastKnownPosition && !e.SearchExpired) return Mode;
  if(Mode==HuntMode.Investigate && !e.SearchExpired) return Mode;
  return Mode=HuntMode.Roam;
 }
 public void Reset()=>Mode=HuntMode.Roam;
}
