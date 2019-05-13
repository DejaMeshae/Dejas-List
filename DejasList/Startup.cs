using DejasList.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DejasList.Startup))]
namespace DejasList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("Admin"))
            {
                //create Admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Admin who will maintain the website
                var user = new ApplicationUser();
                //user.UserName = "Jane";  //not using username, email address is the username
                user.Email = "janedoe@gmail.com";

                string userPWD = "Jane@123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Admin Role
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            //Creating client role
            if (!roleManager.RoleExists("Client"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
            }

            //creating contractor role
            if (!roleManager.RoleExists("Contractor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Contractor";
                roleManager.Create(role);
            }
        }
    }
}


