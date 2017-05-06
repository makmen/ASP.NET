<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="News.aspx.cs" Inherits="WebVacuum.news.News" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:PlaceHolder runat="server" ID="ModeViewAll" Visible ="false">
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
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="ModeView" Visible ="false">
        <div class="ambitios_p4">
	        <div class="ambitios_wrapper ambitios_p2">
		        <h3 class="ambitios_uppercase ambitios_p5"></h3>
		        <div class="ambitios_date">
                    <asp:Literal ID="NewsCreated" runat="server"></asp:Literal>
                    <asp:PlaceHolder ID="NewsEdit" runat="server" Visible="false">
			            <div class="newsedit">
				            <a href="<%# GetUrl %>/News/News.aspx?mode=edit&id=<% Response.Write(Id); %>">Редактировать</a>
			            </div>
			            <div class="cb"></div>
                    </asp:PlaceHolder>
		        </div>
			    <br />
                    <asp:Literal ID="NewsContent" runat="server"></asp:Literal>

	        </div>
	        <a href="javascript:void(0)" onClick="back();">Вернуться на предыдущую страницы</a>
        </div>
        <script type="text/javascript" language="javascript">
            function back() {
                history.go(-1);
            }
        </script>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="ModeAddEdit" Visible ="false">

    <div id="register">
        <h3 class="register">
            <asp:Literal ID="ltHeader" runat="server"></asp:Literal>
        </h3>
        <asp:PlaceHolder ID="ErrorInsert" runat="server" Visible="false">
            <br />
			<div class="mess-top">
                <div class="error">
				    <div class="msg">
                        <asp:Literal ID="showErrorInsert" runat="server"></asp:Literal>
				    </div>
			    </div>
            </div>
        </asp:PlaceHolder>
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
    </asp:PlaceHolder>
</asp:Content>