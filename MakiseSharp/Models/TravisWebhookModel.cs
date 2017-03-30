using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MakiseSharp.Models
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed.")]
    public enum Type
    {
        push, pull_request, cron, api
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    public class TravisWebhookModel
    {
        [JsonProperty("number")]
        public readonly ulong Number;
        [JsonProperty("status")]
        public readonly bool Failed;
        [JsonProperty("status_message")]
        public readonly string StatusMessage;
        [JsonProperty]
        public readonly string commit;
        [JsonProperty]
        public readonly string branch;
        [JsonProperty("message")]
        public readonly string commit_message;
        [JsonProperty]
        public readonly Uri compare_url;
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly Type type;
        [JsonProperty]
        public readonly Uri build_url;
        [JsonProperty]
        public readonly string author_name;
        [JsonProperty]
        public readonly string author_email;
        [JsonProperty]
        public readonly bool pull_request;
        [JsonProperty]
        public readonly int? pull_request_number; //TODO ogarnac to
        [JsonProperty]
        public readonly string pull_request_title;
        [JsonProperty]
        public readonly RepositoryModel repository;

        /*  id": 1,
    "number": "1",
    "status": null,
    "started_at": null,
    "finished_at": null,
    "status_message": "Passed",
    "commit": "62aae5f70ceee39123ef",
    "branch": "master",
    "message": "the commit message",
    "compare_url": "https://github.com/svenfuchs/minimal/compare/master...develop",
    "committed_at": "2011-11-11T11: 11: 11Z",
    "committer_name": "Sven Fuchs",
    "committer_email": "svenfuchs@artweb-design.de",
    "author_name": "Sven Fuchs",
    "author_email": "svenfuchs@artweb-design.de",
    "type": "push",
    "build_url": "https://travis-ci.org/svenfuchs/minimal/builds/1",
    "repository": {
      "id": 1,
      "name": "minimal",
      "owner_name": "svenfuchs",
      "url": "http://github.com/svenfuchs/minimal"
     },
    "config": {
      "notifications": {
        "webhooks": ["http://evome.fr/notifications", "http://example.com/"]
      }
  },
    "matrix": [
      {
        "id": 2,
        "repository_id": 1,
        "number": "1.1",
        "state": "created",
        "started_at": null,
        "finished_at": null,
        "config": {
          "notifications": {
            "webhooks": ["http://evome.fr/notifications", "http://example.com/"]
          }
        },
        "status": null,
        "log": "",
        "result": null,
        "parent_id": 1,
        "commit": "62aae5f70ceee39123ef",
        "branch": "master",
        "message": "the commit message",
        "committed_at": "2011-11-11T11: 11: 11Z",
        "committer_name": "Sven Fuchs",
        "committer_email": "svenfuchs@artweb-design.de",
        "author_name": "Sven Fuchs",
        "author_email": "svenfuchs@artweb-design.de",
        "compare_url": "https://github.com/svenfuchs/minimal/compare/master...develop"
      }
    ]
  }*/
    }
}