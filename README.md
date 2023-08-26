# Multiplayer Mobile Strategy Game Case

This case study was created for Happy Hour Games within a 1-week period.

## Development Progress
Check out the development progress on [Trello](https://trello.com/b/7ZI1egfi).

## Information About The Project

In this project, we've implemented a matchmaking system to pair players with others at similar skill levels. After the match, each player is assigned a color, and a specific number of units are spawned accordingly, all in the player's designated color. Players have control over these units, allowing them to gather resources and explore the environment. Additionally, players can keenly observe the actions and movements of their counterparts.

### Unit Selection

Players have multiple methods to select units:

- Single click to choose a single unit.
- Hold Shift and click to select multiple units at once.
- Draw a square on the screen by holding down the left mouse button to select units within the enclosed area.

### Camera Movement

Navigate the camera using the W-A-S-D keys.

## Technical Considerations

### Room Manager
- The room management system allows players to initiate room search processes suitable for their own skill levels.
- If a room cannot be found, it starts a process to create a room at their own skill level, allowing players to enter rooms matched with their skill levels.

### Network Instantiater
- In object creation for the Photon server, it consistently requests the path information of the object in the resource folder as a string.
- To overcome this inconsistency, we save the objects and their paths in the resources at the beginning of each build process using a scriptable object.
- This way, when sending an object to the Network Instantiater for creation, we find the object in the saved directory and send its path to the Network Instantiater, achieving simplicity in usage.

### Unit Management
- The game manager creates units and generates a specific number for each player in the room during creation.
- During creation, the units' colors are set to each player's own color, allowing players to know which units belong to them and are under their control.

### Game System
- The game system has several interfaces that determine the functions of the game systems.
- These functions regulate the manageability and usability of the game systems.

## Game Manager
- The game manager is the fundamental controller that creates, manages, and operates game systems.

### Command System
- The command system has been developed to be adaptable for every object and mechanism. It can send and process various types of variables.
- The main goal here is to create a user-friendly and easily applicable command system, free from the complexity typically associated with command systems.
- Commands are created with desired variables, processed and executed through providers and responders.

## Gameplay Video
Click on the image to watch the video.
[![Proje Videosu](https://img.youtube.com/vi/1usfHUd-TiA/maxresdefault.jpg)](https://www.youtube.com/watch?v=1usfHUd-TiA)
