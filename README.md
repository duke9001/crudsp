# crudsp

http://www.tutorialsteacher.com/csharp/csharp-generics

<div class="modal fade" id="added" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">

            <div class="modal-body">
                <div class="display-4" style="text-align:center">DONE</div>
            </div>

        </div>
    </div>
</div>
<div class="modal fade" id="fail" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">

            <div class="modal-body">
                <div class="display-4" style="text-align:center">Why you can't add this package?</div>
                <hr/>
                 <ul>
                    <li> You are adding same package twice. </li>
                    <li> Already you add one package from this supplier. </li>
                    <li> This package don't support for your expected number of people. </li>
                </ul>
            </div>

        </div>
    </div>
</div>

@section scripts {

    <script>
        function onsuccess(result) {
            var jsonResult = JSON.parse(result);
            if (jsonResult == true)
            {
                $.ajax({
                    url: '@Url.Action("ViewSupplier/" + @Url.RequestContext.RouteData.Values["id"], "Supplier")',
                    type: 'GET',
                    cache: false,
                }).done(function (result) {
                    $('#added').modal('show');
                    setTimeout(function () {
                        $('#added').modal('hide')
                    }, 1000);
                });
            }
            else
            {
                $('#fail').modal('show');
                setTimeout(function () {
                    $('#fail').modal('hide')
                }, 8000);
            }
           
        }


        function onfailure(result) {
            $('#fail').modal('show');
            setTimeout(function () {
                $('#fail').modal('hide')
            }, 1000);

        }



    </script>

}
