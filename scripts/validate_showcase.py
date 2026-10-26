#!/usr/bin/env python3
"""Local portfolio link and distribution audit; not a Git history, media, or Unity runtime audit."""
from pathlib import Path
import re
import sys
root=Path(__file__).resolve().parent.parent
files=[p for p in root.rglob("*") if p.is_file() and ".git" not in p.parts]
errors=[]
for p in files:
 r=p.relative_to(root)
 if any(x in r.parts for x in ("Assets","Packages","ProjectSettings","templates","source-samples","evidence")):
  errors.append("unwanted directory: "+str(r))
 if str(r) in ("AGENTS.md","PROJECT_STATE.md","docs/PUBLICATION_CHECKLIST.md","docs/USAGE_GUIDE.md"):
  errors.append("internal handoff: "+str(r))
 if p.suffix.lower() in (".prefab",".unity",".fbx",".asset",".dll",".exe",".apk",".pem",".p12",".pfx"):
  errors.append("unexpected binary: "+str(r))
 if p.suffix==".cs" and r.parts[:2]!=("samples","independent-models"):
  errors.append("unexpected C# path: "+str(r))
 if p.suffix==".md":
  for link in re.findall(r"!?\[[^\]]+\]\(([^)]+)\)",p.read_text(encoding="utf-8")):
   if link.startswith(("http:","https:","mailto:","#")):continue
   target=(p.parent/link.split("#")[0]).resolve()
   if not target.is_relative_to(root) or not target.is_file():
    errors.append("broken link: "+str(r)+" -> "+link)
if errors:
 print("SHOWCASE AUDIT FAILED\n"+"\n".join(errors));sys.exit(1)
print(f"SHOWCASE TREE AUDIT PASSED: {len(files)} files checked")
