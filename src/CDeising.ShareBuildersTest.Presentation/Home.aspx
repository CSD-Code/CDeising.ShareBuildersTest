<%@ Page MasterPageFile="~/Layout/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CDeising.ShareBuildersTest.Presentation.Home" Async="true" EnableViewState="true" %>
<%@ Register TagPrefix="app" TagName="Modal" Src="~/Controls/Modal.ascx" %>
<%@ Register TagPrefix="app" TagName="AddUserForm" Src="~/Controls/AddUserForm.ascx" %>
<%@ Register TagPrefix="app" TagName="UpdateUserForm" Src="~/Controls/UpdateUserForm.ascx" %>
<%@ Register TagPrefix="app" TagName="DeleteUserForm" Src="~/Controls/DeleteUserForm.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

                    <%-- TODO: Combine these modals into one modal. --%>

                    <app:Modal runat="server" ID="AddUserModal" OnSaveBtnClicked="AddUserModal_SaveBtnClicked">
                        <ModalContent>
                            <app:AddUserForm runat="server" ID="AddUserForm" OnFormSubmitted="AddUserForm_FormSubmitted"/>
                        </ModalContent>
                    </app:Modal>

                    <app:Modal runat="server" ID="UpdateUserModal" OnSaveBtnClicked="UpdateUserModal_SaveBtnClicked">
                        <ModalContent>
                            <app:UpdateUserForm runat="server" ID="UpdateUserForm" OnFormSubmitted="UpdateUserForm_FormSubmitted"/>
                        </ModalContent>
                    </app:Modal>

                    <app:Modal runat="server" ID="DeleteUserModal" OnSaveBtnClicked="DeleteUserModal_SaveBtnClicked">
                        <ModalContent>
                            <app:DeleteUserForm runat="server" ID="DeleteUserForm" OnFormSubmitted="DeleteUserForm_FormSubmitted" />
                        </ModalContent>
                    </app:Modal>

            <div class="container py-5">

                <h4 class="mb-4">Stations</h4>
                <asp:DropDownList runat="server" ID="DDLStations" 
                    OnSelectedIndexChanged="DDLStations_SelectedIndexChanged"
                    AutoPostBack="true"></asp:DropDownList>

                <asp:Panel runat="server" ID="PanelSelectedStation" Visible="false">

                    <div class="row">
                        <div class="col-lg-4 my-3 my-lg-5">
                            <div class="app-card ">
                                <h4>Station Details</h4>
                                <div class="my-2">
                                    <p class="mb-1 fw-bold">Call Sign:</p>
                                    <asp:Label ID="LBLStationsCallSign" runat="server" />
                                </div>
                                <div>
                                    <p class="mb-1 fw-bold">Station Type:</p>
                                    <asp:Label ID="LBLStationsType" runat="server" />
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-lg-4 my-3 my-lg-5">
                            <div class="app-card">
                                <h4>Market Details</h4>
                                <div class="my-2">
                                    <p class="mb-1 fw-bold">Name:</p>
                                    <asp:Label ID="LBLMarketName" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 my-3 my-lg-5">
                            <div class="app-card">
                                <h4>Network Affiliations</h4>
                                <asp:Repeater runat="server" ID="RepeaterAffiliations">
                                    <HeaderTemplate>
                                        <ul class="app-card-ul">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <p class="mb-1 fw-bold">Name:</p>
                                            <p class="mb-1"><%# Eval("Name") %></p>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>

                    <div class="d-flex justify-content-between align-items-center mt-5 mt-lg-0 mb-5">
                        <h4>Users</h4>
                        <asp:Button runat="server" Text="Add New" ID="AddUserBtn" OnClick="AddUserBtn_Click" CssClass="add-new-btn" />
                    </div>

                    <div class="table-container">
                        <asp:GridView runat="server" ID="GVUsers"
                            CssClass="app-table"
                            AutoGenerateColumns="false"
                            OnRowCommand="GVUsers_RowCommand"
                            ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" CommandName="EditUser" CommandArgument='<%# Eval("Id") %>' ImageURL="~/Content/images/edit.svg" CssClass="me-3" />
                                        <asp:ImageButton runat="server" CommandName="DeleteUser" CommandArgument='<%# Eval("Id") %>' ImageUrl="~/Content/images/delete.svg"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <p class="text-center m-0 p-0">No Users found</p>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PanelNotSelectedStation" Visible="true">
                    <h2 class="mt-4 text-center">Select a Station</h2>
                </asp:Panel>

            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

    

</asp:Content>
