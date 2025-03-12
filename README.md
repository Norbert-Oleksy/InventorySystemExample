# InventorySystemExample
 Recruitment task

## Video
The video shows how the application works in practice

[Watch video](https://youtu.be/sIAtIIp5L9c)

## Quick Overview
### Game flow
Items are loaded from `GameServerMock` into the player's backpack.<br>
(Optional) The player can select which items to equip in the inventory window before starting the battle.<br>
Press the Play button to start the battle.<br>
The player can move around the arena, fighting ghosts.<br>
A new ghost appears every 30 seconds.<br>
Attacking a ghost deals damage to it, eventually killing it when its health reaches zero.<br>
Ghosts try to collide with the player to deal damage.<br>
When the player's health reaches zero, they die, causing the scene to reload.<br>
### Controls
#### UI
- **Mouse** – Interact with UI
#### Game
- **WASD** – Move the player
- **Mouse** – Rotate the player
- **Left Click** – Attack

## Task
### Objective
The goal of this task is to create a simple game using items retrieved from the `GameServerMock` class.<br>
The application should include the following elements:
- Inventory, a UI view that allows equipping and unequipping items.
- 3D gameplay that utilizes the equipped items.
### Inventory - UI View
#### Required Features:
- Displaying items.
- Equipping items.
- Unequipping equipped items.
- A "Play" button that allows starting the game.
#### Assumptions:
- The player can wear only one item per category.
- The game can be started without equipping any items.
- When loading the UI view with items, no item should be equipped by default.
- Attributes of items that are not used in the 3D gameplay should not be displayed in the UI view.
### 3D Gameplay
#### Required Features:
- None.
#### Assumptions:
- The gameplay must utilize at least one of the following attributes:
*Damage*, *HealthPoints*, *Defense*, *LifeSteal*, *CriticalStrikeChance*, *AttackSpeed*, *MovementSpeed*, *Luck*.

## Project
### How It Works
We can divide the gameplay into two stages.<br>

**Menu Stage**<br>
In this stage, the inventory UI is displayed to the player.
It focuses on implementing the core requirements of the task:
- Loading item data and allowing the player to interact with their inventory.
- The player can freely choose and swap equipped items from the available selection.
- Item and player statistics are displayed.

**Arena Stage**<br>
This is the combat phase, where the player fights ghosts in the arena.
Its purpose is to put into practice the choices made in the menu stage.
The player's stats, including both base stats and those from equipped items, directly impact gameplay:
- *HealthPoints* determine how long the player can withstand damage.
- *Defense* reduces the amount of damage taken.
- *MovementSpeed* affects how fast the player moves around the arena.
- *Damage* determines how much damage the player deals when attacking.

## Used
- Unity (version 2021.3.44f1)
- Assets, textures, sprites, etc., that were provided to me along with the task by the recruiter
