<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="ProvidersControl.ascx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Controls.ProvidersControl" %>

<telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgSimilarities"
    runat="server"
    OnNeedDataSource="RgSimilarities_NeedDataSource"
    OnItemDataBound="RgSimilarities_OnItemDataBound"
    Culture="es-ES"
    GroupPanelPosition="Top"
    Width="100%"
    Height="250px"
    CssClass="providers-control-table">

    <GroupingSettings CaseSensitive="false"></GroupingSettings>

    <MasterTableView AutoGenerateColumns="False"
        CommandItemDisplay="None"
        AllowPaging="False"
        AllowSorting="False"
        PagerStyle-AlwaysVisible="True"
        AllowFilteringByColumn="False"
        NoMasterRecordsText="No hay datos a mostrar."
        TableLayout="Fixed">

        <CommandItemSettings ShowExportToExcelButton="False"
            ShowAddNewRecordButton="False"
            ShowRefreshButton="false"
            ShowExportToPdfButton="false" />

        <Columns>
            <telerik:GridBoundColumn UniqueName="Code"
                DataField="PROV_CODIGO"
                HeaderText="Código"
                AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains"
                ShowFilterIcon="false">
                <HeaderStyle Width="150px" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="Name"
                DataField="PROV_NOMBRE"
                HeaderText="Nombre"
                AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains"
                ShowFilterIcon="false">
                <HeaderStyle Width="40%" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="Nif"
                DataField="PROV_NIF"
                HeaderText="Nif"
                AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains"
                ShowFilterIcon="false">
                <HeaderStyle Width="20%" />
            </telerik:GridBoundColumn>
            <telerik:GridEditCommandColumn UniqueName="EditColumn"
                ButtonType="LinkButton"
                HeaderTooltip="Ver Proveedor"
                ItemStyle-HorizontalAlign="Center"
                HeaderStyle-Width="40px"
                ItemStyle-Width="40px"
                ItemStyle-CssClass="fas fa-eye"
                ShowFilterIcon="false"
                EditText=" ">
            </telerik:GridEditCommandColumn>
        </Columns>

        <EditFormSettings EditFormType="Template">
            <FormTemplate>

                <div class="form-group form-group-fake">
                    <%--Nombre--%>
                    <div class="field-container field-30">
                        <span>Nombre:</span>
                        <telerik:RadTextBox ID="modalTxtName"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_NOMBRE") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>

                    <%--Nif--%>
                    <div class="field-container field-20">
                        <span>NIF:</span>
                        <telerik:RadTextBox ID="modalTxtNif"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_NIF") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>

                    <%--Telefono--%>
                    <div class="field-container field-20">
                        <span>Teléfono:</span>
                        <telerik:RadTextBox ID="modalTxtPhone"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_TELEFONO") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>

                    <%--Persona--%>
                    <div class="field-container field-30">
                        <span>Persona contacto:</span>
                        <telerik:RadTextBox ID="modalTxtPerson"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_PERSONA_CONTACTO") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>
                </div>

                <div class="form-group form-group-fake">
                    <%--Direccion--%>
                    <div class="field-container field-30">
                        <span>Dirección:</span>
                        <telerik:RadTextBox ID="modalTxtAddress"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_DIRECCION") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>

                    <%--CP--%>
                    <div class="field-container field-20">
                        <span>C.P.:</span>
                        <telerik:RadTextBox ID="modalTxtCp"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_CODIGO_POSTAL") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>

                    <%--Provincia--%>
                    <div class="field-container field-20">
                        <span>Provincia:</span>
                        <telerik:RadTextBox ID="modalTxtProvince"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("ProvName") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>                    

                    <%--Poblacion--%>
                    <div class="field-container field-30">
                        <span>Población:</span>
                        <telerik:RadTextBox ID="modalTxtPopulation"
                            Width="100%"
                            runat="server"
                            Text='<%#this.Eval("PROV_POBLACION") %>'
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>
                </div>

                <%--BUTTONS--%>
                <div class="buttons">

                    <asp:LinkButton ID="btnCancel"
                        runat="server"
                        CausesValidation="False"
                        CommandName="Cancel"
                        CssClass="Button Cancel">
                        Cerrar
                    </asp:LinkButton>

                </div>

            </FormTemplate>
        </EditFormSettings>
    </MasterTableView>

    <ClientSettings>
        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
    </ClientSettings>
</telerik:RadGrid>