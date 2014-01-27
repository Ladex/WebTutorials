(function ($) {
    
    $(document).ready(function () {
        createViewModel();
    });


    //=================================================================
    // SUMMARY 
    //=================================================================
    //
    // A simple Knockout.js overall page viewmodel, which simply acts as
    // a host for a number of CategoryViewModel items, which are displayed
    // within an embedded Twitter Boostrap Carousel. The first slide
    // will have its articles fetched on initial creation of the 
    // DemoMainPageViewModel. Subsequent slides will have their articles
    // loaded when the slide is requested. The loading of a slides articles
    // will only happen once. Essentially once the articles for a given slide
    // (category) are loaded the first time they are never asked to load again
    var DemoMainPageViewModel = function () {

        this.Categories = ko.observableArray();
        this.Loading = ko.observable(true);
        this.Loaded = ko.observable(false);
        this.HasArticle = ko.observable(false);
        this.CurrentArticle = ko.observable();

        this.SetArticle = function (article) {
            
            this.HasArticle(true);
            this.CurrentArticle(article);

            $('#articleModal').modal({
                keyboard: true
            }).modal('show');

        };

        this.Initialise = function(jsonCats) {

            this.Loading(false);
            this.Loaded(true);
            for (var i = 0; i < jsonCats.length; i++) {
                this.Categories.push(new CategoryViewModel(this, jsonCats[i]));
            }
            
            // Load the articles for the 1st slide (remember KO observableArray, 
            // are functions, so we need to call a function)
            this.Categories()[0].loadArticles();
        };

        // When the Category slide becomes active (requested to be shown via Nav menu)
        // load the Article(s) for the Category slide
        this.mainActionClicked = function (data, event) {
            var sourceElement = event.srcElement;
            var slideNumber = $(sourceElement).data('maincarouselslide');
            $('#mainCarousel').carousel(slideNumber);
            this.Categories()[slideNumber].loadArticles();
        };
    };


    //=================================================================
    // SUMMARY 
    //=================================================================
    //
    // A simple Knockout.js Category viewmodel. Each Category is 1 slide
    // of an embedded Twitter Boostrap Carousel. Each Category also lazy
    // loads an ObservableArray of Article items, when the Category slide
    // becomes active in the Twitter Boostrap Carousel. The lazy loading
    // of the Articles is only done once per slide.
    var CategoryViewModel = function (parent, jsonCat) {

        this.Id = jsonCat.Id;
        this.Parent = parent;
        this.LoadedArticles = ko.observable(false);
        this.Title = ko.observable(jsonCat.Title);
        this.Description = ko.observable(jsonCat.Description);
        this.Articles = ko.observableArray();
        var context = this;

        this.hasArticles = ko.computed(function() {
            return context.Articles().length > 0;
        }, this);

        
        // Shows a modal dialog of the clicked Article
        this.articleClicked = function (article) {
            context.Parent.SetArticle(article);
        };

        // Do the lazy load of the article. Basically only do it
        // when this fuction is called externally
        this.loadArticles = function () {

            
            if (this.LoadedArticles() == true) {
                return;
            }

            this.LoadedArticles(true);

            // Push stuff to articles
            $.when($.ajax('/api/article/GetAll/' + this.Id))
                .then(function (data, textStatus, jqXHR) {
                    for (var i = 0; i < data.length; i++) {
                        context.Articles.push(data[i]);

                        $('#img' + data[i].Id).tooltip({
                            title: data[i].ShortDescription,
                            trigger: 'hover focus'
                        });
                    }
            });
        };
    };
    


    //=================================================================
    // SUMMARY 
    //=================================================================
    //
    // Initialise Carousel, and listen to the carousel controls, that should
    // load the current category slides Article(s) when the slide becomes active.
    // The Article(s) are loaded using a REST based WebApi call
    function hookUpCarouselControls(demoVM) {

        $('#mainCarousel').carousel({
            interval: false,
            pause:true
        }).on('slid.bs.carousel', function (event) {

            var active = $(event.target)
                .find('.carousel-inner > .item.active');
            var from = active.index();
            demoVM.Categories()[from].loadArticles();
        });
    }



    //=================================================================
    // SUMMARY 
    //=================================================================
    //
    // Shows a busy indicator, and then does an initial AJAX call to get
    // the initial Categories using REST based WebApi call. When the initial
    // categories are fetched, a Twitter Boostrap Carousel is created, which
    // is done using a simple Knockout.js ViewModel
    function createViewModel() {     

        var demoVM = new DemoMainPageViewModel();
        ko.applyBindings(demoVM);

        $.when($.ajax("/api/category"))
            .then(function (data, textStatus, jqXHR) {
                demoVM.Initialise(data);
                hookUpCarouselControls(demoVM);
        });
    }
    
})(jQuery);