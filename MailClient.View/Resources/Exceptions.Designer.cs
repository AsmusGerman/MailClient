﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MailClient.View.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Exceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MailClient.View.Resources.Exceptions", typeof(Exceptions).Assembly);
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
        ///   Looks up a localized string similar to No fue posible acceder a la cuenta de correo. Verifique que los campos ingresados sean correctos e intente nuevamente.Si el problema persiste, contacte con soporte técnico..
        /// </summary>
        internal static string LoginException {
            get {
                return ResourceManager.GetString("LoginException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No fue posible registrar la cuenta de correo. Verifique que los campos ingresados sean correctos e intente nuevamente. Si el problema persiste, contacte con soporte técnico..
        /// </summary>
        internal static string RegisterException {
            get {
                return ResourceManager.GetString("RegisterException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No fue posible enviar el correo. Verifique que haya conexión a internet. Si el problema persiste, contacte con soporte técnico..
        /// </summary>
        internal static string SendException {
            get {
                return ResourceManager.GetString("SendException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No fue posible actualizar la casilla de correo. Verifique que haya conexión a internet. Si el problema persiste, contacte con soporte técnico..
        /// </summary>
        internal static string UpdateInboxException {
            get {
                return ResourceManager.GetString("UpdateInboxException", resourceCulture);
            }
        }
    }
}
