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

### **Part 2: MVVM Fundamentals**
- Understanding Model-View-ViewModel pattern
- Implementing `INotifyPropertyChanged`
- Data binding in WPF

### **Part 3: Building the Model**
- Interacting with Civil 3D APIs
- Implementing business logic
- Data access and manipulation

### **Part 4: Creating the ViewModel**
- Property binding setup
- ICommand implementation
- Event handling

### **Part 5: Designing the View**
- XAML UI basics
- Data binding syntax
- Styling and resources

### **Part 6: Ribbon Integration**
- Adding commands to Civil 3D ribbon
- Handling command execution
- UI updates from plugin

### **Part 7: Deployment & Publishing**
- Packaging the plugin
- Distribution methods
- Version management

---

## 💡 Key Concepts Covered

### Data Binding
```csharp
// ViewModel exposes properties
public string ProjectName
{
	get => _projectName;
	set
	{
		if (_projectName != value)
		{
			_projectName = value;
			OnPropertyChanged(nameof(ProjectName));
		}
	}
}
```

### Commands
```xaml
<!-- XAML binding to ViewModel command -->
<Button Command="{Binding RunCommand}" Content="Execute" />
```

### Civil 3D Integration
```csharp
// Accessing Civil 3D from your Model
using Autodesk.Civil.ApplicationServices;
var app = CivilApplication.Current;
var doc = app.ActiveDocument;
```

---

## 🔄 Workflow Example

1. **User clicks button in UI (View)**
2. **Button is bound to ViewModel Command**
3. **Command executes ViewModel method**
4. **ViewModel calls Model method**
5. **Model interacts with Civil 3D**
6. **Results propagated back through ViewModel**
7. **View updates automatically via data binding**

---

## 📦 Configuration & Multi-Version Support

This project supports multiple Civil 3D versions using conditional compilation:

```xml
<PropertyGroup Condition="$(Configuration.Contains('V23'))">
	<TargetFramework>net48</TargetFramework>
	<SoftwareVersion>2023</SoftwareVersion>
	<Civil3DApiVersion>2023.0.0</Civil3DApiVersion>
</PropertyGroup>
```

Switch configurations in Visual Studio to target different Civil 3D versions.

---

## 🎬 What You'll Learn

By following this tutorial series, you'll understand:

✅ How to architect professional Civil 3D plugins  
✅ MVVM pattern implementation in real-world scenarios  
✅ WPF and XAML best practices  
✅ Civil 3D API integration techniques  
✅ Plugin deployment and distribution  
✅ Debugging Civil 3D plugins  

---

## 📝 Code Style & Conventions

- **Naming**: PascalCase for properties/methods, camelCase for local variables
- **MVVM**: Strict separation between M, V, and VM
- **UI**: WPF with XAML markup
- **NETVersion**: .NET 8 with latest C# features enabled

---

## 🐛 Troubleshooting

### Plugin Not Loading?
- Verify manifest path in `PackageContents.xml`
- Check Civil 3D AddIns folder location
- Ensure .NET Framework version matches

### Data Not Binding?
- Verify ViewModel implements `INotifyPropertyChanged`
- Check binding syntax in XAML
- Use Debug output window to catch binding errors

### Civil 3D API Issues?
- Confirm correct API version for your Civil 3D installation
- Review Civil 3D API documentation
- Check for version-specific API changes

---

## 📖 Resources

- [Civil 3D API Documentation](https://help.autodesk.com/view/CIVILDOC/)
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

*Last Updated: 2024*
