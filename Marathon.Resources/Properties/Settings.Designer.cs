﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Marathon.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Maximized")]
        public global::System.Windows.Forms.FormWindowState Workspace_WindowState {
            get {
                return ((global::System.Windows.Forms.FormWindowState)(this["Workspace_WindowState"]));
            }
            set {
                this["Workspace_WindowState"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1280")]
        public int Workspace_WindowWidth {
            get {
                return ((int)(this["Workspace_WindowWidth"]));
            }
            set {
                this["Workspace_WindowWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("720")]
        public int Workspace_WindowHeight {
            get {
                return ((int)(this["Workspace_WindowHeight"]));
            }
            set {
                this["Workspace_WindowHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection Workspace_RecentDocuments {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Workspace_RecentDocuments"]));
            }
            set {
                this["Workspace_RecentDocuments"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Modding_GameExecutable {
            get {
                return ((string)(this["Modding_GameExecutable"]));
            }
            set {
                this["Modding_GameExecutable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Modding_ManagerDetailsVisible {
            get {
                return ((bool)(this["Modding_ManagerDetailsVisible"]));
            }
            set {
                this["Modding_ManagerDetailsVisible"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Modding_ModsDirectory {
            get {
                return ((string)(this["Modding_ModsDirectory"]));
            }
            set {
                this["Modding_ModsDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Workspace_RibbonCollapsed {
            get {
                return ((bool)(this["Workspace_RibbonCollapsed"]));
            }
            set {
                this["Workspace_RibbonCollapsed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Output_ToggleWordWrap {
            get {
                return ((bool)(this["Output_ToggleWordWrap"]));
            }
            set {
                this["Output_ToggleWordWrap"] = value;
            }
        }
    }
}
