﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapWindow.Data.Img {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ImgResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ImgResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MapWindow.Data.Img.ImgResources", typeof(ImgResources).Assembly);
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
        ///   Looks up a localized string similar to Attepted to set the enumeration field to the value &apos;%s&apos;, which is not known..
        /// </summary>
        internal static string HfaEnumerationNotFound {
            get {
                return ResourceManager.GetString("HfaEnumerationNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified charcter &apos;%S&apos; was not a known field type: 124cCesStlLfdmMbox&quot; .
        /// </summary>
        internal static string HfaFieldTypeException {
            get {
                return ResourceManager.GetString("HfaFieldTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The number of rows and number of columns must both be greater than 0, but the header for this item returned %S1 rows and %S2 columns..
        /// </summary>
        internal static string HfaInvalidCountException {
            get {
                return ResourceManager.GetString("HfaInvalidCountException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must first compress the data before accessing this value..
        /// </summary>
        internal static string HfaNotCompressedException {
            get {
                return ResourceManager.GetString("HfaNotCompressedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Insertion via SetInstValue() is not yet supported for pointers..
        /// </summary>
        internal static string HfaPointerInsertNotSupportedException {
            get {
                return ResourceManager.GetString("HfaPointerInsertNotSupportedException", resourceCulture);
            }
        }
    }
}
