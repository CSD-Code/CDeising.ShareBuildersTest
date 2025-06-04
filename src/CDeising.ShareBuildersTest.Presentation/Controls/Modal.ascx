<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Modal.ascx.cs" Inherits="CDeising.ShareBuildersTest.Presentation.Controls.Modal" %>

<asp:Panel runat="server" CssClass="app-modal-container" ID="AppModalContainer">

    <asp:Button runat="server" class="app-modal-overlay" OnClick="CloseModal"></asp:Button>

    <div class="app-modal mx-2">
        <div class="app-modal-header p-4 d-flex justify-content-between align-items-center">
            <div class="d-flex align-items-center">
                <asp:Label runat="server" ID="LBLAppModalTitle" CssClass="app-modal-title" />
            </div>
            
            <asp:ImageButton CssClass="app-modal-close-btn" runat="server" ID="AppModalClose" OnClick="CloseModal" ImageUrl="~/Content/images/close.svg" />
        </div>
        <div class="app-modal-content">
            <asp:PlaceHolder runat="server" ID="PH"></asp:PlaceHolder>
        </div>
        <div class="app-modal-footer p-4 d-flex justify-content-end">
            <div class="me-3">
                <asp:Button runat="server" ID="CloseModalBtn" CssClass="modal-footer-close-btn" Text="Close" OnClick="CloseModal"></asp:Button>
            </div>
            <asp:Button runat="server" ID="SaveModalBtn" Text="Save" CssClass="modal-footer-save-btn" OnClick="SaveModalBtn_Click"></asp:Button>
        </div>
    </div>

</asp:Panel>