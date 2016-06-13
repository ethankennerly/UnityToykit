# Unity Toykit

Lightweight MVC tools for games in Unity 3D C#.

## Features

Convenient:

* Set animation.
* Set text.
* Play sound.
* Listen to keys and buttons.
* Setup a game.

Portable:

* Toolkit.
* Model.
* View Model.
* Controller.
* View.

## Installing

As submodule:

git submodule add git@github.com:ethankennerly/UnityToykit.git Assets/Scripts/UnityToykit

or:

git submodule add https://github.com/ethankennerly/UnityToykit.git Assets/Scripts/UnityToykit

In Unity 5.3.4, I saw a message like:

		error CS0433: The imported type 'NUnit.framework.Assert' is defined multiple times

Then I renamed the nunit.framework.dll to nunit.framework.dll.bak.  It appears as if the newer version or that environment already had Nunit DLL.  If your version of Unity needs that DLL, you can rename it back to nunit.framework.dll

https://bitbucket.org/Unity-Technologies/unitytesttools/issues/54/error-cs0433-the-imported-type

## Setting Up

cp UnityTokkit/Examples/MainExample.cs Main.cs
cp UnityTokkit/Examples/ModelExample.cs Model.cs

Rename the class MainExample to Main.
Rename the class ModelExample to Model.

In Unity, name your root GameObject "Main".
Add component "Main.cs".

The scene graph maps to the descendents of Main.

## Example Game Jam

https://github.com/ethankennerly/add-it-up

Made in one day.

For example of setting text, see:

Assets/Scripts/Main.cs
Assets/Scripts/Model.cs
