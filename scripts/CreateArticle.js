$(document).ready(function () {
    tinymce.init({
        forced_root_block: '',
        selector: 'textarea.editable',
        menubar: false,
        height: '25px',
        toolbar: 'bold italic underline | alignleft aligncenter alignright alignjustify | formatselect fontselect fontsizeselect | bullist numlist | blockquote | undo redo | removeformat'
    });
    //tinymce.init({
    //    forced_root_block: '',
    //    selector: 'div.editable',
    //    inline: true,
    //    toolbar: 'bold italic underline | alignleft aligncenter alignright alignjustify | formatselect fontselect fontsizeselect | bullist numlist | blockquote | undo redo | removeformat'
    //});
});

function previewImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var imgPreview = document.getElementById("imgImage");
            imgPreview.src = e.target.result;
            imgPreview.style.height = "150px";
        }
        reader.readAsDataURL(input.files[0]);
    }
};