using System.Security.Principal;

namespace Bloggie.Models.ViewModels
{
    public class EditTagRequest
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
    }
}
