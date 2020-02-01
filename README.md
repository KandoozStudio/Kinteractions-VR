# What is KVR?

KVR is a VR interaction system/ Hand controller we use at kandooz studio, the philosiphy behind KVR is 
 - Keep it simple ( should only drag monobehaviours to make things work)
 - everything should be unity based ( should use only unity APIs)
 - Cross platform ( it shoud work no matter the platform)
Animation controller
---
the animation controller is the core part of KVR assets it's designed from the ground up using the playable API, to support independent finger movement and custom poses.
the custom poses should be implemented as normal unity animations and added to the hand

Interaction System
---
The Interaction system is the second part of KVR, it is mainly for defining how objects will be held using the defined hands, and what level of control is given to the user


while KVR has been part of the Kandooz VR hands from the start we are rewriting it from scratch as an open source project to be more extendable and easier to use
