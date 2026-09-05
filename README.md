# Salvage Protocol

**Salvage Protocol** is a wave-based FPS survival prototype built from the ground up in **Unity and C#**.

The project focuses on modular gameplay systems, enemy AI, physics-driven interactions, environmental combat, and clean component-based architecture.

> \*\*Status:\*\* Active Development — core gameplay is functional, with visual polish, audio, balancing, and environment art currently in progress.

## Gameplay

Survive increasingly difficult waves of enemies while using firearms, grenades, environmental explosives, and physics-based objects to control the arena.

The combat space is designed around movement, enemy pathing, choke points, environmental hazards, and multiple routes through a compact industrial salvage facility.

## Current Features

* First-person movement, sprinting, jumping, and camera controls
* Hitscan firearm system
* Magazine ammunition and reloading
* Reusable health and damage system
* NavMesh-based enemy navigation
* State-based enemy AI
* Melee enemy combat
* Wave spawning and progression
* Physics-based object carrying and throwing
* Explosive environmental objects
* Chain-reaction explosions
* Throwable grenades
* Shared reusable explosion system
* Gameplay HUD
* Player health, ammunition, grenade, wave, and enemy counters
* Game over and victory states
* Restart and quit flow
* Defined player spawn system
* Fully navigable grayboxed combat arena

## Technical Highlights

### Component-Based Architecture

Gameplay functionality is separated into focused components such as player movement, combat, interaction, health, enemy behavior, and wave management.

This keeps systems reusable and prevents large classes from taking responsibility for unrelated behavior.

### Interfaces

Interfaces such as `IDamageable`, `IInteractable`, `ICarryable`, and `IEnemyAttack` allow gameplay systems to communicate through shared contracts rather than depending on specific implementations.

For example, weapons and explosions can damage any object implementing `IDamageable`.

### Event-Driven Systems

Systems such as health, ammunition, grenades, waves, and game state expose events that other components can subscribe to.

The HUD responds to gameplay changes without needing to continuously poll those systems every frame.

### Data-Driven Weapon Configuration

Weapon statistics are stored in a `ScriptableObject`, separating weapon configuration from weapon behavior.

Current configurable values include:

* Damage
* Range
* Fire rate
* Magazine size
* Reload time

### Enemy AI

Enemies use a small finite-state machine with:

* Idle
* Chase
* Attack
* Dead

Navigation is handled using Unity's NavMesh system, while combat behavior is separated through the `IEnemyAttack` interface.

### Shared Explosion System

Grenades and environmental explosives use the same reusable explosion utility.

The system handles:

* Area damage
* Physics forces
* Layer filtering
* Multiple colliders
* Chain reactions

`HashSet` collections prevent objects with multiple colliders from receiving duplicate damage or explosion forces.

### Physics Interaction

The player can pick up, carry, drop, and throw physics objects.

Carryable objects use an interaction interface so future interactable object types can be added without tightly coupling them to the player controller.

## Controls

|Action|Input|
|-|-|
|Move|WASD|
|Look|Mouse|
|Sprint|Shift|
|Jump|Space|
|Fire|Left Mouse Button|
|Reload|R|
|Interact / Pick Up|E|
|Throw Carried Object|Q|
|Throw Grenade|G|

## Project Structure

```text
Assets/
├── Art/
├── Audio/
├── Materials/
├── Prefabs/
├── Scenes/
│   └── Arena.unity
├── ScriptableObjects/
├── Scripts/
│   ├── Combat/
│   ├── Core/
│   ├── Enemies/
│   ├── Interaction/
│   ├── Player/
│   ├── UI/
│   ├── Waves/
│   └── World/
└── Settings/
```

## Current Development Focus

The core gameplay loop is now playable from start to finish.

Current development is focused on:

* Environment art
* Materials and lighting
* Visual combat feedback
* Audio
* UI polish
* Gameplay balancing
* Additional testing and bug fixing
* Portfolio presentation and gameplay footage

## Development Goals

Salvage Protocol is being developed as both a playable prototype and a portfolio project focused on strengthening my experience with:

* C#
* Unity
* Gameplay programming
* Object-oriented design
* Component-based architecture
* Event-driven programming
* AI systems
* Physics systems
* Level design
* Debugging and iterative development

## Engine

**Unity 6.5 — Universal Render Pipeline**

## Author

**Travis Byers**

C# / C++ developer focused on software, gameplay systems, tools, and interactive applications.

