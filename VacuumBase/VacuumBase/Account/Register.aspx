<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="VacuumBase.Account.Register" %>

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


        <%
            if (ErrMessage != null)
            {
        %>  
            <div class="mess-top">
                <div class="error">
                    <div class="msg"><% Response.Write(ErrMessage); %></div>
                </div>
            </div>
        <% } %>
		<div class="fb">
			<div class="description">Имя:</div>
			<div class="date">
                <asp:TextBox ID="tbName" runat="server" CssClass="w100"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbName")) {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbName"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Отчество:</div>
			<div class="date">
                <asp:TextBox ID="tbMiddleName" runat="server" CssClass="w100"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbMiddleName"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbMiddleName"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Фамилия:</div>
			<div class="date">
                <asp:TextBox ID="tbLastName" runat="server" CssClass="w100"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbLastName"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbLastName"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">E-mail:</div>
			<div class="date">
                <asp:TextBox ID="tbEmail" runat="server" CssClass="w100"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbEmail"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbEmail"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Телефон:</div>
			<div class="date">
                <asp:TextBox ID="tbPhone" runat="server" CssClass="w100"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbPhone"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbPhone"); %>
						</div>
					</div>
                <% } %>
            </div>
			<div class="cb"></div>
		</div>
<%
    if (Mode == "add")
    {
%> 
		<div class="fb">
			<div class="description">Логин:</div>
			<div class="date">
                <asp:TextBox ID="tbLogin" runat="server" CssClass="w100"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbLogin"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbLogin"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Пароль:</div>
			<div class="date">
                <asp:TextBox ID="tbPass" runat="server" CssClass="w100" TextMode="Password"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbPass"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbPass"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Повторить пароль:</div>
			<div class="date">
                <asp:TextBox ID="tbRePass" runat="server" CssClass="w100"  TextMode="Password"></asp:TextBox>
                <%
                    if (Errs.ContainsKey("tbRePass"))
                    {
                %>  
					<div class="error">
						<div class="msg">
                            <% this.GetError("tbRePass"); %>
						</div>
					</div>
                <% } %>
			</div>
			<div class="cb"></div>
		</div>
<% } %>

		<div class="submit">
            <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="Сохранить" OnClick="btnSave_Click" />
		</div> 
    </div> 

</asp:Content>