![image](https://github.com/user-attachments/assets/9f39a34a-1ef6-44d4-a97d-fcfd7ba50375)


# Avalanche

Avalanche is a 2D roguelike game where the player takes on the role of a climber trapped in underground icy caves populated by animated skeletons and mythical creatures. The game includes elements of survival, exploration, and combat.

## Requirements

To run this project, you need to have the .NET SDK installed. Ensure that you have .NET version 7.0 or higher. To check your version, run:

```bash
dotnet --version
```

The graphical version of the game requires **SFML.Net** and its dependencies.

## Installation and Running

1. Clone the repository:
```bash
git clone https://github.com/Leucist/avalanche-game.git
cd avalanche-game
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Install required dependencies:

For **Windows**, ensure you have the necessary SFML dependencies installed. You can download the required DLLs from [SFML's official website](https://www.sfml-dev.org/download.php) and place them in the game directory.

For **Linux**, install the necessary libraries:
```bash
sudo pacman -S csfml openal flac
```
On Debian-based distributions (e.g., Ubuntu):
```bash
sudo apt install libcsfml-audio2.6 libopenal1 libflac8
```

4. Run the game.

 **Graphical** version:
```bash
dotnet run --project Avalanche.Launcher
```

**Console** version:
```bash
dotnet run --project Avalanche.Launcher -- console
```

### Alternative Launch Methods
You can also start the graphical version by double-clicking the corresponding launcher script:

- **Linux:** `Linux Launchers/run_graphics.sh`
- **Windows:** `Windows Launchers/run_graphics.bat`
  


## Project Structure

```
Avalanche/
├── Avalanche.sln                # Solution file that combines all projects
├── Avalanche.Core/              # Core logic and game entities
├── Avalanche.Console/           # Console interface
├── Avalanche.Graphics/          # Graphical interface
├── Avalanche.Launcher/          # Launcher for selecting the interface
└── .gitignore                   # Ignored files and folders
```

## Development Branches

- `main`: Stable version of the project.
- `dev`: Main development branch.
- `feature/*`: Branches for new features.
- `fix/*`: Branches for bug fixes.

## Contributing

To contribute to the project:

1. **Read the Coding Conventions**: Familiarize yourself with our coding standards in [CodingConventions.md](CodingConventions.md).
2. **Create a Branch**: For any feature or fix, create a separate branch from `main`. Use descriptive names for branches.
3. **Implement Changes**: Make sure your code adheres to the conventions outlined in `CodingConventions.md`.
4. **Run Tests**: Ensure all tests pass before submitting your changes.
5. **Create a Pull Request**: When ready, submit a pull request. Include a description of your changes and any relevant issues.


## Pull Request Checklist

- [ ] Code adheres to coding conventions
- [ ] All tests pass
- [ ] Relevant documentation updated


## Credits

### Music
The game features music licensed under **CC BY 4.0**. Special thanks to:

- "**Ice Cave**" by **Visager** – [Free Music Archive](https://freemusicarchive.org/) ([CC BY 4.0](https://creativecommons.org/licenses/by/4.0/))

The music has been used with proper attribution according to the license terms.


## License

This project is licensed under the MIT License.
