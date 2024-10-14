# **S-PONG**
<p align="center">
<img src="https://github.com/wahyuwerayana/S-PONG/assets/115724777/2d7d8995-1cf6-4ea0-944f-9b02478d0b53" width="60%">
</p>

## 📄Description
**S-PONG** is a 2D Pong Game with space theme created with Unity Engine.

## 🎯Gameplay
Players control spaceship positioned on opposite sides of the screen, maneuvering them vertically or horizontally to hit a comet-like ball back and forth. The goal is to score points by getting the ball past the opponent’s spaceship. The simplicity of the controls makes it accessible, while the increasing ball speed as the rally continues keeps the challenge alive.

## 📚Modes
- **Singleplayer** <br>
  Play against an AI opponent, allowing for relaxed play.
- **Local Multiplayer** <br>
  Enjoy local multiplayer, facing off against your friend.
- **Online Multiplayer** <br>
  Play with other player online by joining a room.

## 🕹️Controls
<table>
  <thead>
    <th>Key Binding</th>
    <th width="70%">Function</th>
  </thead>
  <tbody>
    <tr>
      <td>W, A, S, D</td>
      <td>Basic Player Movement (Singleplayer & Online Multiplayer) & Player 1 Movement (Local Multiplayer)</td>
    </tr>
    <tr>
      <td>Arrow Keys</td>
      <td>Player 2 Movement (Local Multiplayer)</td>
    </tr>
  </tbody>
</table>

## ⚙️Setup
- Go to the release tab located in the right side of this page.
- Download the .zip file from the latest release available.
- Extract the .zip file containing the game.
- Open the .exe file.

<br />

# 📖Documentation
- Made using Unity Editor 2022.3.19f1
- Online Multiplayer system using <a href="https://assetstore.unity.com/publishers/57771"> Alteruna Multiplayer </a>
- All assets are from itch.io

## 📑Scripts
### **Player**
| **Script Name**    | **Function**                            |
| ------------------ | --------------------------------------- |
| `PlayerControl.cs` | Manage the player movement using WASD or Arrow Keys |
| `PlayerControlNetwork.cs` | Manage the player movement in Online Multiplayer mode |
| `Bot.cs` | The AI bot for the other spaceship in Singleplayer mode |
### **Ball**
| **Script Name**    | **Function**                            |
| ------------------ | --------------------------------------- |
| `BallControl.cs` | Manage the ball system for adding the initial force on start and resetting the position |
| `BallControlNetwork.cs` | The same like `BallControl.cs` but made for Online Multiplayer mode |
### **Other System**
| **Script Name**    | **Function**                            |
| ------------------ | --------------------------------------- |
| `GameManager.cs` | Manage the main system of the game e.g. Scoring, Scene Changing, Score Check |
| `GameManagerNetwork.cs` | The same like `GameManager.cs` with additional customization for Online Multiplayer |
| `LevelLoader.cs` | Manage the transition between scenes |
| `SideWall.cs` | Responsible when the ball get past the player side |
| `etc` | |
