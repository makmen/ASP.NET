<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangePass.aspx.cs" Inherits="WebVacuum.account.ChangePass" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="register">
        <asp:PlaceHolder ID="SuccessText" runat="server" Visible="false">
			<div class="mess-top">
				<div class="fbok">
                    <div class="success">
                        <asp:Literal ID="showSuccessText" runat="server"></asp:Literal>
                    </div>
				</div>
			</div>
        </asp:PlaceHolder>

		<div class="fb">
			<div class="description">Старый пароль:</div>
			<div class="date">
                <asp:TextBox ID="btnOldPassword" runat="server" CssClass="w100" TextMode="password"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorOldPass" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorOldPass" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Пароль:</div>
			<div class="date">
                <asp:TextBox ID="btnPassword" runat="server" CssClass="w100" TextMode="Password"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorNewPass" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorNewPass" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Повторить пароль:</div>
			<div class="date">
                <asp:TextBox ID="btnRePassword" runat="server" CssClass="w100" TextMode="password"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorReNewPass" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorReNewPass" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="submit news">
            <asp:Button ID="btnSave" runat="server" Text="Сохранить" CssClass="btn" OnClick="btnSave_Click" />
		</div> 
    </div>
</asp:Content>