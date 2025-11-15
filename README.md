# DreamWalker

A Unity-based narrative adventure game exploring the realm between dreams and reality.

## Project Info
- **Engine:** Unity 6000.0.51f1
- **Platform:** PC/Mac

## Features

### Dialogue System
The game uses **Ink** for branching narrative and interactive dialogue.

#### Components
- **DialogueManager.cs** - Manages dialogue flow, displays text and choices
- **DialogueTrigger.cs** - Triggers dialogue when player interacts with NPCs/objects

#### Ink Story Files
Located in `Assets/InkStories/`:
- Story files use `.ink` format
- Unity automatically compiles them to `.json` files
- Placeholder dialogue is currently in place

#### Setup Guide

**1. Dialogue UI Setup:**
- Canvas with Dialogue Panel
- TextMeshProUGUI for dialogue text
- 3 Button elements for player choices

**2. DialogueManager Setup:**
- Create GameObject with DialogueManager script
- Assign UI references (panel, text, choice buttons)

**3. NPC/Object Setup:**
- Add DialogueTrigger script to interactable objects
- Add Collider component (set as Trigger)
- Assign compiled `.ink.json` file
- Optional: Add visual cue GameObject

**4. Player Controls:**
- Press **E** or **Space** near NPCs to start dialogue
- Press **Space** or **Enter** to continue dialogue
- Click buttons to select dialogue choices

#### Writing Dialogue
Edit `.ink` files in `Assets/InkStories/`. Basic syntax:

```ink
// Comment
This is dialogue text.

* [Choice 1]
    Response to choice 1
* [Choice 2]
    Response to choice 2

-> END
```

**Resources:**
- [Ink Documentation](https://github.com/inkle/ink/blob/master/Documentation/WritingWithInk.md)
- [Inky Editor](https://github.com/inkle/inky/releases)

## Development

### Recent Updates
- Implemented dialogue system with Ink integration
- Added player controller and movement scripts
- Created multiple scene tiles (Opening, Tile2, Tile3)
- Added door mechanics and scene transitions

### Scripts Location
`Assets/_Scripts/`

### Scenes
- BedroomScene
- Opening (Camron)
- Tile2 (Camron)
- Tile3 (Camron)

## Notes
- Current dialogue contains placeholder text for testing
- Actual story dialogue will be added later
