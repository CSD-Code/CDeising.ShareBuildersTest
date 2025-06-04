using CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation;
using CDeising.ShareBuildersTest.Application.Users.GetUsers;
using CDeising.ShareBuildersTest.Application.Users.UpdateUser;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDeising.ShareBuildersTest.Presentation.Controls
{
    public partial class UpdateUserForm : UserControl
    {
        readonly ISender mediatrSender;

        /// <summary>
        /// The User that was selected, if applicable.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// The selected Station.
        /// Users are created in relation to a Station.
        /// </summary>
        public StationDto Station { get; set; }

        /// <summary>
        /// Triggered when the "Form" has been submitted successfully.
        /// </summary>
        public event EventHandler FormSubmitted;

        public UpdateUserForm()
        {
            // User Controls do not support true constructor DI.
            mediatrSender = Global.ServiceProvider.GetService<ISender>();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
                if (User != null)
                {
                    TxtUserFirstName.Text = User?.FirstName;
                    TxtUserLastName.Text = User?.LastName;
                    TxtUserId.Text = User?.Id.ToString();
                }

                if (Station != null)
                {
                    TxtStationId.Text = Station?.Id.ToString();
                }

                if (User != null || Station != null)
                {
                    ClearValidation();
                }
            }
        }

        /// <summary>
        /// Sets form fields back to default.
        /// </summary>
        protected void ClearValidation()
        {
            TXTUserFirstNameValidation.Visible = false;
            TXTUserLastNameValidation.Visible = false;
        }

        /// <summary>
        /// Submits the "Form".
        /// When the "Form" has been submitted successfully, an event will be raised.
        /// </summary>
        /// <returns></returns>
        public async Task SubmitAsync()
        {
            ClearValidation();

            Guid.TryParse(TxtUserId.Text, out var userId);
            Guid.TryParse(TxtStationId.Text, out var stationId);

            if (stationId != Guid.Empty && stationId != null)
            {
                try
                {
                    await mediatrSender.Send(new UpdateUserCommand()
                    {
                        Id = userId,
                        FirstName = TxtUserFirstName.Text,
                        LastName = TxtUserLastName.Text,
                        StationId = stationId
                    });

                    FormSubmitted?.Invoke(this, EventArgs.Empty);
                }
                catch (FluentValidation.ValidationException ex)
                {
                    // TODO: This logic should be shared between the Create and Update forms.
                    // Write a helper class or an extension method.

                    foreach (var error in ex.Errors)
                    {
                        var control = (Label)FindControl($"TXTUser{error.PropertyName}Validation");

                        if (control != null)
                        {
                            control.Text = error.ErrorMessage;
                            control.Visible = true;
                        }
                    }
                }

            }
        }
    }
}