#  Infinity Task

A mobile puzzle game built in Unity where players rotate grid elements to connect power sources to terminals. Developed as a technical assessment focusing on clean architecture, mobile responsiveness, and data persistence.

##  Features

### Gameplay
* **Node-Based Puzzle Mechanics:** Rotate lines and curves to complete circuits.
* **Progressive Difficulty:** Multiple levels with increasing complexity.
* **Visual Feedback:** Particle effects (sparks) and dynamic lighting upon circuit progression.

### Technical Implementation
* **Mobile-First Design:** Custom camera system that adapts to any aspect ratio (Phone/Tablet) while maintaining grid visibility.
* **Data Persistence:** JSON-based save system to track level completion and high scores locally.
* **Analytics Integration:** Amplitude SDK implementation for tracking core lifecycle and Gameplay Events (`AppInitialized`, `LevelCompleted`).
* **Scalable Architecture:**
  * Strict separation of concerns (Core, Data, InGame Managers).
  * Event-driven communication (Observer pattern) to decouple UI from Game Logic.

##  Tech Stack
* **Engine:** Unity 6.0
* **Language:** C#
* **Plugins:** Amplitude SDK, TextMeshPro
* **Platform:** Android

## Installation & Testing

**Download the latest APK:**
[<img src="https://img.shields.io/badge/Download-.apk-brightgreen?style=for-the-badge&logo=android">](https://github.com/ChrystianShiruo/infinity-task/releases/latest)

1. Download the `.apk` from the [Releases Page](https://github.com/ChrystianShiruo/infinity-task/releases).
2. Install on any Android device.
3. **Note:** The app is forced to **Portrait Mode**.

## Project Structure

Important directories for reviewers:

```text
Assets/
├── Scripts/
│   ├── Setup/ # Bootstrapper, Scene Loading, Main Scene setup, DataManagement, LevelManagement
│   ├── Data/ # ScriptableObject and .json data structure
│   └── InGame/ # Level specific components and Level related Events
