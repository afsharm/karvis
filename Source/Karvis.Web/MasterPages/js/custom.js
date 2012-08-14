$(function() { 

/*ios safari fix*/
    var viewportmeta = document.querySelector && document.querySelector('meta[name="viewport"]'),
        ua = navigator.userAgent,
        gestureStart = function() {
            viewportmeta.content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6";
        },
        scaleFix = function() {
            if (viewportmeta && /iPhone|iPad/.test(ua) && !/Opera Mini/.test(ua)) {
                viewportmeta.content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
                document.addEventListener("gesturestart", gestureStart, false);
            }
        };
    scaleFix();
    

/*ie7-8*/
    if ($.browser.msie) {
        if ($.browser.version == 7) $('body').addClass('ie7');
        if ($.browser.version == 8) $('body').addClass('ie8');
        if ($('body').hasClass('ie8') || $('body').hasClass('ie7')) {
            $('#footer-menu li:last-child,.post-info li:last-child,ul#footer-menu li:last-child,.breadcrumbs li:last-child,.widget_twitter ul li:last-child,table tr td:last-child,,table th:last-child,.carousel .slides ul li:last-child').addClass('last');

            $('table th:nth-child(2n),.price-item ul li:nth-child(2n)').addClass('nth-2n');
            $('table tr:nth-child(2n+3)').addClass('nth-2n_3');
        }
    };
    

/*Hovers*/
    $('.proj-img').has('a').append("<span></span>");
    $('.proj-img a').not('.prettyPhoto').parent().addClass("ef-hov-link");
    $('.proj-img').hover(function() {
        $(this).find('a').stop().animate({
            "top": "0",
            "left": "0",
            "bottom": "0",
            "right": "0",
            "opacity": "0.5"
        }, 'fast');
        $(this).has('a').find('span').stop().animate({
            "left": "10px",
            "bottom": "10px"
        }, 300);
    }, function() {
        $(this).find('a').stop().animate({
            "top": "10%",
            "left": "10%",
            "bottom": "10%",
            "right": "10%",
            "opacity": "0"
        }, 'fast');
        $(this).has('a').find('span').stop().animate({
            "bottom": "-60px",
            "left": "-60px"
        }, '100');
    });

    $('.social-bar a.soc').prepend('<span></span>');
    $('.social-bar a.soc').hover(function() {
        $(this).find('span').stop().animate({
            height: "100%",
            opacity: "1"
        }, 'fast');
    }, function() {
        $(this).find('span').stop().animate({
            height: "0",
            opacity: "0"
        }, 'fast');
    });
    

/*Dropdown menu */
    $('ul.sf-menu').superfish({
        delay: 200,
        animation: {
            opacity: 'show'
        },
        speed: 500
    });
    $('ul.sf-menu').mobileMenu({
        defaultText: 'Navigate to...',
        className: 'ef-select-menu',
        subMenuClass: 'sub-menu',
        subMenuDash: '&ndash;'
    });
    

/*jPreloader*/
    $(".proj-img").preloader();
    

/*Portfolio filter*/
    $('ul.ef-portfolio li.ef-item').append('<span class="cover"></span>');
    $('ul#filter a').click(function() {
        $('ul#filter .current').removeClass('current');
        $(this).parent().addClass('current');
        var filterVal = $(this).text().toLowerCase().replace(' ', '-');
        if (filterVal == 'all') {
            $('ul.ef-portfolio li.ef-item.portf-hidden').find('.cover').animate({
                "opacity": "0"
            }).css({
                display: "none"
            });
        } else {
            $('ul.ef-portfolio li').each(function() {
                if (!$(this).hasClass(filterVal)) {
                    $(this).addClass('portf-hidden').find('.cover').css({
                        display: "block"
                    }).animate({
                        "opacity": "0.9"
                    }, 'slow');
                } else {
                    $(this).removeClass('portf-hidden').find('.cover').animate({
                        "opacity": "0"
                    }).css({
                        display: "none"
                    });
                }
            });
        }
        return false;
    });
    

/*prettyPhoto*/
    $("a[class^='prettyPhoto']").prettyPhoto({
        theme: 'light_rounded'
    }); 
    
    
/*jFlickfeed*/
    $('.jflickr').jflickrfeed({
        limit: 9,
        qstrings: {
            id: '12300506@N03'
        },
        itemTemplate: '<li>' + '<a href="{{image}}" title="{{title}}">' + '<img src="{{image_s}}" alt="{{title}}" />' + '<span></span>' + '</a>' + '</li>'
    }, function(data) {
    
    });
    
    
/*jTweet*/
    $(".tweet").tweet({
        count: 2,
        avatar_size: 32,
        username: "evgenyfireform",
        loading_text: "Loading tweets",
        refresh_interval: 60
    }).bind("loaded", function() {
        $(this).find("a").attr("target", "_blank");
    });
    
    
/*Tabs*/
    $('.ef-tabs').tabs({
        fx: {
            opacity: 'show'
        },
        selected: 0
    });
    

/*Toggle box*/
    $('.ef-toggle-box').addClass('toggle-icn');
    $('.ef-toggle-box .toggle-content').css("display", "none");
    $('.ef-toggle-box li:first-child').addClass('open').find('.toggle-content').css("display", "block");
    $('.ef-toggle-box .toggle-head').click(function() {
        $(this).next('.toggle-content').toggle('blind', 200);
        $(this).parent().toggleClass('open');
    });
    
    
/*ScrollToTop*/
    jQuery.fn.topLink = function(settings) {
        settings = jQuery.extend({
            min: 1,
            fadeSpeed: 200,
            ieOffset: 50
        }, settings);
        return this.each(function() {
            var el = $(this);
            el.css('display', 'none');
            $(window).scroll(function() {
                if (!jQuery.support.hrefNormalized) {
                    el.css({
                        'position': 'absolute',
                        'top': $(window).scrollTop() + $(window).height() - settings.ieOffset
                    });
                }
                if ($(window).scrollTop() >= settings.min) {
                    el.fadeIn(settings.fadeSpeed);
                } else {
                    el.fadeOut(settings.fadeSpeed);
                }
            });
        });
    };
    $('a.totop').topLink({
        min: 200,
        fadeSpeed: 500
    });
    $('a.totop').click(function(e) {
        e.preventDefault();
        $.scrollTo(0, 800);
    });
    
    
/*Accordeon*/
    $(".accordion").accordion({
        autoHeight: false,
        navigation: true
    });
    
    
/*Quick Contact on top*/
    $('a.contact-bar-tab').hover(function() {
        $(this).parent().parent().toggleClass("close");
    });
    var expand = $('#contact-wrap .qcontact');
    expand.css("display", "none");
    $('a.contact-bar-tab').click(function() {
        if (expand.is(":visible")) {
            expand.slideUp(500, 'easeInOutExpo');
            $(this).removeClass("close");
            $(this).parent().parent().parent().removeClass("close");
        } else {
            expand.slideDown(500, 'easeInOutExpo');
            $(this).addClass("close");
            $(this).parent().parent().parent().addClass("close");
        };
        return false;
    });
    
    
/*jQuery init*/
    $('body').removeClass('sg-nojs');
    $(".proj-img").fitVids();
    
    
/*goMap*/
    $(".ef-map").goMap({
		maptype:"ROADMAP",
		address: 'Baranovichi, Belarus',	/*Center map by address*/
		zoom: 3, 							/*Default Zoom level*/
		scaleControl: true,
		navigationControl: true, 
        scrollwheel: false, 
        mapTypeControl: true,
        mapTypeControlOptions: { 
            position: 'RIGHT', 
            style: 'DROPDOWN_MENU' 
        },
        markers: [{  
            latitude: 53.12112,
            longitude: 25.98335, 
            html: { 
                content: 'First office', 
                popup: false 
            }
        },{  
            latitude: 61.52401, 
            longitude: 105.31876, 
            html: 'Second office', 
            popup: false 
            
        },{  
            latitude: 60.12816, 
            longitude: 18.64350, 
            html: 'Third office', 
            popup: false
        }],
        
        hideByClick: true,
        icon: 'images/home.png', 
        addMarker: false
        /* Other plugin options see here: http://www.pittss.lv/jquery/gomap/examples.php */
    }); 
   
});

/*Window onload*/

$(window).load(function() {


/*Align height*/
    $(".col-height > div,.col-height > li.ef-item").equalHeight();
    $(".ef-extras .acc-head").equalHeight();


/*Main homepage slider*/
    $('#main-slider').flexslider({
        animation: "slide",
        easing: "easeInCubic",
        slideshowSpeed: 4000,
        pauseOnHover: true,
        start: function() {
        
    		$('.slider-preloader').css({
                display: 'none'
            });
            
            $('#main-slider').css({visibility:'visible'}).find('img').animate({opacity:'1'},500);
             
            $('.flex-caption').css({
                display: 'block'
            }).stop().animate({
                bottom: '30px'
            });
        },
        before: function() {
            $('.flex-caption').stop().css({
                display: 'none',
                bottom: '0',
                opacity: '0'
            });
        },
        after: function() {
            $('.flex-caption').css({
                display: 'block'
            }).stop().animate({
                bottom: '30px',
                opacity: '0.9'
            });
        }
    });
    
    $('#main-slider').hover(function() {
		
		$(this).find('.cap-title').addClass("cap-hov");
        $(this).find('.flex-direction-nav a').css({
            opacity: '1'
        });
        }, function() {
        	$(this).find('.cap-title').removeClass("cap-hov");
        	$(this).find('.flex-direction-nav a').css({
        	    opacity: '0'
        });
    });
    
    $('.carousel,.post-slider').hover(function() {
    	var navTop = Math.round($('.carousel ul.slides li .proj-img').height()/2);
    	
    	$(this).css({backgroundImage:'none'});
    	
    	$('.carousel .flex-direction-nav li a').css({top:navTop});
    
        $(this).find('.flex-direction-nav a').css({
            opacity: '1'
        });
        
        $(this).find('a.flex-prev').stop().animate({
            left: '-15px'
        });
        
        $(this).find('a.flex-next').stop().animate({
            right: '-15px'
        });
        
    }, function() {
        $(this).find('.flex-direction-nav a').css({
            opacity: '0'
        });
        
        $(this).find('a.flex-prev').stop().animate({
            left: '0'
        });
        
        $(this).find('a.flex-next').stop().animate({
            right: '0'
        });
    });
    
/*Post slider*/
    $('.post-slider').flexslider({
        slideshow: false,
        animation: "slide",
        controlNav: true,
        directionNav: true,
        start: function() {
        	$('.post-slider').find('img').css({visibility:'visible'}).delay(500).animate({opacity:'1'},300);
        }
    });
    

/*Recent projects carousel*/
    $('.carousel').flexslider({
        slideshow: false,
        animation: "slide",
        controlNav: false,
        directionNav: true,
        start: function() {
            $('.carousel').fadeIn(500);
        }
    });

    
/*Flickr hover*/
    $('.jflickr li a').hover(function() {
        $(this).find('span').stop().animate({
            opacity: '0.4'
        }, 100);
    }, function() {
        $(this).find('span').stop().animate({
            opacity: '0'
        }, 300);
    });
});