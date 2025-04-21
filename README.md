🔔 WarningBar for Revit
A lightweight WPF-based notification bar for Autodesk Revit add-ins.  
---
## 💡 About
This tool was built as part of my Revit plugin development workflow.  
Special thanks to **ChatGPT** for assisting with the WPF and interop implementation.

---
## 📷 Preview 
(https://github.com/user-attachments/assets/69822eef-8c40-4ef9-8c89-5777f2fcdaab)
---

## 🧩 Usage
```csharp
using (WarningBar warningBar = new WarningBar("Your title"))
{
    warningBar.Show();

// Logic code

    warningBar.Dispose();
}
