
    window.onload = function () {

        document.getElementById('AddField').onclick = AddARow;
    }

    function AddARow() {

        $('#ChildTable tr:last').after('<tr><td colspan="10">@Html.EditorFor(model => Model.ApplicationTableFields[i].Description, new { htmlAttributes = new { @class = "form-control" } })</td><td colspan="3">@Html.DropDownListFor(model => Model.ApplicationTableFields[i].FieldType, new SelectList(ViewBag.FieldTypeList, "Text", "Value"), Model.ApplicationTableFields[i].FieldType)</td><td>Tree</td><td colspan="2"><center>@Html.CheckBoxFor(model => Model.ApplicationTableFields[i].IsAudited)</center></td><td class="text-center">@using (Html.BeginForm("DeleteChild", "ApplicationTableAndFieldsViewModel")){@Html.Hidden("ApplicationTableFieldID", Model.ApplicationTableFields[i].ApplicationTableFieldID)<input type="submit" class="btn btn-default btn-xs" value="Delete"/>}</td></tr>');
        };

