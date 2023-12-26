Unity Game Dev – Case Study Adriano Amato
This ReadMe should explain every solution implemented for the given taks, if something is 
unclear please contact me, I will be more than happy to give extra explinations!
#1 Optimizations
For the first point, optimizing the UI assets, I removed every Raycast target from Images that shoudn't be interactable (I.E. the level bar images) and furthermore 
I enabled GPU instancing for the barrel materials.
As for the pooling system for the barrels I added the Singleton "TileSpawner", the name is used to follow the projects naming convention, in which I create a pool of Tiles
with a minSize and a maxSize that can be easily changed in the inspector (the script is attatched to the Tower prefab), I moved the choice of the tile to spawn in the 
CreatedTile() method and then in the "Tower.cs" script I simply ask to get a Tile from the TileSpawner when the tower needs one and release the ones that get destroyed.

2# Mission System
To access the mission system, I added an access point that followed the UX design of the Options button, it interacts with the "MissionPopup" that displays relevant
informations about the mission. To implement the Mission system I added a MissionManager that holds a dictionary of all the missions and holds a reference to the
number of missions completed and the entry point to enable it, it also defines events to handle the start, advancement and finishing part of a mission, triggering them
when appropriate (I also implemented an "EventManager.cs" that defines what kind of events missions and the MissionManager use, I moved it in the project settings to
ensure that the methods are initialize before any OnEnable and avoid any race condition). The missions and the respective states are stored in the PlayerPrefs using a 
simple conversion to Json, this could be improved with a dedicated saving system that encrypts the savefile.
The single missions are defined with a structure that divides static and non-static data of the mission:
1. Static information: the static informations are stored in a ScriptableObject "MissionInfoSO" that holds a unique Id for the mission, the display name, the prerequisite 
of the mission (other missions to complete before you can start it) and the steps to complete the mission. The mission steps are implemented with an abstract class called "MissionStep" that holds 
the difficulty of the step, a display name, an internal boolean to check if the step is finished, the mission Id and the index of the step; this abstract class defines the firing of events linked
to missions, to notify everyone about a finished step, a change of state of a step and the initialization of the step. Mission steps need to be completed in the selected order in the "MissionInfoSO".
2. Non-Static information: The non-static informations are stored in a "Mission" class that holds the static informations of the MissionInfoSO, the state of the mission, the index of the current 
MissionStep and an array of states for the MissionSteps. This class handles the interaction with the mission and its steps with methods like "MoveToNextStep()".
