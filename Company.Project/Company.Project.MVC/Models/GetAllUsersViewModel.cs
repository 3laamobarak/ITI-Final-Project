namespace Company.Project.MVC.Models
{
    public class GetAllUsersViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nid { get; set; }
    }

}
