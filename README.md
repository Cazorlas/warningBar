🔔 WarningBar for Revit
A lightweight WPF-based notification bar for Autodesk Revit add-ins.  
Useful for showing short messages like warnings or status updates in a non-intrusive way.
---
## 💡 About
This tool was built as part of my Revit plugin development workflow.  
Special thanks to **ChatGPT** for assisting with the WPF and interop implementation.

---
## 📷 Preview
![WarningBar Preview]  
![image](https://github.com/user-attachments/assets/69822eef-8c40-4ef9-8c89-5777f2fcdaab)
---

## 🧩 Usage
```csharp
using (WarningBar warningBar = new WarningBar("Your title"))
{
    warningBar.Show();

// Logic code

    warningBar.Dispose();
}
