# DreamWalker

A therapeutic interactive experience designed to help people with PTSD through immersive dream exploration.

## 🌙 Project Overview

DreamWalker is a third-person narrative game where players guide a character who awakens at night and walks through dreamscapes. The experience is designed as a therapeutic tool to help individuals with PTSD process trauma in a safe, controlled virtual environment.

### Key Features
- **Third-person perspective** for emotional safety and distance
- **Dream-like environments** with surreal, atmospheric visuals
- **Therapeutic gameplay mechanics** designed for PTSD treatment
- **Immersive audio-visual experience** with advanced lighting and shadows

## 🎮 Technical Specifications

- **Engine:** Unreal Engine 5
- **Platform:** Desktop (Mac/PC)
- **Development:** Blueprint Visual Scripting
- **Graphics:** Shader Model 5 with enhanced lighting
- **Version Control:** Git with Git LFS

## 🚀 Getting Started

### Prerequisites
- Unreal Engine 5 (same version as team)
- Git with Git LFS
- macOS Monterey 12.0+ or Windows 10+
- 8GB RAM minimum (16GB recommended)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/MarinaPollak/DreamWalker.git
   cd DreamWalker
   ```

2. **Install Git LFS (if not already installed):**
   ```bash
   # Mac:
   brew install git-lfs
   
   # Initialize and pull large files:
   git lfs install
   git lfs pull
   ```

3. **Generate project files:**
   - Right-click on `DreamWalker.uproject`
   - Select "Generate Visual Studio project files"
   
4. **Open the project:**
   - Double-click `DreamWalker.uproject`
   - Unreal Engine will launch the project

## 🏗️ Project Structure

```
DreamWalker/
├── Content/              # Game assets, blueprints, materials
├── Config/               # Project configuration files
├── DreamWalker.uproject  # Main project file
├── .gitignore           # Git ignore rules
├── .gitattributes       # Git LFS configuration
└── README.md            # This file
```

## 👥 Team Collaboration

### Workflow
1. **Pull latest changes** before starting work: `git pull`
2. **Create feature branches** for major changes: `git checkout -b feature-name`
3. **Commit frequently** with descriptive messages
4. **Push changes** regularly: `git push`
5. **Communicate** with team about major asset changes

### Best Practices
- **Always pull before pushing** to avoid conflicts
- **Use descriptive commit messages** (e.g., "Add dreamy movement mechanics")
- **Don't commit generated files** (already handled by .gitignore)
- **Test builds** before pushing major changes
- **Document new features** in this README

## 🎯 Development Guidelines

### Therapeutic Considerations
- Keep content **non-triggering** and appropriate for PTSD therapy
- Focus on **calming, peaceful** interactions
- Avoid **sudden movements** or **jarring audio**
- Design for **emotional safety** and **player agency**

### Technical Guidelines
- Use **Blueprint** for all game logic (accessible to all team members)
- Keep **performance optimized** for therapeutic use
- Maintain **consistent visual style** across dream sequences
- **Test frequently** on target platforms

## 🛠️ Troubleshooting

### Common Issues
- **Large files not downloading:** Run `git lfs pull`
- **Project won't open:** Regenerate project files
- **Performance issues:** Check graphics settings in Project Settings
- **Merge conflicts:** Communicate with team before major changes

### Getting Help
- Check Unreal Engine documentation
- Ask team members in project discussions
- Create GitHub issues for bugs or questions

## 📚 Resources

- [Unreal Engine Documentation](https://docs.unrealengine.com/)
- [Blueprint Visual Scripting](https://docs.unrealengine.com/5.0/en-US/blueprints-visual-scripting-in-unreal-engine/)
- [Git LFS Documentation](https://git-lfs.github.io/)
- [PTSD Treatment Guidelines](https://www.ptsd.va.gov/) (for reference)

## 🤝 Contributing

1. **Fork** the project (if external contributor)
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

## 📝 License

This project is developed for educational purposes as part of a class project. All rights reserved by the development team.

## 👩‍💻 Team Members

- **Marina Pollak** - Project Lead & Repository Owner
- *Add team members as they join*

## 🎖️ Acknowledgments

- Thanks to our instructor for guidance on therapeutic game design
- Unreal Engine community for resources and support
- PTSD research community for insights into therapeutic applications

---

**🌟 Remember:** This project aims to help people heal. Let's build something beautiful and meaningful together.
