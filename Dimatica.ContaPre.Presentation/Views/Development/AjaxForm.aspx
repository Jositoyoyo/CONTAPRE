<%@ Page 
    Title="Formulario Ajax" 
    Language="C#" 
    MasterPageFile="~/Views/Shared/MasterPage.Master" 
    AutoEventWireup="true" 
    CodeBehind="AjaxForm.aspx.cs" 
    Inherits="Dimatica.ContaPre.Presentation.Views.Development.AjaxForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#submitButton').click(function (e) {
                e.preventDefault();

                var nombre = $('#nombre').val();

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~/Views/Ajax/AjaxController.aspx/ProcessRequest") %>',
                    data: JSON.stringify({ nombre: nombre }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        $('#response').html('Fecha: ' + response.d.date + '<br>Nombre: ' + response.d.nombre);
                    },
                    error: function (xhr, status, error) {
                        $('#response').html('Error: ' + xhr.responseText);
                    }
                });
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ajaxForm" class="container">
        <h3>Formulario Ajax</h3>
        <div>
            <label for="nombre">Nombre:</label>
            <input type="text" id="nombre" class="form-control" />
        </div>
        <div>
            <button id="submitButton" class="btn btn-primary">Enviar</button>
        </div>
        <div id="response"></div>
    </div>
</asp:Content>