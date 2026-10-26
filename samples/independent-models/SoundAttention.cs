// © 2026 Protosoul Studios. Standalone acoustic-evidence illustration.
namespace ZombieYear.Showcase.Models;
public readonly record struct SoundEvent(Point Position,double Radius,double Intensity,double At,double Lifetime);
public sealed class SoundAttention
{
 private Point? point; private double strength,acceptedAt;
 public Point? Destination=>point;
 public static double Score(SoundEvent e,Point listener,double now)
 {
  if(e.Radius<=0||e.Lifetime<=0||!double.IsFinite(now)) throw new ArgumentOutOfRangeException(nameof(e));
  double age=now-e.At,dx=e.Position.X-listener.X,dz=e.Position.Z-listener.Z;
  double distance=Math.Sqrt(dx*dx+dz*dz);
  if(age<0||age>e.Lifetime||distance>e.Radius) return 0;
  return Math.Clamp(e.Intensity*(1-.8*distance/e.Radius)*(1-age/e.Lifetime),0,1);
 }
 public double Current(double now)=>point is null?0:Math.Max(0,strength-.08*Math.Max(0,now-acceptedAt));
 public bool Consider(Point where,double score,double now)
 {
  if(!double.IsFinite(score)||score<0||score>1||!double.IsFinite(now))
   throw new ArgumentOutOfRangeException(nameof(score));
  if(score<=0) return false;
  if(point is { } old)
  {
   double dx=old.X-where.X,dz=old.Z-where.Z;
   if(dx*dx+dz*dz<1.25*1.25) {if(score>Current(now)){strength=score;acceptedAt=now;}return false;}
   if(score<Current(now)+.12) return false;
  }
  point=where; strength=score;acceptedAt=now;return true;
 }
 public void Reset(){point=null;strength=0;acceptedAt=0;}
}
