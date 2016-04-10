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
