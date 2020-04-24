using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class NewCommentViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        [System.ComponentModel.DisplayName("Name")]
        public string Username { get; set; }
        [Required]
        public int PostId { get; set; }
    }
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public NewCommentViewModel NewComment { get; set; }
    }
}
