# Hand Animation System

This system can be considered the breads and butter of KVR, on it's core it's based on [unity Playables](https://docs.unity3d.com/Manual/Playables.html), and [Unity Avatar masks](https://docs.unity3d.com/2019.4/Documentation/Manual/class-AvatarMask.html).

## HandAnimationController.cs

This script is a monobehaviour that controls the way the hand looks, this component exposes float values that controls every finger closure, the values are between 0 and 1 where 0 is fully open, and 1 is closed as a fist

![Hand Animation Controller](/Readme/HandsInAction.gif)

those values can be accessed from other scripts by using an indexer like so:

	var animationController = GetComponent<HandAnimationController>();
	FingerName fingerName = FingerName.Index; // all fingers are stored here from the thumb to the pinky
	animationController[fingerName] = value; // you can also use an int ( thumb is 0, pinky is 4)

you can easily integrate this into your favorite VR SDK by reading the values from the controller and setting them directly

## FingerName
finger type is an enum that has one of the follwoing values
        Thumb = 0
        Index = 1
        Middle = 2
        Ring = 3
        Pinky = 4
        Trigger=5
        Grip=6
**trigger** and **grip** are used for the Input system and are useless with the hand animation system, so you can just ignore them here 

## Custom pose support

the system also support custome poses in case you want to make a pose that can't be achieved with simple finger rotations.
![CustomePoses](Readme/CustomPoses.gif)

### adding poses to the list
a custom pose is just an animation file with one keyframe that represent the target position of the hand.

you can add the pose to the [Hand data asset](HandData) by just dragging the animation to the poses list on the bottom of it
![CustomePoses](Readme/HandAssetPoses.gif)


### changing poses from script
all you need to do is just access the Pose property and set it to the pose number you want as well as setting  __StaticPose__ to true
    var animationController = GetComponent<HandAnimationController>();
    animationController.StaticPose=true;
    animationController.Pose=2;// will set it to the third pose in the list

Make sure to set it back to false if you no longer need the custom pose