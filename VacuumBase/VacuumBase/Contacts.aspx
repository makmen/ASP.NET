<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Contacts.aspx.cs" Inherits="VacuumBase.Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- <script> сценарий </script> -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="ambitios_uppercase">Контактная информация </h3>
    <div id="contact_form">
	    <div id="Note"></div>
            <%
                if (this.send && this.success) 
                {
            %>
                <div class="mess-top">
                    <div class="fbok">
                        <div class="success">Письмо отправлено</div>
                    </div>
                </div>
            <%
                }
                else if (this.send && !this.success)
                {
            %>
	            <div class="mess-top">
		            <div class="error">
			            <div class="msg">Ошибка отправки данных</div>
		            </div>
	            </div>
            <%
                }
            %>
		    <div class="field ambitios_input_standat_height ambitios_p2">
			    <label>Имя</label>
                <asp:TextBox ID="btnName" runat="server" CssClass="required"></asp:TextBox>
		    </div>
		    <div class="field ambitios_input_standat_height ambitios_p2">
			    <label>Email</label>
                <asp:TextBox ID="btnEmail" CssClass="required email" runat="server"></asp:TextBox>
		    </div>
		    <div class="ambitios_textarea ambitios_p2 field">
			    <label>Сообщение</label>
                <asp:TextBox ID="btnMessage" runat="server" CssClass="required" TextMode="MultiLine" Rows="5" Columns="10"></asp:TextBox>
		    </div>
		    <div>
			    <div class="buttons-wrapper">
				    <div class="ambitios_wrapper ambitios_p2">
					    <div class="ambitios_button_contact">
                            <asp:Button ID="btnContact" runat="server" Text="Send" OnClick="btnContact_Click" />
					    </div>
				    </div>
			    </div>
		    </div>
    </div>
    <div class="ambitios_wrapper">
	    <div class="ambitios_fleft">
		    <h3 class="ambitios_uppercase">Директор: Высоцкий Василий Семенович </h3>
		    Phone: +375 29 615 14 12<br />
		    Fax: 8017 125 32 64<br />
		    Email: <a href="mailto:mail@vactt@mail.ru">vactt@mail.ru</a><br /> 
		    Email: <a href="mailto:mail@vvs200362@list.ru">vvs200362@list.ru</a> 
	    </div>
    </div>
    <br />
    <div class="ambitios_picture ambitios_p2">
	    <script type="text/javascript" charset="utf-8" src="https://api-maps.yandex.ru/services/constructor/1.0/js/?sid=eG5OG_eatgnABSizBk2fviWWJi38Kdu4&width=100%&height=400&lang=ru_RU&sourceType=constructor"></script>
    </div>
</asp:Content>