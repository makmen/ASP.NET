<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="News.aspx.cs" Inherits="VacuumBase.News.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- <script> сценарий </script> -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% 
    if (Mode == "ViewAll")
    {
%>
        <asp:Repeater ID="Repeter" runat="server">
            <ItemTemplate>
                <div class="ambitios_p4">
	                <div class="ambitios_wrapper ambitios_p2">
		                <h3 class="ambitios_uppercase ambitios_p5"><%# Eval("Title") %></h3>
		                <div class="ambitios_date"><%# Eval("Created") %></div>
	                </div>
	                <p><%# Eval("Content") %> ... </p>
	                <div class="ambitios_wrapper">
		                <a href="<%# GetUrl %>/News/News.aspx?mode=view&id=<%# Eval("id") %>" class="ambitios_button_small_rev ambitios_fleft">Читать</a>
	                </div>
                </div>
            </ItemTemplate>

            <%--Шаблон разделителя элементов списка--%>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>

            <%--Шаблон подписи--%>
            <FooterTemplate>
                <div class="ambitios_pagination">
                    
                    <% for(int i = 1; i <= NumPages; i++) { %>
                        <a href="<%# GetUrl %>/News/News.aspx?p=<% Response.Write(i); %>"><% Response.Write(i); %></a>&nbsp;
                    <% } %>
                </div>
            </FooterTemplate>
        </asp:Repeater>
<% 
    }
    else if (Mode == "View")
    {
%>
    <div class="ambitios_p4">
	    <div class="ambitios_wrapper ambitios_p2">
		    <h3 class="ambitios_uppercase ambitios_p5"><% Response.Write(this.NewNews.Title); %></h3>
		    <div class="ambitios_date">
                <% Response.Write(this.NewNews.Created); 

                if (Session["login"] != null && Session["group"].ToString() == "1") 
                {
                %>
			        <div class="newsedit">
				        <a href="<%# GetUrl %>/News/News.aspx?mode=edit&id=<% Response.Write(Id); %>">Редактировать</a>
			        </div>
			        <div class="cb"></div>
                <% } %>
		    </div>
			    <br />
                <% Response.Write(this.NewNews.Content); %>
	    </div>
	    <a href="javascript:void(0)" onClick="back();">Вернуться на предыдущую страницы</a>
    </div>
    <script type="text/javascript" language="javascript">
        function back() {
            history.go(-1);
        }
    </script>
<% 
    }
    else
    { // добавление и редактирование
%>
    <h3 class="register">
        <% 
            if (Mode == "Add")
            {
                Response.Write("Добавить новость");
            } else {
                Response.Write("Редактировать новость");
            }
        %>
    </h3>
    <div id="register">
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
			    <div class="description">Название:</div>
			    <div class="date">
            <asp:TextBox ID="tbTitle" runat="server" CssClass="w100"></asp:TextBox>
			    </div>
			    <div class="cb"></div>
		    </div>
		    <div class="fb">
			    <div class="description">Содержание:</div>
			    <div class="date">
            <asp:TextBox ID="tbContent" runat="server" CssClass="w100" TextMode="MultiLine" Rows="30" Columns="10"></asp:TextBox>
			    </div>
			    <div class="cb"></div>
		    </div>
		    <div class="submit news">
          <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="Сохранить" OnClick="btnSave_Click" />
		    </div> 
    </div>
<% 
    }
%>
</asp:Content>