<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="WebVacuum.account.Register" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
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
        <asp:PlaceHolder ID="ErrorInsert" runat="server" Visible="false">
			<div class="error">
				<div class="msg">
                    <asp:Literal ID="showErrorInsert" runat="server"></asp:Literal>
				</div>
			</div>
        </asp:PlaceHolder>
		<div class="fb">
			<div class="description">Имя:</div>
			<div class="date">
                <asp:TextBox ID="tbName" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorName" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorName" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Отчество:</div>
			<div class="date">
                <asp:TextBox ID="tbMiddleName" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorMiddleName" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorMiddleName" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Фамилия:</div>
			<div class="date">
                <asp:TextBox ID="tbLastName" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorLastName" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorLastName" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">E-mail:</div>
			<div class="date">
                <asp:TextBox ID="tbEmail" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorEmail" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorEmail" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Телефон:</div>
			<div class="date">
                <asp:TextBox ID="tbPhone" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorPhone" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorPhone" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
            </div>
			<div class="cb"></div>
		</div>
        <asp:PlaceHolder ID="holderAdd" runat="server" Visible="false">
		<div class="fb">
			<div class="description">Логин:</div>
			<div class="date">
                <asp:TextBox ID="tbLogin" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorLogin" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorLogin" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Пароль:</div>
			<div class="date">
                <asp:TextBox ID="tbPass" runat="server" CssClass="w100" TextMode="Password"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorPass" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorPass" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fb">
			<div class="description">Повторить пароль:</div>
			<div class="date">
                <asp:TextBox ID="tbRePass" runat="server" CssClass="w100"  TextMode="Password"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorRePass" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorRePass" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
		<div class="fbcaptaha">
			<div class="description">Введите код:</div>
			<div class="image">
				<img id="captchaDiv" src="../Capcha.ashx" alt="" />
                <img id="refresh" src="<%# GetUrl %>/images/refresh.gif" alt="" />
			</div>
			<div class="date">
                <asp:TextBox ID="tbCaptcha" runat="server" CssClass="w100"></asp:TextBox>
                <asp:PlaceHolder ID="ErrorCaptcha" runat="server" Visible="false">
					<div class="error">
						<div class="msg">
                            <asp:Literal ID="showErrorCaptcha" runat="server"></asp:Literal>
						</div>
					</div>
                </asp:PlaceHolder>
			</div>
			<div class="cb"></div>
		</div>
        <script type="text/javascript">
            $('#refresh').click(function () {
                $('#captchaDiv').attr('src', '../Capcha.ashx?r=' + Math.random());
            });
        </script> 
        </asp:PlaceHolder>

		<div class="submit">
            <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="Сохранить" OnClick="btnSave_Click" />
		</div> 
    </div> 

</asp:Content>