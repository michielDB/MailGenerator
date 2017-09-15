var _locNewArticle = "";
var _locAddCell = "";
var _locDeleteRow = "";

$(document).ready(function () {
    tinymce.init({
        forced_root_block: '',
        selector: 'textarea.editable',
        menubar: false,
        height: '25px',
        toolbar:
            'bold italic underline | alignleft aligncenter alignright alignjustify | formatselect fontselect fontsizeselect | bullist numlist | undo redo | removeformat | contactTags infoTags newsletterTags',
        setup: function (editor) {
            editor.addButton('contactTags',
                {
                    type: 'menubutton',
                    text: 'Contact tags',
                    icon: false,
                    menu: [
                        {
                            text: 'Aanspreektitel',
                            onclick: function() {
                                editor.insertContent('{{contact_title}}');
                            }
                        }, {
                            text: 'Voornaam',
                            onclick: function() {
                                editor.insertContent('{{contact_first_name}}');
                            }
                        }, {
                            text: 'Achternaam',
                            onclick: function() {
                                editor.insertContent('{{contact_last_name}}');
                            }
                        }, {
                            text: 'Straat',
                            onclick: function() {
                                editor.insertContent('{{contact_street}}');
                            }
                        }, {
                            text: 'Huisnummer',
                            onclick: function() {
                                editor.insertContent('{{contact_nr}}');
                            }
                        }, {
                            text: 'Bus',
                            onclick: function() {
                                editor.insertContent('{{contact_bus}}');
                            }
                        }, {
                            text: 'Postcode',
                            onclick: function() {
                                editor.insertContent('{{contact_postNr}}');
                            }
                        }, {
                            text: 'Gemeente',
                            onclick: function() {
                                editor.insertContent('{{contact_place}}');
                            }
                        }, {
                            text: 'Land',
                            onclick: function() {
                                editor.insertContent('{{contact_country}}');
                            }
                        }, {
                            text: 'Telefoonnummer',
                            onclick: function() {
                                editor.insertContent('{{contact_tel}}');
                            }
                        }, {
                            text: 'Email',
                            onclick: function() {
                                editor.insertContent('{{contact_email}}');
                            }
                        }, {
                            text: 'Bedrijf',
                            onclick: function() {
                                editor.insertContent('{{contact_company}}');
                            }
                        }
                    ]
                });
            editor.addButton('infoTags',
                {
                    type: 'menubutton',
                    text: 'Info tags',
                    icon: false,
                    menu: [
                        {
                            text: 'Info 1',
                            onclick: function() {
                                editor.insertContent('{{info_1}}');
                            }
                        },
                        {
                            text: 'Info 2',
                            onclick: function() {
                                editor.insertContent('{{info_2}}');
                            }
                        }, {
                            text: 'Info 3',
                            onclick: function() {
                                editor.insertContent('{{info_3}}');
                            }
                        }, {
                            text: 'Info 4',
                            onclick: function() {
                                editor.insertContent('{{info_4}}');
                            }
                        }, {
                            text: 'Info 5',
                            onclick: function() {
                                editor.insertContent('{{info_5}}');
                            }
                        }, {
                            text: 'Info 6',
                            onclick: function() {
                                editor.insertContent('{{info_6}}');
                            }
                        }, {
                            text: 'Info 7',
                            onclick: function() {
                                editor.insertContent('{{info_7}}');
                            }
                        }, {
                            text: 'Info 8',
                            onclick: function() {
                                editor.insertContent('{{info_8}}');
                            }
                        }, {
                            text: 'Info 9',
                            onclick: function() {
                                editor.insertContent('{{info_9}}');
                            }
                        }, {
                            text: 'Info 10',
                            onclick: function() {
                                editor.insertContent('{{info_10}}');
                            }
                        }
                    ]
                });
            editor.addButton('newsletterTags',
                {
                    type: 'menubutton',
                    text: 'Nieuwsbrief tags',
                    icon: false,
                    menu: [
                        {
                            text: 'Uitschrijflink',
                            onclick: function () {
                                editor.insertContent('{{unsubscribe:Schrijf je uit}}');
                            }
                        },
                        {
                            text: 'Bekijk online link',
                            onclick: function () {
                                editor.insertContent('{{view_online:Bekijk deze mail online}}');
                            }
                        }
                    ]
                });
        }
    });

    _locNewArticle = document.getElementById('locNewArticle').value;
    _locAddCell = document.getElementById('locAddCell').value;
    _locDeleteRow = document.getElementById('locDeleteRow').value;
});

function previewHeaderImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var imgPreview = document.getElementById("imgHeaderImage");
            imgPreview.src = e.target.result;
            imgPreview.style.height = "150px";
        }
        reader.readAsDataURL(input.files[0]);
    }
};
function previewFooterImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var imgPreview = document.getElementById("imgFooterImage");
            imgPreview.src = e.target.result;
            imgPreview.style.height = "150px";
        }
        reader.readAsDataURL(input.files[0]);
    }
};

var counter = 2; // row 1 already exists

function addNewRow() {
    //var newRow = $("<tr>");
    var newBtnRow = $("<tr>");
    var cols = "";
    var btnCol = "";

    var lastRowId = 0;
    var rowCount = lastRowId + 1;
    if ($('#tableAddRow tr').length > 0) {
        lastRowId = $('#tableAddRow tr').last()[0].id;
        rowCount = lastRowId.substring(lastRowId.lastIndexOf('w') + 1);
        ++rowCount;
    }

    //for (var i = 0; i < columns; i++) {
    cols += '<td><input type="submit" runat="server" value="' + _locNewArticle + '" name="btnaddarticle" id="btnAddArticle_' +
            rowCount +
            '_1" class="btn btn-mini" onclick="__doPostBack(id,\'\')" /></td>';
    //}
    var newRow = $('<tr id="tableAddRow' + rowCount + '">');

    newRow.append(cols);
    $("#tableAddRow").append(newRow);

    btnCol = '<td style="height: 51px">' +
        '<input type="button" runat="server" class="btn btn-info" id="addCell_' + rowCount + '" value="' + _locAddCell + '" onclick="addCell(id)"/>' +
        '<input type="button" runat="server" class="btn btn-danger" id="removeRow' + rowCount + '" value="' + _locDeleteRow + '" onclick="__doPostBack(id,\'\')"/>' +
        '</td>';
    newBtnRow.append(btnCol);
    $("#tableButtons").append(newBtnRow);
    counter++;
};
function addCell(sender) {
    var rowNumber = sender.split("_").pop();
    var colNumber = $("#tableAddRow" + rowNumber + " td").length + 1;

    if (colNumber <= 3) {
        $('#tableAddRow' + rowNumber).append(
            '<td><input type="submit" value="' + _locNewArticle + '" name="btnaddarticle" id="btnAddArticle_' +
            rowNumber +
            '_' +
            colNumber +
            '" class="btn btn-mini" onclick="__doPostBack(id,\'\')" /></td>');
    }
    return false;
}