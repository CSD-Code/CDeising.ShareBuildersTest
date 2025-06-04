using System;
using System.Web.UI;

namespace CDeising.ShareBuildersTest.Presentation.Controls
{
    /// <summary>
    /// Reusable modal control that functions entirely without JS.
    /// </summary>
    public partial class Modal : UserControl
    {
        public string ModalTitle { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Control ModalContent { get; set; }

        public event EventHandler SaveBtnClicked;

        readonly string viewStateModalTitleKey = "ModalTitle";

        /// <summary>
        /// Opens the modal.
        /// </summary>
        public void OpenModal()
        {
            SetupState();

            AppModalContainer.CssClass += " app-modal-active";
        }

        /// <summary>
        /// Closes the modal.
        /// </summary>
        public void CloseModal()
        {
            HideModal();
        }

        /// <summary>
        /// Closes the modal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CloseModal(object sender, EventArgs e)
        {
            HideModal();
        }

        protected void Page_Load(object sender, EventArgs e)

        {
            if (IsPostBack)
            {
                PH?.Controls.Add(ModalContent);
            }
        }

        /// <summary>
        /// Manages the state of the modal control.
        /// </summary>
        protected void SetupState()
        {
            string modalTitle = ViewState[viewStateModalTitleKey] as string;

            if (string.IsNullOrEmpty(modalTitle) && !string.IsNullOrEmpty(ModalTitle))
            {
                ViewState.Add(viewStateModalTitleKey, ModalTitle);
            }

            LBLAppModalTitle.Text = ViewState[viewStateModalTitleKey] as string;
        }



        /// <summary>
        /// Trigger an event that the submit or save button was pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveModalBtn_Click(object sender, EventArgs e)
        {
            SaveBtnClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Performs the logic to actually hide the modal.
        /// </summary>
        protected void HideModal()
        {
            AppModalContainer.CssClass = AppModalContainer.CssClass.Replace("app-modal-active", string.Empty);
        }
    }
}