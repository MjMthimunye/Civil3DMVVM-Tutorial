# Jacobian.Civil3D - MVVM Civil 3D Plugin Tutorial

Welcome to the **Jacobian.Civil3D** project! This is a comprehensive tutorial demonstrating how to build a **Civil 3D Plugin using the MVVM (Model-View-ViewModel) design pattern**.

## 📺 About This Tutorial

This project is designed as a **YouTube video series** that walks you through:
- Building a professional Civil 3D plugin from scratch
- Implementing the MVVM pattern in a Revit-like plugin architecture
- Creating responsive WPF user interfaces integrated with Civil 3D
- Best practices for plugin development

---

## 🎯 Project Overview

**Jacobian.Civil3D** is a sample plugin that demonstrates the MVVM architecture in a Civil 3D context. The project showcases how to separate concerns and create maintainable, testable plugin code.

### Key Features
✅ **MVVM Architecture** - Clean separation of Model, View, and ViewModel  
✅ **WPF UI** - Modern user interface using Windows Presentation Foundation  
✅ **Civil 3D Integration** - Seamless interaction with Civil 3D APIs  
✅ **Multi-Version Support** - Compatible with Civil 3D 2023, 2024, and 2025  
✅ **Ribbon Commands** - Custom ribbon buttons and commands  

---

## 📁 Project Structure

```
Jacobian.Civil3D/
├── Application.cs                    # Plugin application entry point
├── Tools/
│   └── Civil3DMVVM/
│       ├── Civil3DMVVMCommand.cs      # Command handler (Controller/Command Pattern)
│       ├── Model/
│       │   └── Civil3DMVVMModel.cs    # Data model and business logic
│       ├── ViewModel/
│       │   └── Civil3DMVVMViewModel.cs # View logic and state management
│       └── View/
│           ├── Civil3DMVVMView.xaml   # WPF UI (XAML)
│           ├── Civil3DMVVMView.xaml.cs # UI code-behind
│           └── Styles.xaml            # UI styling
├── Utilities/
│   └── RibbonUtils.cs                # Ribbon UI utilities
├── Resources/
│   ├── Images/                       # UI images and assets
│   └── IconImages/                   # Icon assets
└── Manifest/
	└── PackageContents.xml           # Civil 3D plugin manifest
```

---

## 🏗️ MVVM Architecture Explained

### **Model** (`Civil3DMVVMModel.cs`)
- Contains business logic and data
- Independent of UI framework
- Handles Civil 3D object manipulation
- Example: Reading/writing Civil 3D entities

### **ViewModel** (`Civil3DMVVMViewModel.cs`)
- Mediates between Model and View
- Exposes data and commands to the View
- Handles user interaction logic
- Implements `INotifyPropertyChanged` for data binding
- Example: Format and prepare data for UI display

### **View** (`Civil3DMVVMView.xaml` + `Civil3DMVVMView.xaml.cs`)
- WPF XAML UI definition
- Binds to ViewModel properties
- No direct business logic
- Example: TextBoxes, Buttons, Lists bound to ViewModel

### **Command** (`Civil3DMVVMCommand.cs`)
- Entry point for Civil 3D ribbon commands
- Instantiates and displays the View
- Bridges Civil 3D application commands with your plugin

---

## 🔧 Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | .NET | 8.0 (with .NET Framework 4.8 fallback) |
| **UI Framework** | WPF | Latest (Windows Presentation Foundation) |
| **Plugin Platform** | Civil 3D | 2023, 2024, 2025 |
| **Language** | C# | Latest features (Nullable, Implicit Usings) |
| **Architecture** | MVVM | Design Pattern |

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022 or later (Community Edition is fine)
- Civil 3D 2023, 2024, or 2025 installed
- .NET 8 SDK or .NET Framework 4.8
- Basic understanding of C# and XAML

### Building the Project

1. **Open the Solution**
   ```powershell
   # Navigate to the project directory
   cd "C:\path\to\Jacobian.Civil3D"

   # Open in Visual Studio
   start Jacobian.Civil3D.slnx
   ```

2. **Select Configuration**
   - Choose your Civil 3D version:
	 - `DebugV23` / `ReleaseV23` for Civil 3D 2023
	 - `DebugV24` / `ReleaseV24` for Civil 3D 2024
	 - `DebugV25` / `ReleaseV25` for Civil 3D 2025

3. **Build the Solution**
   ```
   Build > Build Solution (Ctrl+Shift+B)
   ```

4. **Debug or Deploy**
   - For debugging: Set Civil 3D as the startup application
   - For deployment: Package and place in Civil 3D AddIns folder

---

## 📚 Tutorial Flow

### **Part 1: Project Setup**
- Creating a new Civil 3D plugin project
- Configuring multi-version support
- Setting up project structure


---

## 📖 Resources

- [Civil 3D API Documentation](https://civapidocs.com/)
- [WPF Data Binding Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-overview)
- [MVVM Pattern Guide](https://www.telerik.com/blogs/mvvm-pattern-guide)
- [C# Design Patterns](https://refactoring.guru/design-patterns/csharp)

---

## 🎥 YouTube Channel

Subscribe to see the complete video series breaking down each component step-by-step!

---

## 📄 License

This project is provided as a learning resource for Civil 3D plugin development.

---

## 💬 Questions & Feedback

Encountered issues or have suggestions? Follow along with the YouTube video series for detailed explanations and troubleshooting tips!

---

**Happy Coding! 🚀**

*Last Updated: 2026*
