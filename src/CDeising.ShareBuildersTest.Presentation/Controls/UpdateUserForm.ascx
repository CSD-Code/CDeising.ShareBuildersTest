<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateUserForm.ascx.cs" Inherits="CDeising.ShareBuildersTest.Presentation.Controls.UpdateUserForm" %>

<div class="my-5">
    <label class="mb-2 fw-bold">First Name</label>
    <asp:TextBox runat="server" ID="TxtUserFirstName" AutoCompleteType="None" />
    <asp:Label runat="server"
        ID="TXTUserFirstNameValidation"
        Visible="false"
        CssClass="validation-txt"/>
</div>

<div class="my-5">
    <label class="mb-2 fw-bold">Last Name</label>
    <asp:TextBox runat="server" ID="TxtUserLastName" AutoCompleteType="None" />
    <asp:Label runat="server"
        ID="TXTUserLastNameValidation"
        Visible="false"
        CssClass="validation-txt"/>
</div>

<asp:TextBox runat="server" Visible="false" ID="TxtUserId" />
<asp:TextBox runat="server" Visible="false" ID="TxtStationId" />