# Kinteractions VR
**KVR** is a VR interaction system/ Hand controller we use at kandooz studio, the philosiphy behind KVR is
- Keep it simple ( should only drag monobehaviours to make things work)
- all aspects of it must be controlled from the unity Editor( without any external tools)
- Cross platform ( it shoud work no matter the platform) 

# Core Components:
- [Hand Animation System](HandAnimationController.md)
- [VR Input System](VRInput)
- [Interactions system](Interactable)

# how to use it

## you need a hand
As a start you need a rigged hand model with at least two animation poses:
- **relaxed** : this should be the hand netural(open position).
- **fist** : as the name implies just a fist pose.
we happen to be selling some hand models that are preconfigured for KVR you can find them on the [asset store](https://assetstore.unity.com/publishers/36568)

## configuring the hands

1. put your hand model inside an empty GameObject with uniform scale
2. Add the HandAnimationController script to the empty game object
3. Create prefabs for both Right and left hands
4. Create a hand data asset in the project tab
	
    > Right click on the assets Folder -> Create -> Kandooz -> KVR -> HandData
5. Drag the prefabs you just created in this asset
6. drag the animations you have to the Open/Close elements

![The HandData asset](/Readme/HandData.png)

7. this one is a bit tricky but you need to create an [avatar mask](https://docs.unity3d.com/2017.4/Documentation/Manual/class-AvatarMask.html) for each finger 
8. drag the created masks into the same Hand data Asset file


and that's it, the hand is now configured

## Using the hands with your custom interaction system and your own inputs

![Hand Animation Controller](/Readme/HandsInAction.gif)
now you just need to drag the hand you created to the scene and by changing the sliders for every finger you can change if it is closed or opened.

you can control the fingers from another script from the hand animation controller

	var animationController = GetComponent<HandAnimationController>();
	FingerName fingerName = FingerName.index; // all fingers are stored here from the thumb to the pinky
	animationController[fingerName] = value; // you can also use an int ( thumb is 0, pinky is 4)


## using our built in Input manager
we built an small input wrapper on top of the unity Input Manager, to use it you need to:

1. create an input manager asset 
	>Right click on the project tab -> Create -> Kandooz -> KVR ->VR Input Manager
2. click Seed axis
3. Add the HandInputManager Component on your hand prefab
4. attach a headset and press play



## Grabbing objects and interactions

if there is anyobject in the scene you want to be grabable just add the Interactable component to it as well as the throwable component

then configure how the object will be grabbed by clicking on the Edit Right hand / Edit left hand button


![Interactable](/Readme/Interactions.gif)
