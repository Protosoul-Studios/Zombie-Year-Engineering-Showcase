// © 2026 Protosoul Studios. Independent .NET example, not game source.
namespace ZombieYear.Showcase.Models;
public readonly record struct Point(double X,double Z);
public sealed class VisionMemory
{
 private (Point Position,double At)? last;
 public bool Visible {get;private set;}
 public static bool CanSee(Point observer,Point forward,Point candidate,double maxRange,double fovDegrees,bool clear)
 {
  if(maxRange<=0||fovDegrees<=0||fovDegrees>=180) throw new ArgumentOutOfRangeException(nameof(maxRange));
  double dx=candidate.X-observer.X,dz=candidate.Z-observer.Z;
  double d=Math.Sqrt(dx*dx+dz*dz),f=Math.Sqrt(forward.X*forward.X+forward.Z*forward.Z);
  if(!clear||d==0||d>maxRange||f==0) return false;
  double dot=(dx*forward.X+dz*forward.Z)/(d*f);
  return dot>=Math.Cos(fovDegrees*Math.PI/360);
 }
 public void Observe(Point where,double at)
 {
  if(!double.IsFinite(at)||(last is { } old&&at<old.At)) throw new ArgumentOutOfRangeException(nameof(at));
  last=(where,at); Visible=true;
 }
 public void LoseSight()=>Visible=false;
 public Point? LastKnown(double now,double lifetime)=>last is { } x&&now>=x.At&&now-x.At<=lifetime?x.Position:null;
 public void Clear(){last=null;Visible=false;}
}
