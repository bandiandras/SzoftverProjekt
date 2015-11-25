define([], function () {
    var vm = {
        startTimer: startTimer,
        displayNextImage: displayNextImage,
        displayName: 'Durandal 451',        
        features: [
            'OWIN OAuth support (with 3rd party authentication providers)',
            'Remember user using local storage',
            'Secure routing',
            'Customise views for the user dependent on user roles'
        ],
        references: [
            {name:'Durandal', url: 'http://durandaljs.com/'},
            {name:'Bootstrap', url: 'http://getbootstrap.com/'},
            {name:'ASP.NET OWIN', url: 'http://www.asp.net/vnext/overview/authentication'}            
        ]

    };        
    function displayNextImage() {
        x = (x === images.length - 1) ? 0 : x + 1;
        document.getElementById("img").src = images[x];
    }

    function displayPreviousImage() {
        x = (x <= 0) ? images.length - 1 : x - 1;
        document.getElementById("img").src = images[x];
    }

    function startTimer() {
        setInterval(displayNextImage, 2000);
    }

    var images = [], x = -1;
    images[0] = "Content/images/asd.png";
    images[1] = "Content/images/iconOriginal.png";
    images[2] = "Content/images/ios-startup-image-landscape.png";
    return vm;
});