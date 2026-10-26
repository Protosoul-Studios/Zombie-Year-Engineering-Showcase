#!/usr/bin/env python3
"""Guard the tracked public portfolio, not its history, licenses, or Unity runtime."""
from pathlib import Path
from urllib.parse import unquote
import re
import subprocess
import sys

ROOT = Path(__file__).resolve().parent.parent
tracked = subprocess.check_output(["git", "ls-files", "-z"], cwd=ROOT).decode("utf-8")
paths = [Path(p) for p in tracked.split("\0") if p]
names = {p.as_posix() for p in paths}
errors = []
blocked_dirs = {"Assets", "Packages", "ProjectSettings", "Library", "templates",
                "source-samples", "evidence", "bin", "obj"}
blocked_exts = {".unity", ".prefab", ".fbx", ".asset", ".dll", ".exe",
                ".apk", ".aab", ".unitypackage", ".pem", ".p12", ".pfx"}
internal = {"AGENTS.md", "PROJECT_STATE.md", "docs/PUBLICATION_CHECKLIST.md",
            "docs/USAGE_GUIDE.md"}
media_exts = {".mp4", ".webm", ".png", ".jpg", ".jpeg", ".webp"}
md_link = re.compile(r"!?\[[^\]]+\]\(([^)]+)\)")
token = re.compile(r"(?:gh[pousr]_[A-Za-z0-9]{20,}|github_pat_[A-Za-z0-9_]{20,}|AKIA[A-Z0-9]{16})")

for path in paths:
    relative = path.as_posix()
    suffix = path.suffix.lower()
    full = ROOT / path
    if any(part in blocked_dirs for part in path.parts):
        errors.append(f"disallowed project path: {relative}")
    if relative in internal:
        errors.append(f"internal planning file: {relative}")
    if suffix in blocked_exts or path.name in {"id_rsa", "id_ed25519"}:
        errors.append(f"prohibited binary or credential file: {relative}")
    if path.name == ".env" or path.name.startswith(".env."):
        errors.append(f"environment file: {relative}")
    if suffix in media_exts and path.parts[:2] != ("media", "videos"):
        errors.append(f"media outside approval gallery: {relative}")
    if suffix == ".cs" and path.parts[:2] != ("samples", "independent-models"):
        errors.append(f"nonportfolio C# path: {relative}")
    if not full.is_file():
        errors.append(f"missing tracked file: {relative}")
        continue
    if suffix not in {".md", ".cs", ".py", ".yml", ".yaml", ".gitattributes", ".gitignore"}:
        continue
    body = full.read_text(encoding="utf-8")
    if token.search(body):
        errors.append(f"credential-like token: {relative}")
    if suffix != ".md":
        continue
    if "github.com/Protosoul-Studios/Zombie-Year-Hunt-Or-Be-Hunted" in body:
        errors.append(f"private development URL: {relative}")
    if re.search(r"\[(SYSTEM|YOUR CONTRIBUTION|UNITY VERSION|PRECISE CLAIM)\]", body):
        errors.append(f"template placeholder: {relative}")
    for link in md_link.findall(body):
        target = link.split(" ", 1)[0].strip("<>")
        if not target or target.startswith(("#", "https:", "http:", "mailto:")):
            continue
        resolved = (full.parent / unquote(target.split("#", 1)[0].split("?", 1)[0])).resolve()
        if not resolved.is_relative_to(ROOT) or resolved.relative_to(ROOT).as_posix() not in names:
            errors.append(f"broken local link: {relative} -> {target}")

if errors:
    print("SHOWCASE TREE AUDIT FAILED")
    for error in errors:
        print(" - " + error)
    sys.exit(1)

print(f"SHOWCASE TREE AUDIT PASSED: {len(paths)} tracked files and local links checked")
print("Not a review of Git history, copyright, third-party media or Unity runtime.")
