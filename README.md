# Unity Play Bar Extender

**Play Bar Extender** is a package made for Unity 6 that allow to add features to the play.

![PlayBarExtender](https://github.com/user-attachments/assets/83ab38f0-bb3d-489c-a994-f579d5e77489)

This project has been inspired by [marijnz's unity-toolbar-extender](https://github.com/marijnz/unity-toolbar-extender).

## Features

### Custommize the play bar
- Add your own features to the play bar easily.
  
### Scene Switcher
- Automatic Scene Buttons: Displays buttons for all scenes in your project (scenes must be included in the build settings).
- One-Click Scene Switching: Click a button to instantly switch scenes without needing to find them in the Project window.
  
### Play From Here
- Spawn Player at Editor Camera: Automatically place your player at the editor camera's location when starting the game.
- Move Player While Playing: Relocate the player to the editor camera's current position during play mode.
- Customizable Actions: Add custom functions to the "Play From Here" behavior using a ScriptableObject, configurable via an editor window.

## Installation

In order to use Play Bar Extender, you can import the Unity Package available in [Releases](https://github.com/Theo-Mestre/Unity-Playbar-Extender/releases)

You can also download the sources and drop them in your project Assets folder.

## Usage

Once the package is imported to your project. The `Play from here` button and scenes switchers should appear. If that's not the case, check for errors in the console.

You can enable `Play from here` by the clicking the toggle button.

![PlayFromHere](https://github.com/user-attachments/assets/59b372f4-1b12-4a4f-b4fc-7e7b771308d9)

You can also add Function to be called when `Play from here` is enabled.
Go to `Tools/Play Bar Extender Settings`, this will open a window where you can change the settings of the button.

![PlayBarExtenderWindow](https://github.com/user-attachments/assets/87554041-79f3-4447-bbd4-ad039d567415)

## Requirements

- Unity 6.0 or higher

Might work on previous version but I didn't tested it

## Contributions
Contributions are welcome! 
Please create a new branch, and submit a pull request.  <br>
For issues or feature requests, please open a ticket in the Issues section.

## License

This package is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
