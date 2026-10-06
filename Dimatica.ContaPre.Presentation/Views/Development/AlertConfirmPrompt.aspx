<%@ Page
    Title=""
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    CodeFile="AlertConfirmPrompt.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Development.AlertConfirmPrompt" %>

<%@ Register Src="~/Views/Shared/Components/Modals/CustomRawWindow/CustomRawWindow.ascx" TagPrefix="uc" TagName="CustomRawWindow" %>
<%@ Register Src="~/Views/Shared/Components/Alerts/CustomRadAlert/CustomRadAlert.ascx" TagPrefix="uc" TagName="CustomRadAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc:CustomRawWindow ID="customModal" runat="server" />
    <uc:CustomRadAlert ID="CustomRadAlert" runat="server" />

    <div class="container">

        <div class="demo-container">
            <h2>Dialogs called from client: </h2>
            <button style="width: 230px;" onclick="radalert('Radalert is called from the client!', 330, 180, 'Client RadAlert', alertCallBackFn, $dialogsDemo.imgUrl);  return false;">
                radalert from client
            </button>
            <br />
            <br />
            <button style="width: 230px;" onclick="radconfirm('Client radconfirm: Are you sure?', confirmCallBackFn, 330, 180, null, 'Client RadConfirm', $dialogsDemo.imgUrl); return false;">
                radconfirm from client
            </button>
            <br />
            <br />
            <button style="width: 230px;" onclick="radprompt('Client RadPrompt: What is the answer of Life, Universe and Everything?', promptCallBackFn, 350, 230, null, 'Client RadPrompt', '42'); return false;">
                radprompt from client
            </button>
        </div>
        <br />
        <br />
        <button type="button" onclick="showCustomConfirmation(); return false;">Mostrar Confirmación</button>
        <br /><br />
        <button type="button" onclick="confirmExecute(); return false;">Mostrar Confirmación2</button>
        <button type="button" onclick="confirmExecute2(); return false;">Mostrar Confirmación3</button>


         <br /><br />
        <button type="button" onclick="myalerta(); return false;">Mostrar alerta</button>
                <button type="button" onclick="myalerta2(); return false;">Mostrar alerta2</button>

      

    </div>


    <telerik:RadScriptBlock runat="server">

        <script>


            function myalerta() {
                openCustomRadAlert("Ha ocurrido un error ejecutando la Modificación de Crédito en cuestion", 350, 300, "Error");
            }

            function myalerta2() {
                openCustomRadAlert2({
                    title: 'Error de Ejecución',
                    message: '<strong>Error:</strong> No se pudo completar la acción solicitada.',
                    width: 350,
                    height: 300,
                    buttonText: 'Entendido',
                    callback: function () {
                        console.log("La alerta se cerró");
                    }
                });
            }


            function showCustomConfirmation() {
                radalert('<strong>radalert</strong> returned the following result: <h3 style="color: #ff0000;">ssss</h3>', 350, 250, 'Result', null);
            }

            // Function to open the modal and handle the result
            function confirmExecute()
            {
                openCustomModal("¿Estás seguro de que deseas realizar esta acción?", 350, 250, "Confirmación", function (isConfirmed) {
                    if (isConfirmed) {
                        console.log("Acción confirmada");
                    } else {
                        console.log("Acción cancelada");
                    }
                });
            }

            function confirmExecute2()
            {
                openCustomModal2({
                    title: 'Confirmación',
                    message: '<strong>¿Desea continuar?</strong> <p>Esta acción es irreversible.</p>',
                    width: 450,
                    height: 200,
                    okText: 'Sí',
                    cancelText: 'No',
                    callback: function (isConfirmed) {
                        if (isConfirmed) {
                            console.log("Acción confirmada");
                        } else {
                            console.log("Acción cancelada");
                        }
                    }
                });

                
            }

            (function (global, undefined) {
                var demo = {};


                // Mueve la llamada a radalert dentro del evento load de Sys.Application
                Sys.Application.add_load(function () {
             //       radalert('Radalert is called from the client!', 330, 180, 'Client RadAlert', alertCallBackFn);
                });

                

                function alertCallBackFn(arg) {
                    radalert('<strong>radalert</strong> returned the following result: <h3 style="color: #ff0000;">' + arg + '</h3>', 350, 250, 'Result');
                }

                function confirmCallBackFn(arg) {
                    radalert('<strong>radconfirm</strong> returned the following result: <h3 style="color: #ff0000;">' + arg + '</h3>', 350, 250, 'Result');
                }

                function promptCallBackFn(arg) {
                    radalert('After 7.5 million years, <strong>Deep Thought</strong> answers:<h3 style="color: #ff0000;">' + arg + '</h3>', 350, 250, 'Deep Thought');
                }

                Sys.Application.add_load(function () {
                    // attach a handler to radio buttons to update global variable holding image url
                    $telerik.$('input:radio').bind('click', function () {
                        demo.imgUrl = $telerik.$(this).val();
                    });
                });

                global.alertCallBackFn = alertCallBackFn;
                global.confirmCallBackFn = confirmCallBackFn;
                global.promptCallBackFn = promptCallBackFn;

                global.$dialogsDemo = demo;
            })(window);
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
