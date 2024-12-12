# Unity Play Bar Extender

**Play Bar Extender** is a package made for Unity 6 that allow to add features to the play.

![AssetsDirectoryCreator](https://github.com/user-attachments/assets/fd87abab-cf3c-41f3-85ba-6cb3b28cbf07)

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

### Time Scaler
- Change the time scale directly from the slider on the play bar.

### Cheat Sheet
- Call function anytime: Call gameplay function by clicking a button to make debug easier.
- Customize Actions: You can add your own functions using a ScriptableObject, configurable via an editor window.

## Installation

In order to use Play Bar Extender, you can import the Unity Package available in [Releases](https://github.com/Theo-Mestre/Unity-Playbar-Extender/releases)

You can also download the sources and drop them in your project Assets folder.

## Usage

Once the package is imported to your project. The `Play from here` button and scenes switchers should appear. If that's not the case, check for errors in the console.

You can enable `Play from here` by the clicking the toggle button.

![PlayFromHere](https://github.com/user-attachments/assets/59b372f4-1b12-4a4f-b4fc-7e7b771308d9)

You can also add Function to be called when `Play from here` is enabled.
Go to `Tools/Play Bar Extender Settings`, this will open a window where you can change the settings of the button.

![AssetsDirectoryCreatorWindow](https://github.com/user-attachments/assets/7feb51da-f5eb-4fba-b66b-9fae694429cc)

Since **version 1.1**, this window also allow you to add function to the cheat sheet dropdown menu and edit the time scale slider's clamp values

The Cheat Sheet may not appear until there are **functions registered!**

## Requirements

- Unity 6.0 or higher

Might work on previous version but I didn't tested it

## Contributions
Contributions are welcome! 
Please create a new branch, and submit a pull request.  <br>
For issues or feature requests, please open a ticket in the Issues section.

## License

This package is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
