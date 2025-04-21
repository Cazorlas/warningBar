🔔 WarningBar for Revit
A lightweight WPF-based notification bar for Autodesk Revit add-ins.  

---

## 💡 About
This tool was built as part of my Revit plugin development workflow.  
Special thanks to **ChatGPT** for assisting with the WPF and interop implementation.

---

## 📷 Preview 
![image](https://github.com/user-attachments/assets/e450d4f2-f4b8-4baa-807b-b9b3e36cfebc)

---

## 🧩 Usage
```csharp
using (WarningBar warningBar = new WarningBar("Your title"))
{
    warningBar.Show();

    // Logic code

    warningBar.Dispose();
}
