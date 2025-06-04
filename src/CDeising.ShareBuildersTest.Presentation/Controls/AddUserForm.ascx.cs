using CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation;
using CDeising.ShareBuildersTest.Application.Users.CreateUser;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDeising.ShareBuildersTest.Presentation.Controls
{
    /// <summary>
    /// A "form" to add users.
    /// </summary>
    public partial class AddUserForm : UserControl
    {
        readonly ISender mediatrSender;

        /// <summary>
        /// The selected Station.
        /// Users are created in relation to a Station.
        /// </summary>
        public StationDto Station { get; set; }

        /// <summary>
        /// Triggered when the "Form" has been submitted successfully.
        /// </summary>
        public event EventHandler FormSubmitted;

        public AddUserForm()
        {
            // User Controls do not support true constructor DI.
            mediatrSender = Global.ServiceProvider.GetService<ISender>();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Station != null)
                {
                    ResetFormFields();
                    ClearValidation();
                    TxtStationId.Text = Station?.Id.ToString();
                }
            }
        }

        /// <summary>
        /// Hides the validation errors.
        /// </summary>
        protected void ClearValidation()
        {
            TXTUserFirstNameValidation.Visible = false;
            TXTUserLastNameValidation.Visible = false;
        }

        /// <summary>
        /// Resets form fields back to default.
        /// </summary>
        protected void ResetFormFields()
        {
            TxtUserFirstName.Text = string.Empty;
            TxtUserLastName.Text = string.Empty;
        }

        /// <summary>
        /// Submits the "Form".
        /// When the "Form" has been submitted successfully, an event will be raised.
        /// </summary>
        /// <returns></returns>
        public async Task SubmitAsync()
        {
            ClearValidation();

            Guid.TryParse(TxtStationId.Text, out var stationId);

            if (stationId != Guid.Empty && stationId != null)
            {
                try
                {
                    await mediatrSender?.Send(new CreateUserCommand()
                    {
                        StationId = stationId,
                        FirstName = TxtUserFirstName.Text,
                        LastName = TxtUserLastName.Text
                    });

                    ResetFormFields();

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