function viewModel()
{
    _this = this;
    this.photos = ko.observableArray()
}

$(document).ready(function ()
{
    $.ajax(
    {
        url: "/Photos/Home/",
        type: "GET",
        cache: false
    }).done(function (data)
    {
        var images = new viewModel();
        ko.applyBindings(images);
        for (var i = 0; i < data.length; i++)
        {
            var photo = data[i];
            images.photos.push(photo);
        }
    });    
});