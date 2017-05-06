<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangePass.aspx.cs" Inherits="VacuumBase.Account.ChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- <script> сценарий </script> -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="register">
        <%
            if (OkMessage != null)
            {
        %>  
            <div class="mess-top">
                <div class="fbok">
                    <div class="success"><% Response.Write(OkMessage); %></div>
                </div>
            </div>
        <% } %>
		<div class="fb">
			<div class="description">Старый пароль:</div>
			<div class="date">
                <asp:TextBox ID="btnOldPassword" runat="server" CssClass="w100" TextMode="password"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("btnOldPassword"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("btnOldPassword"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Пароль:</div>
			<div class="date">
                <asp:TextBox ID="btnPassword" runat="server" CssClass="w100" TextMode="Password"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("btnPassword"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("btnPassword"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Повторить пароль:</div>
			<div class="date">
                <asp:TextBox ID="btnRePassword" runat="server" CssClass="w100" TextMode="password"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("btnRePassword"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("btnRePassword"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="submit news">
            <asp:Button ID="btnSave" runat="server" Text="Сохранить" CssClass="btn" OnClick="btnSave_Click" />
		</div> 
    </div>
</asp:Content>
