﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPageViewer {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyPageViewer.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 文件夹下存在索引的文档！.
        /// </summary>
        internal static string TextDocumentInFolder {
            get {
                return ResourceManager.GetString("TextDocumentInFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 错误.
        /// </summary>
        internal static string TextError {
            get {
                return ResourceManager.GetString("TextError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 不能重命名根节点!.
        /// </summary>
        internal static string TextErrorRenameRoot {
            get {
                return ResourceManager.GetString("TextErrorRenameRoot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 不正确的配置!.
        /// </summary>
        internal static string TextErrorSettings {
            get {
                return ResourceManager.GetString("TextErrorSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 文件夹下存在文件！.
        /// </summary>
        internal static string TextFileInFolder {
            get {
                return ResourceManager.GetString("TextFileInFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 提示.
        /// </summary>
        internal static string TextHint {
            get {
                return ResourceManager.GetString("TextHint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 不合法的路径名称!.
        /// </summary>
        internal static string TextIllegalPathName {
            get {
                return ResourceManager.GetString("TextIllegalPathName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 父节点：{0}.
        /// </summary>
        internal static string TextParentNodeName {
            get {
                return ResourceManager.GetString("TextParentNodeName", resourceCulture);
            }
        }
    }
}
