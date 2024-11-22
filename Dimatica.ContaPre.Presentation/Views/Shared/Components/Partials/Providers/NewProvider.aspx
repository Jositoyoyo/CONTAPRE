<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/Components/Partials/Partials.master"
    AutoEventWireup="true"
    CodeBehind="NewProvider.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Shared.Components.Partials.Providers.NewProvider" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Views/Shared/Components/Partials/RadPartials.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<%@ Register Src="~/Views/Shared/Components/Alerts/CustomRadAlert/CustomRadAlert.ascx" TagPrefix="uc" TagName="CustomRadAlert" %>
<%@ Register Src="~/Views/Shared/Components/Modals/CustomRawWindow/CustomRawWindow.ascx" TagPrefix="uc" TagName="CustomRawWindow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <uc:CustomRawWindow ID="customModal" runat="server" />
    <uc:CustomRadAlert ID="CustomRadAlert" runat="server" />

    <div class="container">

        <div id="RadCustomFormPanel">

            <h4>DATOS BÁSICOS</h4>

            <div class="form-container">

                <div class="form-group form-group-fake">

                    <div class="field-container field-30">
                        <span>Nombre:</span>
                        <asp:TextBox ID="txtName" Width="100%" runat="server" MaxLength="60" CssClass="form-control" />
                    </div>

                    <div class="field-container field-5">
                        <span>NIF:</span>
                        <asp:TextBox ID="txtNifFirst" runat="server" MaxLength="1" CssClass="form-control" />
                        <asp:RegularExpressionValidator ControlToValidate="txtNifFirst" ValidationExpression="^[A-Za-z]$" ErrorMessage="Debe ser una sola letra." ValidationGroup="ProviderGroup" runat="server" CssClass="text-danger" />
                    </div>

                    <%--guion--%>
                    <div class="field-5 nif-guion">
                        <span>-</span>
                    </div>

                    <div class="field-container field-15">
                        <span>&nbsp;</span>
                        <asp:TextBox ID="txtNifNumber" runat="server" MaxLength="8" CssClass="form-control" />
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtNifNumber"
                            ValidationExpression="^\d{7,8}$"
                            ErrorMessage="Debe ser un número de 7 a 8 dígitos"
                            ValidationGroup="ProviderGroup"
                            runat="server"
                            CssClass="text-danger" />

                    </div>

                    <%--guion--%>
                    <div class="field-5 nif-guion">
                        <span>-</span>
                    </div>

                    <div class="field-container field-5">
                        <span>&nbsp;</span>
                        <asp:TextBox ID="txtNifLast" runat="server" MaxLength="1" CssClass="form-control" />
                        <asp:RegularExpressionValidator ControlToValidate="txtNifLast" ValidationExpression="^[A-Za-z]$" ErrorMessage="Debe ser una sola letra." ValidationGroup="ProviderGroup" runat="server" CssClass="text-danger" />
                    </div>

                    <div class="field-container field-15">
                        <span>Teléfono:</span>
                        <asp:TextBox ID="txtPhone" Width="100%" runat="server" MaxLength="20" CssClass="form-control" />
                    </div>

                    <div class="field-container field-20">
                        <span>Persona contacto:</span>
                        <asp:TextBox ID="txtPerson" Width="100%" runat="server" MaxLength="75" CssClass="form-control" />
                    </div>

                </div>

                <div class="form-group form-group-fake">

                    <div class="field-container field-35">
                        <span>Dirección:</span>
                        <asp:TextBox ID="txtAddress" Width="100%" runat="server" MaxLength="100" CssClass="form-control" />
                    </div>

                    <div class="field-container field-10">
                        <span>C.P.:</span>
                        <asp:TextBox ID="txtCp" Width="100%" runat="server" MaxLength="5" CssClass="form-control" />
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtCp"
                            ValidationExpression="^\d{5}$"
                            ErrorMessage="El código postal debe ser un número de 5 dígitos válido."
                            ValidationGroup="ProviderGroup"
                            runat="server"
                            CssClass="text-danger" />
                    </div>

                    <div class="field-container field-20">
                        <span>Provincia:</span>
                        <select id="ddlProvince" runat="server" class="form-control">
                            <option value="-1">< Seleccione ></option>
                        </select>
                    </div>

                    <div class="field-container field-35">
                        <span>Población:</span>
                        <asp:TextBox ID="txtPopulation" Width="100%" runat="server" MaxLength="50" CssClass="form-control" />
                    </div>

                </div>

                <h4>DATOS BANCARIOS</h4>

                <div class="form-group form-group-fake">

                    <div class="field-container field-10">
                        <span>CCC:</span>
                        <asp:TextBox ID="txtCcc" Width="100%" runat="server" MaxLength="10" CssClass="form-control" />
                    </div>

                    <div class="field-container field-15">
                        <span>Entidad:</span>
                        <asp:TextBox ID="txtCcEntity" Width="100%" runat="server" MaxLength="4" CssClass="form-control" />
                    </div>

                    <div class="field-container field-15">
                        <span>Sucursal:</span>
                        <asp:TextBox ID="txtCcBranch" Width="100%" runat="server" MaxLength="4" CssClass="form-control" />
                    </div>

                    <div class="field-container field-10">
                        <span>DC:</span>
                        <asp:TextBox ID="txtCcDc" Width="100%" runat="server" MaxLength="2" CssClass="form-control" />
                    </div>

                    <div class="field-container field-25">
                        <span>Cuenta:</span>
                        <asp:TextBox ID="txtCcAccount" Width="100%" runat="server" MaxLength="10" CssClass="form-control" />
                    </div>

                </div>

                <div class="form-group form-group-fake">

                    <div class="field-container field-20">
                        <span>Domicilio:</span>
                        <asp:TextBox ID="txtBranchAddress" Width="100%" runat="server" MaxLength="100" CssClass="form-control" />
                    </div>

                    <div class="field-container field-20">
                        <span>Entidad:</span>
                        <asp:TextBox ID="txtBranchEntity" Width="100%" runat="server" MaxLength="50" CssClass="form-control" />
                    </div>

                    <div class="field-container field-10">
                        <span>C.P.:</span>
                        <asp:TextBox ID="txtBranchCp" Width="100%" runat="server" MaxLength="5" CssClass="form-control" />
                        <!-- Validador para asegurar que el CP sea válido (exactamente 5 dígitos) -->
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtBranchCp"
                            ValidationExpression="^\d{5}$"
                            ErrorMessage="El código postal debe ser un número de 5 dígitos válido."
                            ValidationGroup="ProviderGroup"
                            runat="server"
                            CssClass="text-danger" />
                    </div>

                    <div class="field-container field-20">
                        <span>Localidad:</span>
                        <asp:TextBox ID="txtBranchLocation" Width="100%" runat="server" MaxLength="50" CssClass="form-control" />
                    </div>

                </div>

                <div class="buttons">
                    <asp:HyperLink ID="Button1" CssClass="insert" runat="server" onclick="confirmInsertProvider(); return false;" ValidationGroup="ProviderGroup">Insertar nuevo proveedor</asp:HyperLink>
                    <asp:HyperLink ID="Button2" CssClass="close" runat="server" onclick="closeAction(); return false;">Cerrar</asp:HyperLink>
                    <asp:HyperLink ID="Button3" CssClass="clear" runat="server" Style="display:none;" onclick="alertClearForm(); return false;">Limpiar</asp:HyperLink>
                </div>

            </div>

        </div>

    </div>

    <script type="text/javascript">

        function alertClearForm() {

            openCustomModal2({
                title: 'Limpiar el formulario',
                message: 'Atencion, se va a limpiar el formulario. <strong>¿Desea continuar?</strong>',
                width: 350,
                height: 200,
                okText: 'Sí',
                cancelText: 'Cancelar',
                showCustomRadModalcloseButton: false,
                callback: function (isConfirmed) {
                    if (isConfirmed) {
                        clearForm();
                    }
                }
            });
        }

        function clearForm() {
            // Limpiar todos los campos del formulario
            $("#<%= txtNifFirst.ClientID %>").val('');
            $("#<%= txtNifNumber.ClientID %>").val('');
            $("#<%= txtNifLast.ClientID %>").val('');
            $("#<%= txtName.ClientID %>").val('');
            $("#<%= txtPhone.ClientID %>").val('');
            $("#<%= txtPerson.ClientID %>").val('');
            $("#<%= txtAddress.ClientID %>").val('');
            $("#<%= txtCp.ClientID %>").val('');
            $("#<%= ddlProvince.ClientID %>").val('-1');
            $("#<%= txtPopulation.ClientID %>").val('');
            $("#<%= txtCcc.ClientID %>").val('');
            $("#<%= txtCcEntity.ClientID %>").val('');
            $("#<%= txtCcBranch.ClientID %>").val('');
            $("#<%= txtCcDc.ClientID %>").val('');
            $("#<%= txtCcAccount.ClientID %>").val('');
            $("#<%= txtBranchAddress.ClientID %>").val('');
            $("#<%= txtBranchEntity.ClientID %>").val('');
            $("#<%= txtBranchCp.ClientID %>").val('');
            $("#<%= txtBranchLocation.ClientID %>").val('');
        }

        function confirmInsertProvider()
        {

            const txtName = $("#<%= txtName.ClientID %>").val();
            const txtNifNumber = $("#<%= txtNifNumber.ClientID %>").val();

            if (!txtName && !txtNifNumber)
            {
                openCustomRadAlert2({
                    title: 'Datos de formulario no validos',
                    message: 'Por favor, debe indicar un nombre de proveedor o documento',
                    width: 350,
                    height: 300,
                    buttonText: 'Entendido',
                    callback: null
                });
                return ;
            }

            if (Page_ClientValidate("ProviderGroup"))
            {
                openCustomModal2({
                    title: 'Confirmación',
                    message: '<p>Se va a insertar un nuevo proveedor</p><strong>¿Desea continuar?</strong>',
                    width: 350,
                    height: 200,
                    okText: 'Sí',
                    cancelText: 'No',
                    callback: function (isConfirmed) {
                        if (isConfirmed) {
                            InsertProvider();
                        }
                    }
                });


            } else {
                openCustomRadAlert2({
                    title: 'Datos de formulario no validos',
                    message: 'Por favor, revise los datos del formulario.',
                    width: 350,
                    height: 300,
                    buttonText: 'Entendido',
                    callback: null
                });
            }

        }

        function InsertProvider() {

            openCustomRadAlert2({
                title: 'Enviando datos',
                message: 'Enviado datos al servidor. Espere por favor...',
                width: 350,
                height: 300,
                buttonText: '',
                showCustomRadAlertCloseButton: false,
                callback: null
            });

            const sUrl = "<%= ResolveUrl("~/Views/Maintenance/Providers.aspx/AddProvider") %>";

            $.ajax({
                type: "POST",
                url: sUrl,
                data: JSON.stringify({
                    name: $("#<%= txtName.ClientID %>").val(),
                    nifFirst: $("#<%= txtNifFirst.ClientID %>").val(),
                    nifNumber: $("#<%= txtNifNumber.ClientID %>").val(),
                    nifLast: $("#<%= txtNifLast.ClientID %>").val(),
                    phone: $("#<%= txtPhone.ClientID %>").val(),
                    person: $("#<%= txtPerson.ClientID %>").val(),
                    address: $("#<%= txtAddress.ClientID %>").val(),
                    cp: $("#<%= txtCp.ClientID %>").val(),
                    province: $("#<%= ddlProvince.ClientID %>").val(),
                    population: $("#<%= txtPopulation.ClientID %>").val(),
                    ccc: $("#<%= txtCcc.ClientID %>").val(),
                    entity: $("#<%= txtCcEntity.ClientID %>").val(),
                    branch: $("#<%= txtCcBranch.ClientID %>").val(),
                    dc: $("#<%= txtCcDc.ClientID %>").val(),
                    account: $("#<%= txtCcAccount.ClientID %>").val(),
                    branchAddress: $("#<%= txtBranchAddress.ClientID %>").val(),
                    branchEntity: $("#<%= txtBranchEntity.ClientID %>").val(),
                    branchCp: $("#<%= txtBranchCp.ClientID %>").val(),
                    branchLocation: $("#<%= txtBranchLocation.ClientID %>").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    const responseObject = JSON.parse(response.d);

                    console.log(responseObject);

                    if (responseObject.status === "success") {

                        $("#<%= Button1.ClientID %>").hide();
                        $("#<%= Button3.ClientID %>").hide();

                        // Deshabilitar todos los inputs en el formulario
                        $("input").prop("disabled", true);
                        $("select").prop("disabled", true); 

                        openCustomRadAlert2({
                            title: 'Exito en la operacion',
                            message: 'Los datos del proveedor han sido guardados con éxito!',
                            width: 350,
                            height: 300,
                            buttonText: 'Entendido',
                            callback: function () {
                                closeAction();
                            }
                        });


                    } else {

                        openCustomRadAlert2({
                            title: 'Error al guardar los datos',
                            message: 'No se pudo completar la acción solicitada : ' + responseObject.message,
                            width: 350,
                            height: 300,
                            buttonText: 'Entendido',
                            callback: null
                        });
                    }
                },
                error: function () {
                  
                    openCustomRadAlert2({
                        title: 'Error al guardar los datos',
                        message: 'Ha ocurrido un error de aplicacion. Por favor contacte con el administrador',
                        width: 350,
                        height: 300,
                        buttonText: 'Entendido',
                        callback: null
                    });
                }
            });


        }

        function closeAction()
        {
            clearForm();
            window.close();
        }

        $(document).ready(function () {

            const $clearButton = $("#<%= Button3.ClientID %>");
            $clearButton.hide(); // Aseguramos que está oculto al inicio

            // Detectar cambios en campos de texto y select
            $(".form-control, select").on("input change", function () {
                let showClear = false;

                // Verificar si al menos un campo tiene datos o un select está seleccionado
                $(".form-control, select").each(function () {
                    if ($(this).val().trim() !== "" && $(this).val() !== "-1") {
                        showClear = true;
                        return false; // Salir del bucle al encontrar un campo lleno
                    }
                });

                if (showClear) {
                    $clearButton.show(); // Mostrar botón
                } else {
                    $clearButton.hide(); // Ocultar botón
                }
            });
        });



    </script>

</asp:Content>
