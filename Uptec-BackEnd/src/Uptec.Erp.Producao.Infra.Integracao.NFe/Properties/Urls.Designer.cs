﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Urls {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Urls() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Uptec.Erp.Producao.Infra.Integracao.NFe.Properties.Urls", typeof(Urls).Assembly);
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
        ///   Looks up a localized string similar to /v2/nfe/REFERENCIA.
        /// </summary>
        internal static string UrlCancelamentoNota {
            get {
                return ResourceManager.GetString("UrlCancelamentoNota", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /v2/nfe/REFERENCIA/carta_correcao.
        /// </summary>
        internal static string UrlCartaCorrecaoNota {
            get {
                return ResourceManager.GetString("UrlCartaCorrecaoNota", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /v2/nfe/REFERENCIA.
        /// </summary>
        internal static string UrlConsultaNota {
            get {
                return ResourceManager.GetString("UrlConsultaNota", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /v2/nfe/REFERENCIA/email.
        /// </summary>
        internal static string UrlEmail {
            get {
                return ResourceManager.GetString("UrlEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /v2/nfe?ref=REFERENCIA.
        /// </summary>
        internal static string UrlEnvioNota {
            get {
                return ResourceManager.GetString("UrlEnvioNota", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /v2/nfe/inutilizacao.
        /// </summary>
        internal static string UrlInutilizacao {
            get {
                return ResourceManager.GetString("UrlInutilizacao", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://homologacao.focusnfe.com.br.
        /// </summary>
        internal static string UrlRaizHomologacao {
            get {
                return ResourceManager.GetString("UrlRaizHomologacao", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.focusnfe.com.br.
        /// </summary>
        internal static string UrlRaizProducao {
            get {
                return ResourceManager.GetString("UrlRaizProducao", resourceCulture);
            }
        }
    }
}