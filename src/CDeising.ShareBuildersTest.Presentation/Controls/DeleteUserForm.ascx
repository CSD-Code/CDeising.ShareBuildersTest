<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteUserForm.ascx.cs" Inherits="CDeising.ShareBuildersTest.Presentation.Controls.DeleteUserForm" %>

<p class="m-0 py-4 text-black">You are about to delete the user: <asp:Label runat="server" ID="LBLDeleteUser" CssClass="fw-bold" /></p>

<asp:TextBox Visible="false" runat="server" ID="TxtUserId" />