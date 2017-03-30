using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace MakiseSharp.Models
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    public class RepositoryModel
    {
        [JsonProperty]
        public readonly ulong id;
        [JsonProperty]
        public readonly string name;
        [JsonProperty]
        public readonly string owner_name;
        [JsonProperty]
        public Uri url;
    }
}