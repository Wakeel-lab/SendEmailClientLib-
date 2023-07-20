using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmailService.Model
{
    public class EmailObjectModel
    {
        [Required]
        public IEnumerable<string> TO { get; set; }
        [Required]
        public string FROM { get; set; }
        [Required]
        public string CONTENT { get; set; }
        public string SUBJECT { get; set; }
        public List<IFormFile> ATTACHMENTS { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        [JsonIgnore]
        public string TEMPLATECONFIG { get; set; }
    }
}
