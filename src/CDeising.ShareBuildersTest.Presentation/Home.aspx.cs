using CDeising.ShareBuildersTest.Application.Affiliations.Queries.GetAffiliationForStation;
using CDeising.ShareBuildersTest.Application.Markets.Queries.GetMarket;
using CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation;
using CDeising.ShareBuildersTest.Application.Stations.Queries.GetStations;
using CDeising.ShareBuildersTest.Application.Users.GetUser;
using CDeising.ShareBuildersTest.Application.Users.GetUsers;
using CDeising.ShareBuildersTest.Presentation.ViewModels;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDeising.ShareBuildersTest.Presentation
{
    public partial class Home : Page
    {
        readonly ISender mediatrSender;

        readonly string viewStateSelectedStationKey = "SelectedStation";

        public Home()
        {
            // Web forms is unable to use constructor DI directly.
            mediatrSender = Global.ServiceProvider.GetService<ISender>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Page_Load is not able to be async.
                RegisterAsyncTask(new PageAsyncTask(Page_LoadAsync));
            }
        }

        protected async Task Page_LoadAsync()
        {
            if (!IsPostBack)
            {
                // Retrieve all data and populate controls.
                await PopulateDDLStationsAsync();
                await PopulateStationDetailsAsync();
            }
        }

        /// <summary>
        /// Get all Stations from DB and bind them to DDL.
        /// </summary>
        /// <returns></returns>
        protected async Task PopulateDDLStationsAsync()
        {
            DDLStations.DataSource = await mediatrSender.Send(new GetStationsQuery());
            DDLStations.DataTextField = nameof(StationBriefDto.CallSign);
            DDLStations.DataValueField = nameof(StationBriefDto.Id);
            DDLStations.DataBind();

            // Insert an empty option and set it as default.
            // This forces the user to select an option.
            DDLStations.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            DDLStations.SelectedIndex = 0;

        }

        /// <summary>
        /// Triggers when the Stations Dropdownlist has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async void DDLStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlStations = (DropDownList)sender;

            Guid.TryParse(ddlStations.SelectedValue, out var id);

            if (id != Guid.Empty)
            {
                await PopulateStationDetailsAsync(id);
                await PopulateGVUsersAsync();

                PanelSelectedStation.Visible = true;
                PanelNotSelectedStation.Visible = false;
            }
            else
            {
                PanelSelectedStation.Visible = false;
                PanelNotSelectedStation.Visible = true;
            }
        }

        /// <summary>
        /// Gets all Users for a Station and binds them to GridView.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        protected async Task PopulateGVUsersAsync()
        {
            var stationId = (ViewState[viewStateSelectedStationKey] as StationDetailVm)?.Station.Id;

            if (stationId != null && stationId != Guid.Empty)
            {
                GVUsers.DataSource = await mediatrSender.Send(new GetUsersQuery() { StationId = stationId.Value });
                GVUsers.DataBind();
            }
        }

        /// <summary>
        /// Get all details for the selected Station.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        protected async Task PopulateStationDetailsAsync(Guid? stationId = null)
        {
            StationDetailVm stationDetailVm = null;

            if (stationId == null)
            {
                stationDetailVm = ViewState[viewStateSelectedStationKey] as StationDetailVm;
            }

            if (stationDetailVm == null && stationId != null && stationId != Guid.Empty)
            {
                stationDetailVm = new StationDetailVm
                {
                    Station =  await mediatrSender.Send(new GetStationQuery() { StationId = stationId.Value })
                }; 
            }

            if (stationDetailVm != null)
            {
                var marketTask = mediatrSender.Send(new GetMarketQuery() { Id = stationDetailVm.Station.MarketId });
                var affiliationsTask = mediatrSender.Send(new GetAffiliationForStationQuery() { StationId = stationDetailVm.Station.Id });

                await Task.WhenAll(marketTask, affiliationsTask);

                stationDetailVm.Market = marketTask.Result;
                stationDetailVm.Affiliations.AddRange(affiliationsTask.Result);

                ViewState[viewStateSelectedStationKey] = stationDetailVm;

                LBLStationsCallSign.Text = stationDetailVm.Station.CallSign;
                LBLStationsType.Text = stationDetailVm.Station.Type.ToString();
                LBLMarketName.Text = stationDetailVm.Market.Name;
                RepeaterAffiliations.DataSource = stationDetailVm.Affiliations;
                RepeaterAffiliations.DataBind();
            }
        }

        protected async void GVUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid.TryParse(e.CommandArgument.ToString(), out var userId);

            if (userId != Guid.Empty)
            {
                var selectedUser = await mediatrSender.Send(new GetUserQuery() { UserId = userId });

                StationDetailVm stationDetailVm = ViewState[viewStateSelectedStationKey] as StationDetailVm;

                if (e.CommandName.Equals("edituser", StringComparison.OrdinalIgnoreCase))
                {
                    UpdateUserForm.User = selectedUser;
                    UpdateUserForm.Station = stationDetailVm.Station;
                    UpdateUserModal.ModalTitle = "Edit User";
                    UpdateUserModal.OpenModal();


                }
                else if (e.CommandName.Equals("deleteuser", StringComparison.OrdinalIgnoreCase))
                {
                    DeleteUserForm.User = selectedUser;
                    DeleteUserModal.ModalTitle = "Delete User";
                    DeleteUserModal.OpenModal();
                }
            }
        }

        // TODO: These event handlers can probably be combined and simplified.

        protected void AddUserBtn_Click(object sender, EventArgs e)
        {
            AddUserModal.ModalTitle = "Add User";
            AddUserForm.Station = (ViewState[viewStateSelectedStationKey] as StationDetailVm)?.Station;
            AddUserModal.OpenModal();
        }

        protected async void AddUserModal_SaveBtnClicked(object sender, EventArgs e)
        {
            await AddUserForm.SubmitAsync();
        }

        protected async void UpdateUserModal_SaveBtnClicked(object sender, EventArgs e)
        {
            await UpdateUserForm.SubmitAsync();
        }

        protected async void DeleteUserModal_SaveBtnClicked(object sender, EventArgs e)
        {
            await DeleteUserForm.SubmitAsync();
        }

        protected async void AddUserForm_FormSubmitted(object sender, EventArgs e)
        {
            await PopulateGVUsersAsync();
            AddUserModal.CloseModal();
        }

        protected async void UpdateUserForm_FormSubmitted(object sender, EventArgs e)
        {
            await PopulateGVUsersAsync();
            UpdateUserModal.CloseModal();
        }

        protected async void DeleteUserForm_FormSubmitted(object sender, EventArgs e)
        {
            await PopulateGVUsersAsync();
            DeleteUserModal.CloseModal();
        }
    }
}