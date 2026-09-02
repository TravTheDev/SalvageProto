# Salvage Protocol

Salvage Protocol is a work-in-progress first-person action prototype built with Unity and C#. The project focuses on modular gameplay programming: player movement and combat, reusable damage systems, enemy AI, physics-based interactions, explosive objects, and wave progression.

The repository documents my ongoing development process and demonstrates how I structure gameplay systems so they remain reusable, configurable, and easier to extend.

> **Project status:** Active development. Core gameplay systems are functional; UI, feedback, content, and visual presentation are still being refined.

## Current Features

### Player

* First-person movement using Unity's `CharacterController`
* Walking, sprinting, jumping, and mouse-controlled camera movement
* Input handled through Unity's Input System

### Combat

* Hitscan weapon firing with configurable damage, range, fire rate, magazine size, and reload time
* Weapon statistics stored in reusable `ScriptableObject` assets
* Grenade throwing with a timed fuse, physics, radial damage, and explosion force
* Shared health and damage contracts that can be used by players, enemies, and world objects

### Enemies and Waves

* NavMesh-based enemy movement
* State-driven enemy behavior: idle, chase, attack, and dead
* Interface-based enemy attacks
* Configurable enemy waves, spawn intervals, and spawn locations
* Events for wave starts and completion

### Interaction and World Objects

* Interface-driven interaction system
* Physics objects that can be picked up, carried, dropped, and thrown
* Damageable explosive barrels
* Shared explosion logic that prevents objects with multiple colliders from receiving duplicate damage or force

## Technical Highlights

* **Interface-driven systems:** `IDamageable`, `IInteractable`, `ICarryable`, and `IEnemyAttack` separate gameplay contracts from individual implementations.
* **Component-based design:** Health, death behavior, attacks, movement, and interaction are composed from focused MonoBehaviours instead of being placed in one large controller.
* **Reusable explosion handling:** Grenades and explosive barrels both use the same explosion utility for radial damage and physics force.
* **Event-driven communication:** Health and wave systems expose events so other systems can react without tightly coupling their implementations.
* **Data-driven weapons:** `WeaponData` keeps weapon tuning separate from firing behavior and allows new configurations to be created in the Unity Editor.
* **Defensive checks:** Gameplay systems validate required references and state before performing actions.

## Selected Source Files

* [`Health.cs`](Assets/Scripts/Core/Health.cs) - reusable health, healing, damage, and death events
* [`ExplosionUtility.cs`](Assets/Scripts/Core/ExplosionUtility.cs) - shared radial damage and physics-force handling
* [`HitscanWeapon.cs`](Assets/Scripts/Combat/HitscanWeapon.cs) - firing, ammunition, timing, raycasts, and reloading
* [`WeaponData.cs`](Assets/Scripts/Combat/WeaponData.cs) - ScriptableObject-based weapon configuration
* [`EnemyController.cs`](Assets/Scripts/Enemies/EnemyController.cs) - NavMesh movement and enemy state transitions
* [`WaveManager.cs`](Assets/Scripts/Waves/WaveManager.cs) - configurable wave spawning and progression
* [`PlayerInteraction.cs`](Assets/Scripts/Interaction/PlayerInteraction.cs) - object detection, interaction, carrying, dropping, and throwing

## Project Structure

```text
Assets/
|-- Scenes/                # Playable arena scene and navigation data
|-- Prefabs/               # Enemies, grenades, and interactive objects
|-- ScriptableObjects/     # Weapon configuration assets
`-- Scripts/
    |-- Combat/            # Weapons, grenades, and combat input
    |-- Core/              # Health, damage contracts, and shared utilities
    |-- Enemies/           # Enemy state logic and attacks
    |-- Interaction/       # Interactable and carryable object systems
    |-- Player/            # First-person movement and camera controls
    |-- Waves/             # Enemy spawning and wave progression
    `-- World/             # Explosive and destructible world objects
```

## Controls

|Action|Keyboard and Mouse|
|-|-|
|Move|`W`, `A`, `S`, `D`|
|Look|Mouse|
|Sprint|`Left Shift`|
|Jump|`Space`|
|Fire|`Left Mouse Button`|
|Reload|`R`|
|Interact / pick up / drop|`E`|
|Throw carried object|`Q`|
|Throw grenade|`G`|

## Built With

* Unity `6000.5.5f1`
* C#
* Universal Render Pipeline `17.5.0`
* Unity Input System `1.19.0`
* AI Navigation `2.0.14`
* Visual Studio
* Git and GitHub

## Getting Started

### Requirements

* Unity Hub
* Unity Editor `6000.5.5f1`
* Git

### Run the Project

1. Clone the repository:

```bash
   git clone https://github.com/TravTheDev/SalvageProto.git
   ```

2. Add the cloned folder as a project in Unity Hub.
3. Open the project with Unity `6000.5.5f1`.
4. Open `Assets/Scenes/Arena.unity`.
5. Enter Play Mode.

Unity will restore the required packages from `Packages/manifest.json` when the project is opened.

## Current Development Focus

* Completing the player HUD and combat feedback
* Refining aiming, weapon behavior, and moment-to-moment responsiveness
* Expanding enemy and wave variety
* Improving visual and audio presentation
* Preparing a downloadable gameplay build and demonstration video

## What This Project Demonstrates

Salvage Protocol is intended as a gameplay-programming portfolio project. It demonstrates my experience with C#, Unity, object-oriented programming, interfaces, events, ScriptableObjects, state-based AI, physics interactions, reusable gameplay utilities, and iterative debugging.

## Developer

**Travis Byers** - Junior Gameplay Programmer

* [GitHub](https://github.com/TravTheDev)
* [LinkedIn](https://www.linkedin.com/in/travis-dev)

