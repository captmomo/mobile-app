﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LH.Forcas.Localization {
    using System;
    using System.Reflection;
    
    
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
    internal class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LH.Forcas.Localization.AppResources", typeof(AppResources).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to New Account.
        /// </summary>
        internal static string AccountsDetailPage_Title_New {
            get {
                return ResourceManager.GetString("AccountsDetailPage_Title_New", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you really want to delete the account {0} and all related transactions?.
        /// </summary>
        internal static string AccountsListPage_DeleteAccountConfirmMsgFormat {
            get {
                return ResourceManager.GetString("AccountsListPage_DeleteAccountConfirmMsgFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete account?.
        /// </summary>
        internal static string AccountsListPage_DeleteAccountConfirmTitle {
            get {
                return ResourceManager.GetString("AccountsListPage_DeleteAccountConfirmTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to We&apos;re really sorry, but an error occured while deleting the account. If this happens again, please report it at our website..
        /// </summary>
        internal static string AccountsListPage_DeleteAccountError {
            get {
                return ResourceManager.GetString("AccountsListPage_DeleteAccountError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Accounts.
        /// </summary>
        internal static string AccountsListPage_Title {
            get {
                return ResourceManager.GetString("AccountsListPage_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking.
        /// </summary>
        internal static string AccountTypeEnum_Checking {
            get {
                return ResourceManager.GetString("AccountTypeEnum_Checking", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string AlertDialog_ErrorTitle {
            get {
                return ResourceManager.GetString("AlertDialog_ErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OK.
        /// </summary>
        internal static string AlertDialog_OK {
            get {
                return ResourceManager.GetString("AlertDialog_OK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No.
        /// </summary>
        internal static string ConfirmDialog_No {
            get {
                return ResourceManager.GetString("ConfirmDialog_No", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yes.
        /// </summary>
        internal static string ConfirmDialog_Yes {
            get {
                return ResourceManager.GetString("ConfirmDialog_Yes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Overview.
        /// </summary>
        internal static string Dashboard_Title {
            get {
                return ResourceManager.GetString("Dashboard_Title", resourceCulture);
            }
        }
    }
}
