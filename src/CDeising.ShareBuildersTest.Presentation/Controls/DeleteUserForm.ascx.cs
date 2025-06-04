using CDeising.ShareBuildersTest.Application.Users.DeleteUser;
using CDeising.ShareBuildersTest.Application.Users.GetUsers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace CDeising.ShareBuildersTest.Presentation.Controls
{
    /// <summary>
    /// "Form" to delete users. 
    /// </summary>
    public partial class DeleteUserForm : UserControl
    {
        readonly ISender mediatrSender;

        /// <summary>
        /// The User that was selected, if applicable.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Triggered when the "Form" has been submitted successfully.
        /// </summary>
        public event EventHandler FormSubmitted;

        public DeleteUserForm()
        {
            // User Controls do not support true constructor DI.
            mediatrSender = Global.ServiceProvider.GetService<ISender>();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            TxtUserId.Text = User?.Id.ToString();
            LBLDeleteUser.Text = $"{User?.FirstName}  {User?.LastName}";
        }

        /// <summary>
        /// Submits the "Form".
        /// When the "Form" has been submitted successfully, an event will be raised.
        /// </summary>
        /// <returns></returns>
        public async Task SubmitAsync()
        {
            Guid.TryParse(TxtUserId.Text, out var userId);

            if (userId != Guid.Empty && userId != null)
            {
                await mediatrSender.Send(new DeleteUserCommand()
                {
                    Id = userId
                });
                FormSubmitted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}