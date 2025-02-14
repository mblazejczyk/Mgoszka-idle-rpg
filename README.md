# Mgoszka
[GameJolt](https://gamejolt.com/games/mgoszka/578490) [Google Play](https://play.google.com/store/apps/details?id=com.YellowSink.Mgoszka)

## Description
Mgoszka is a simple idle RPG game where you have the opportunity to collect items, upgrade your character, and fight enemies. The game is intentionally slowed down to allow for "on the side" gameplay. The game offers:
- Various NPC quests,
- Daily quests,
- Equipment upgrades,
- Item collection,
- Development of multiple stats,
- Combat system,
- Skill system,
- In-game guides,
- Interactive map,
- Automatic save system,
- Language options between Polish and English.

## Scripts

Below is a list of scripts along with their descriptions:

| File Name                  | Description |
|----------------------------|-------------|
| **ActiveFalse_timer.cs**      | The script deactivates an object after a specified time. If the object is active, it starts a coroutine that waits for the given time and then disables the object. |
| **AnimationController.cs**    | Controls the player's animation based on their movement. It checks for position changes and sets the appropriate animation transition value. |
| **DownloadUpdate.cs**         | Opens a given URL in the user's default browser. |
| **EqSystem.cs**               | Manages the player's inventory, stores items, handles their use, removal, and displays detailed information. |
| **Fight.cs**                  | Handles the combat system, including attacking, dodging, critical hits, and the run and teleportation system. |
| **MapSizeSystem.cs**          | Manages the size and content of the map, allows zooming in and out, and hides/reveals elements based on the player's knowledge of the area. |
| **MissionSystem.cs**          | Manages the quest system, saves player progress, and rewards for completing quests. |
| **NPC_spawner.cs**            | Responsible for dynamically spawning NPCs in the game world, adjusting their locations and properties. |
| **OnTriggerAction.cs**        | Executes specific actions upon entering a trigger, such as starting dialogues or interactions. |
| **PlayerStats.cs**            | Stores the player character's stats, such as health points, experience, attack, and defense. |
| **SettingsSystem.cs**         | Manages game settings, saves and reads them, allowing for gameplay customization. |
| **ShopSystem.cs**             | Handles the shop system, allowing for buying and selling items. |
| **TimeCountingSystem.cs**     | Monitors game time, such as day and night cycles, and counts down to events. |
| **TranslationSystem.cs**      | Manages translations and displays texts in different languages. |
| **TravelPreparations.cs**     | Prepares the player for travel, manages inventory and quest requirements. |
| **UiController.cs**           | Controls the user interface, handles menus, HUD, and interactive elements. |
| **VersionController.cs**      | Checks and manages the game version, enabling updates and save compatibility. |
| **enymieStats.cs**            | Stores enemy stats, including their health, damage, armor, and rewards. |
| **itemParameters.cs**         | Defines item parameters, such as their effects on the character, stats, and rarity. |

The file contains scripts used in the Unity game, mainly for managing gameplay mechanics, animations, inventory, and combat.

## Requirements
Unity 2020 or newer

## Author
Mateusz Błażejczyk
