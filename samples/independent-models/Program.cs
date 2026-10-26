// © 2026 Protosoul Studios. Checks only the independent portfolio models.
using ZombieYear.Showcase.Models;
int count=0;
void Check(string label,bool ok){if(!ok)throw new Exception("FAILED: "+label);count++;Console.WriteLine($"PASS {count:00} {label}");}

void Throws<T>(string label, Action action) where T : Exception
{
    try { action(); }
    catch (Exception e) when (e is T) { Check(label, true); return; }
    throw new Exception("Expected " + typeof(T).Name + ": " + label);
}
var e=new HuntEvidence(true,true,true,false,false);
var h=new HunterStatePolicy();
Check("hunter begins roaming",h.Mode==HuntMode.Roam);
Check("visual pursuit",h.Step(e)==HuntMode.Pursue);
Check("sight priority over sound",h.Step(new(false,true,true,true,false))==HuntMode.Pursue);
Check("lost sight enters search",h.Step(new(false,true,false,false,false))==HuntMode.Search);
Check("search remains active",h.Step(new(false,true,false,false,false))==HuntMode.Search);
Check("search expires",h.Step(new(false,true,false,false,true))==HuntMode.Roam);
Check("sound investigation",h.Step(new(false,false,false,true,false))==HuntMode.Investigate);
h.Reset();Check("hunt reset",h.Mode==HuntMode.Roam);
var a=new Point(0,0);var f=new Point(0,1);
Check("visible forward",VisionMemory.CanSee(a,f,new(0,5),10,90,true));
Check("blocked sight",!VisionMemory.CanSee(a,f,new(0,5),10,90,false));
Check("outside range",!VisionMemory.CanSee(a,f,new(0,15),10,90,true));
Check("outside cone",!VisionMemory.CanSee(a,f,new(5,0),10,90,true));
var v=new VisionMemory();Check("no initial memory",v.LastKnown(1,2)==null);
v.Observe(new(1,2),3);v.LoseSight();
Check("frozen last position",!v.Visible&&v.LastKnown(4,2)==new Point(1,2));
Check("memory expiry",v.LastKnown(6,2)==null);
v.Clear();Check("memory clearing",v.LastKnown(3,2)==null);
var se=new SoundEvent(new(0,1),10,1,0,5);
Check("near sound audible",SoundAttention.Score(se,a,1)>0);
Check("distant sound inaudible",SoundAttention.Score(se,new(100,100),1)==0);
Check("expired sound inaudible",SoundAttention.Score(se,a,6)==0);
Check("fresh louder than old",SoundAttention.Score(se,a,1)>SoundAttention.Score(se,a,4));
var attention=new SoundAttention();
Check("first acoustic attention",attention.Consider(a,.5,1));
Check("weak competing sound ignored",!attention.Consider(new(8,8),.3,2));
Check("strong sound redirects",attention.Consider(new(8,8),.9,3));
Check("attention decays",attention.Current(4)<attention.Current(3));
attention.Reset();Check("attention clearing",attention.Destination==null);
var factors=new SurfaceFactors(1,1);
var steps=new FootstepComposer();
Check("no stationary footstep",steps.Advance(0,Pace.Walk,Stance.Stand,factors).Count==0);
Check("accumulate travel",steps.Advance(1,Pace.Walk,Stance.Stand,factors).Count==0);
Check("emit after stride",steps.Advance(.7,Pace.Walk,Stance.Stand,factors).Count==1);
Check("multiple long-distance steps",new FootstepComposer().Advance(5,Pace.Walk,Stance.Stand,factors).Count==3);
var sprint=new FootstepComposer().Advance(2,Pace.Sprint,Stance.Stand,factors)[0];
var crouch=new FootstepComposer().Advance(2,Pace.Sprint,Stance.Crouch,factors)[0];
Check("crouch quieter than sprint",crouch.Radius<sprint.Radius&&crouch.Intensity<sprint.Intensity);
var player=new SurvivorControlPolicy();
Check("initial standing",!player.Crouching);
Check("begin crouch",player.Toggle(false));
Check("obstruction blocks stand",player.Toggle(false));
Check("clearance permits stand",!player.Toggle(true));
Check("sprint selects speed",player.Speed(3,6,1.5,true)==6);
player.Toggle(true);Check("crouch blocks sprint speed",player.Speed(3,6,1.5,true)==1.5);
Check("forward relative movement",SurvivorControlPolicy.Move(0,1,0)==new PlanarMove(0,1));
Check("diagonal input normalized",SurvivorControlPolicy.Move(1,1,0).X<1);
Check("pitch limit",SurvivorControlPolicy.Pitch(95,-35,70)==70);

Throws<ArgumentOutOfRangeException>("invalid view range rejected",
    () => VisionMemory.CanSee(a,f,new(0,5),0,90,true));
Throws<ArgumentOutOfRangeException>("nonfinite view point rejected",
    () => VisionMemory.CanSee(a,f,new(double.NaN,1),10,90,true));
Throws<ArgumentOutOfRangeException>("backdated observation rejected",
    () => { var m=new VisionMemory();m.Observe(a,10);m.Observe(a,9); });
Throws<ArgumentOutOfRangeException>("invalid memory lifetime rejected",
    () => v.LastKnown(10,-1));
Throws<ArgumentOutOfRangeException>("invalid sound radius rejected",
    () => SoundAttention.Score(new SoundEvent(a,0,1,0,5),a,1));
Throws<ArgumentOutOfRangeException>("nonfinite sound intensity rejected",
    () => SoundAttention.Score(new SoundEvent(a,10,double.NaN,0,5),a,1));
Check("future sound event does not count",
    SoundAttention.Score(new SoundEvent(a,10,1,5,2),a,3)==0);
Check("zero evidence never redirects",
    !attention.Consider(new(9,9),0,5)&&attention.Destination==null);
Throws<ArgumentOutOfRangeException>("backdated attention rejected",
    () => { var focus=new SoundAttention();focus.Consider(a,.5,10);focus.Consider(new(5,5),.9,9); });
Throws<ArgumentOutOfRangeException>("negative travel rejected",
    () => new FootstepComposer().Advance(-1,Pace.Walk,Stance.Stand,factors));
Throws<ArgumentOutOfRangeException>("invalid surface rejected",
    () => new FootstepComposer().Advance(2,Pace.Walk,Stance.Stand,new(-1,1)));
Throws<ArgumentOutOfRangeException>("nonfinite movement speed rejected",
    () => player.Speed(double.NaN,6,1.5,false));
Throws<ArgumentOutOfRangeException>("invalid pitch range rejected",
    () => SurvivorControlPolicy.Pitch(0,90,-90));

Console.WriteLine($"ALL {count} INDEPENDENT MODEL CHECKS PASSED");
