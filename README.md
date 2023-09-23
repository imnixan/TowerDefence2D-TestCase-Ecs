Unity test ecs; 

Tower types:

- Base tower, if destroyed game ends. click on it to heal;
- empty place, click on it to spawn defence tower;
- Defence tower, just tower to distract enemies from base tower, click on it to upgrade to AttackerTower;
- Attacker tower, tower that attacks enemies, click on it to heal;



Enemies types:
- goblin, melee attack;
- archers, range attack;

Code organisation:


Gameplay based on LeoEcs systems;

Entry point - class GameStarter which start class EcsStart, when player hit play button;
