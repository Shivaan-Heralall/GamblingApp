//body preloader
$(window).on('load', function () {
  setTimeout(function () { // allowing 3 secs to fade out loader
    $('.page-loader').fadeOut('slow');
  }, 3500);
});

//On Scroll Header fixed to top
$(window).scroll(function () {
  if ($(window).scrollTop() >= 50) {
    $('header').addClass('fixed-top smooth');
  }
  else {
    $('header').removeClass('fixed-top smooth');
  }
});

// On hover open dropdown menu & clickable parent link
jQuery(function ($) {
  if ($(window).width() > 320) {
    $('.navbar .dropdown').hover(function () {
      $(this).find('.dropdown-menu').first().stop(true, true).delay(20).slideDown();

    }, function () {
      $(this).find('.dropdown-menu').first().stop(true, true).delay(20).slideUp();

    });

    $('.navbar .dropdown > a').click(function () {
      location.href = this.href;
    });

  }
});

//menu toggel
$('.menu-toggle').on('click', function () {
  $('.btn-wrapper').toggleClass('menu--is-revealed');
});

//  On Scroll back to top
$(window).on('scroll', function () {

  // Back Top Button
  if ($(window).scrollTop() > 500) {
    $('.scrollup').addClass('back-top');
  } else {
    $('.scrollup').removeClass('back-top');
  }
});
// On Click Section Switch
// used for back-top
$('[data-type="section-switch"]').on('click', function () {
  if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
    var target = $(this.hash);
    if (target.length > 0) {

      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
      $('html,body').animate({
        scrollTop: target.offset().top
      }, 1000);
      return false;
    }
  }
});

//partner slider
$('#carouselfeatures').owlCarousel({
  loop: true,
  margin: 30,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  nav: true,
  dots: false,
  navText: ["<i class='bi bi-arrow-left-short'></i>", "<i class='bi bi-arrow-right-short'></i>"],
  responsiveClass: true,
  responsive: {
    0: {
      items: 1
    },
    600: {
      items: 2
    },
    1000: {
      items: 4
    }
  }
});

// hero pic effect
const bg = document.querySelector('.hero-image');
const windowWidth = window.innerWidth / 5;
const windowHeight = window.innerHeight / 5;

bg.addEventListener('mousemove', (e) => {
  const mouseX = e.clientX / windowWidth;
  const mouseY = e.clientY / windowHeight;

  bg.style.transform = `translate3d(-${mouseX}%, -${mouseY}%, 0)`;
});

// video frame open popup
jQuery(document).ready(function ($) {
  // Define App Namespace
  var popup = {
    // Initializer
    init: function () {
      popup.popupVideo();
    },
    popupVideo: function () {

      $('.video_model').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false,
        gallery: {
          enabled: true
        }
      });

      // Image Gallery Popup
      $('.gallery_container').magnificPopup({
        delegate: 'a',
        type: 'image',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false,
        gallery: {
          enabled: true
        }
      });

    }
  };
  popup.init($);
});


