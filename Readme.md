SNAKE LUCKY

Gameplay

Le jeux ce joue avec Z Q S D :
Z : Haut 
Q : Gauche 
S : Bas 
D : Droite


Le serpent doit manger des pommes et fait :
+ 25 points
+ 1 taille de serpent

Une boîtes bonus peut apparaître et contient :
+ 20 pts
+ 50 pts
+ 1 taille de serpent

Le jeux demarre avec 20% de chance d'apparaître et augmente de 5% a chaque mange
Le % est reset quand un bonus apparait

Objectif : 

Le joueur doit faire le score le plus haut 
Un tableau des 10 meilleurs score est visible

Condition de victoire :
Remplir la grille 

Condition de defaite :
La tête du serpent touche un mur ou ce touche soi même


Stucture 

Assets : Contient tout les PNG
Component : Gere les composant visuel et leurs logique (MAP, PNJ, OBJET, MENU) 
Enums : Contient les bonus de cube 
SceneGame : Contient toutes les scenes
SceneUtils : Manage les SceneGame
Services : Les outils 


Algorithme de queue : 
Utilsation d'une "Queue<T>"
La tête ce situe en dernières position et j'empile a chaque aggrandissement du serpent

Utilsation d'un service locator :
- Score en cours de partie
Recupere/MaJ temps réel
- HightScore 
Recupere/Actualise/Affiche le score board
