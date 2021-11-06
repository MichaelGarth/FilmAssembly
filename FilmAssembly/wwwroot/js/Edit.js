var app = new Vue({
    el: "#app",
    data: {
        imageSelected: false
    },
    methods: {
        selectPoster: function (event) {
            const selectedElement = document.getElementsByClassName("poster selected-image");

            if (selectedElement[0] === event.currentTarget) {
                return;
            }

            const posters = document.getElementsByClassName("poster");
            Object.values(posters).forEach(element => element.classList.remove("selected-image"));

            event.currentTarget.classList.toggle("selected-image");

            document.getElementById("posterInput").value = event.currentTarget.src;
        },
        where: function(item) {
            return item = item;
        }
    }
})