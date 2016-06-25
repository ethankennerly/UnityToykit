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

Because the models and controllers are separated from the view, the models and controllers are easy to test.
http://www.gamasutra.com/view/news/164363/Indepth\_Unit\_testing\_in\_Unity.php

## Installing

As submodule:

git submodule add git@github.com:ethankennerly/UnityToykit.git Assets/Scripts/UnityToykit

or:

git submodule add https://github.com/ethankennerly/UnityToykit.git Assets/Scripts/UnityToykit

Unity 5.2 or lower
==================

Unity 5.3 builds in NUnit.

http://forum.unity3d.com/threads/editor-test-runner-nunit.358248/

On Unity 5.2 or lower, you can install UnityTestTools from the Asset Store.  I had these included in an earlier commit.  You could download those which are smaller:

https://github.com/ethankennerly/UnityToykit/tree/f2a01b622e39bf52bab54036d710d67f565c7128/UnityTestTools


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
