# Pikmin Squad Prototype

A Pikmin inspired prototype where you grab your little units and fling them at enemies.

- Play: No public build
- Made: March to April 2025, solo prototype
- Team: [@mfclinton](https://github.com/mfclinton)
- Engine: Unity, C#

This is an export of a private repo with only the code I wrote. Art, the Unity project files, and third party plugins aren't included. The history is squashed into one commit.

## What I built

- I built it on an ECS-style setup of my own on top of regular Unity components. Components only hold data and register themselves when they turn on, and each system keeps a live list of the ones it cares about. A system that starts late still hears about everything that already exists.
- Damage and knockback get queued and then applied once by their own systems, so several hits landing together just add up.
- You drag units around and fling them with spring forces, the scroll wheel spins the one you're holding, and holding right click pulls it back.
- Units and enemies only target what they can see, and they keep chasing a target for a few seconds after losing sight of it. Paths come from the [A* Pathfinding Project](https://arongranberg.com/astar/), and I wrote how units follow them.
