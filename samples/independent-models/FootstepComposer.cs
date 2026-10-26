// © 2026 Protosoul Studios. Independent event-composition example.
namespace ZombieYear.Showcase.Models;
public enum Stance { Stand,Crouch }
public enum Pace { Walk,Sprint }
public readonly record struct SurfaceFactors(double Radius,double Intensity);
public readonly record struct Footstep(double Radius,double Intensity);
public sealed class FootstepComposer
{
 private double distance;
 public IReadOnlyList<Footstep> Advance(double travel,Pace pace,Stance stance,SurfaceFactors surface)
 {
  if(!double.IsFinite(travel)||travel<0||!double.IsFinite(surface.Radius)||surface.Radius<0||
   !double.IsFinite(surface.Intensity)||surface.Intensity<0)
   throw new ArgumentOutOfRangeException(nameof(travel));
  if(travel==0){distance=0;return Array.Empty<Footstep>();}
  bool crouch=stance==Stance.Crouch,sprint=!crouch&&pace==Pace.Sprint;
  double stride=sprint?1.9:1.65;
  double radius=12*(crouch ? 0.35 : sprint ? 1.65 : 1)*surface.Radius;
  double intensity=Math.Clamp(.5*(crouch ? 0.45 : sprint ? 1.35 : 1)*surface.Intensity,0,1);
  distance+=travel;
  var events=new List<Footstep>();
  while(distance>=stride)
  {
   distance-=stride;
   if(radius>0&&intensity>0) events.Add(new Footstep(radius,intensity));
  }
  return events;
 }
 public void Reset()=>distance=0;
}
