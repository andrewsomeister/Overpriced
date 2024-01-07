# Virtual Reality Game - Overpriced
Authors: Yiyuan Fang, Zhe Xiong, Andrew Shusterman

## Introduction
"Overpriced" is a dynamic VR game where you join a busy food truck team. Tasked
with crafting burgers per wall-displayed orders, each accurate creation earns you
$20. The game, lasting an intense 5 minutes, involves chopping vegetables, cooking
steaks, fetching buns, and assembling burgers in sequence. Keeping your workspace
clean is key, as you discard trash balls from chopping. Successfully complete an 
order by placing it in the Collection zone and pressing the button, aiming for 
accuracy to avoid error sounds and earn revenue.

## Demo

<a href="http://www.youtube.com/watch?feature=player_embedded&v=UvlmjfvHIok" target="_blank">
 <img src="http://img.youtube.com/vi/UvlmjfvHIok/maxresdefault.jpg" alt="Watch the video" width="240" height="180" border="10" />
</a>

Click image to watch demo!

## Mechanics / Components
### Main Menu
Players enter a Main Menu scene to choose between the tutorial and the game.

### Tutorial
The tutorial is set up as another scene with the same environment as the
main game, but with visual and audio assistance to help the player find
objects, learn movements, and understand the essential game mechanics.

### Grabbing
When the player presses and holds both buttons ("A" and "B" or "X" and
"Y") as well as both triggers (middle and index) on a controller, they can
grab an object. Upon releasing any button or trigger, the object will
detach from the controller and fall with gravity. This works for both
controllers.

### Opening Fridge
When the player hovers his controller over the fridge door, the fridge
door will highlight indicating that the player can open the fridge.
Then, after a "grab" motion with all four buttons pressed, the fridge
door will open, playing a door opening sound.

### Cutting
There are two objects in the fridge that you can "cut" with a knife -
"Lettuce" and "Tomato". After grabbing the object out of the fridge and
placing it ideally on the cutting board, you can then grab the knife and
swing the knife at the object, which will cut "slices" of the vegetable
that can then be placed on the burger. Cutting motion is force
sensitive. If there is too little force, the knife won't penetrate. If
there is too much, the tomato will be destroyed into a trash ball.

### Object Regeneration
Most "Grabbable" objects can regenerate at the original position after
they are grabbed. This allows the user to grab an unlimited number of
ingredients of their choice to assemble burgers.

### Steak Cooking
For the "steak" object inside the fridge, if it is placed on top of a
pan on the Cooking Stove, a growing timer appears above the steak. While
the steak is "cooking" a ticking sound will play. After 10 seconds, the
steak will finish cooking, and a "ring" sound will play indicating the
steak is done. The steak turns into a cooked patty, which you can then
place on the burger.

### Trash Generation
Trash balls will generate in two scenarios. Either when you successfully
finish chopping a "tomato" or "lettuce", or when you chop a vegetable
with too much force. These trash balls are grabbable, and throwable, and
you are meant to throw the trashballs into the trashcan located at the
end of the food truck. This feature serves no real purpose, mainly just
an annoyance when there are to many trash balls and a fun gimmick to
"throw out the trash".

### Burger Assembly System
The player is expected to place the prepared ingredients into a burger
box to assemble a burger. Any "Grabbable" objects can be dropped into
the burger box; however, only food ingredients will snap into place and
stack up as the next burger layer, while other objects will trigger an
error sound.

### Order Verification System
An assembled burger is checked against the orders when the player moves
the tray into the collection area and presses the push button. If the
burger ingredients match any of the orders shown on the wall and are
placed in the same sequence, a success sound is played, the revenue
increases by \$20, and a new order is generated. Otherwise, an error
sound is played and the controllers vibrate. Either way, the burger box
gets cleared so the player can assemble the next burger.

## Interactions / Implementation (Class requirements of the game)
### Throwing
The player can interact with various objects in the food truck. One
interaction is throwing. Trash balls generated during cutting can be
thrown into the trash bin. Ingredients can also be thrown from one place
to another.
#### Implementation
The throwing interaction is implemented using Unity's physics engine.
The LinearVelocity and AngularVelocity of the player's hand movement
are captured when the object is released (any of the four buttons is
released). This information is then used to apply a corresponding force
and direction to the object in the game's physics engine.

### Filling a Container
Filling a container is implemented by allowing ingredients to be placed
into the burger box. The ingredients then move together with the burger
box when the tray is grabbed.
#### Implementation
The BurgerAssembly.cs script attached to the Burger Boxes in the scene
handles stacking up the ingredients and filling the burger box. It
maintains a list of ingredients already placed in the box. Whenever a
food ingredient is released inside the box's trigger collider, it finds
the burger base or the last ingredient's box collider and positions the
new ingredient relative to the collider bounding volume's center and
height. Added ingredients are set to have the burger base as the
transform parent so they all move together when the burger box moves.

### Swing and Hit
Swing and Hit is used when cutting vegetables. The user can place either
a lettuce or a tomato on the cutting board, then hold the knife, and
swing with appropriate force to cut the vegetable into slices. If the
force is within a desired range, the user can cut the vegetable into cut
limit number of slices. If the force is too much, it will destroy the
vegetable, and a trash ball is generated. If too little force is
applied, nothing will happen.
#### Implementation
The swing and hit interaction is implemented using Colliders attached to
the object, and the Unity function "On-CollisionEnter". When a "tomato"
or "lettuce" object would detect a collision with the "knife" object, a
"slicing" sound would play, and a "slice" prefab specific to the
vegetable would spawn.

### Alternate Grab
"Extended Grab" is used to implement alternate grab. Although most
objects can be grabbed by pressing both buttons and both triggers, some
objects are located out of comfortable reach, such as burger buns and
fries. To reach them, point the controller at the objects. The
controller then highlights and emanates a ray to the pointed object. The
player can then perform the same "grab" action to move the object to the
desired location. Similar to grab, release any button also releases the
object. Note: This only works on the right hand.
#### Triggering and Effect
The alternate grab is triggered by 2 specific types of objects in the
scene. The fries, and burger buns located on the shelf on one side of
the truck. When your joystick is pointed roughly in the direction of
these items, a thin blue ray will emanate from your controller to the
object, and the object will highlight. The Effect is that once
highlighted, the user can "grab" the object as he would normally by
pressing all 4 buttons, and moving it with the ray still attached to a
desired location.
#### Implementation
The alternate grab interaction is implemented using the built-in Unity
"Ray" and "RayCast" objects. At all times, this "ray" emanates from the
player's hand, but it is hidden until the engine recognizes that the
"RaycastHit" is hitting the object (to which the AlternateGrab script is
attached). The ray is then enabled and the object that is being hit is
highlighted with an alternate "highlight" material. The object is then
treated like a normal "Grabbable" object, and moves around with the
movement of the players hand. Upon release, the object reverts back to
its default material and its "AlternateGrab" script is disabled, since
it now needs to be assembled onto the burger.

### Alternate Locomotion
Alternate locomotion is achieved by allowing the player to move around
by swinging his arms while pressing down on both triggers on both
controllers. This simulates the running motion and the finger positions
while running in real life.
#### Implementation
The implementation takes the y-axis rotation of the camera as the
forward direction to apply player movement. The distance by which both
controllers moved while arms are swinging is scaled up as the distance
for player movement. Details of the implementation are in
SwingingArms.cs.

## Group Member Contribution
### Andrew
 - Built the environment, including the food truck, kitchen components,
road/sidewalks, regenerating cars, buildings, background music,
etc...
 - Built the main menu and all associated interactions (entering
tutorial or playing game)
 - Implemented Fridge Door Opening, Steak Cooking, and the Extended
Grab.
 - Helped bug fix for other features such as grabbing and throwing.
 - 
### Yiyuan
 - Made the tutorial with audio guidance, visual cue and checking points.
 - Made cuttable vegetables and swing and hit motions.
 - Realized throwing trash motion with trash balls and trash cans
 - Added various sound effects and haptic feedback.

### Zhe
 - Implemented locomotion by swinging arms
 - Implemented burger assembly, order generation and verification
 - Implemented Game Manager with a timer and revenue system to control the game play
