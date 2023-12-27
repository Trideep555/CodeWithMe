// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if (document.getElementById('editor')) {
    var editor = ace.edit("editor");
    editor.setTheme("ace/theme/monokai");
    document.getElementById('type').disabled = true;
}
$(function () {
    // Attach event handlers to elements on the page
    $('#example').DataTable();
    $("#select").chosen();
    $(".editor-check").click((e) => {
        e.preventDefault();
        //alert("clicked");
        $("#Code").val(editor.getValue());
        var id = $("#lang").val().split(":")[1];
        $("#LanguageId").val(id);
        $(".editor-form").submit();
    })

});
function changeit(e) {
    if (e.value == "")
        return;
    var x = e.value.split(":");
    console.log(x);
    var session = editor.getSession();
    session.setMode({
        path: "ace/mode/" + x[0].toLowerCase(),
        v: Date.now()
    })
    document.getElementById('type').disabled = false;
    var select = document.getElementById("type");
    for (var i = 0; i < select.length; i++) {
        var txt = select.options[i].dataset.id;
        if (txt !=x[1] ) {
            $(select.options[i]).attr('disabled', 'disabled').hide();
        } else {
            $(select.options[i]).removeAttr('disabled').show();
        }

    }
}
function upload(e) {
    e.preventDefault();
    document.getElementById('file').click();
}
function showme(e) {
    document.getElementById('img').src=URL.createObjectURL(e.files[0])
}