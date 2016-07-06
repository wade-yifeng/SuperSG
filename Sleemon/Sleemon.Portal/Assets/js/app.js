/**
 * @name AceApp
 * @description
 * # AceApp
 *
 * Main module of the application.
 */
var app = angular
  .module('AceApp', [
    'ngAnimate',
    'ngResource',
    'ngSanitize',
    'ngTouch',
	//'angular-loading-bar',
	'oc.lazyLoad',
	'ui.bootstrap',
	'ui.router',
	'ace.directives',
	'ngStorage'	
  ])
  .config(function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider/**, cfpLoadingBarProvider*/) {
	//cfpLoadingBarProvider.includeSpinner = true;
	
	$urlRouterProvider.otherwise('/dashboard');
	
    $stateProvider
	  .state('dashboard', {
		url: '/dashboard',
		title: 'Dashboard',
		icon: 'fa fa-tachometer',
        
		templateUrl: '/tests/pages/dashboard.html',
		controller: 'DashboardCtrl',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'angular-flot',
						serie: true,
						files: ['/Assets/components/Flot/jquery.flot.js',
						'/Assets/components/Flot/jquery.flot.pie.js',
						'/Assets/components/Flot/jquery.flot.resize.js',
						'/Assets/components/angular-flot/angular-flot.js']
					},
					{
						name: 'easypiechart',
						files: ['/Assets/components/easypiechart/angular.easypiechart.js']
					},
					{
						name: 'sparkline',
						files: ['/Assets/components/jquery.sparkline/index.js']
					},
					{
						name: 'AceApp',
						files: ['/Assets/js/controllers/pages/dashboard.js']
					}]);
			}]
		}
      })
	  
	  
	  .state('ui', {
		'abstract': true,
		//url: '/ui',
		title: 'UI & Elements',
		template: '<ui-view/>',
		
		icon: 'fa fa-desktop'
      })
	  .state('ui.typography', {
		url: '/typography',
		title: 'Typography',

		templateUrl: '/tests/pages/typography.html',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'prettify',
						files: ['/Assets/components/google-code-prettify/prettify.css',	'/Assets/components/google-code-prettify/src/prettify.js']
					}]);
			}]
		}
      })
	  .state('ui.elements', {
		url: '/elements',
		title: 'Elements',

		templateUrl: '/tests/pages/elements.html',
		controller: 'ElementsCtrl',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'bootbox',
						files: ['/Assets/components/bootbox.js/bootbox.js']
					},
					{
						name: 'easypiechart',
						files: ['/Assets/components/easypiechart/angular.easypiechart.js']
					},
					{
						name: 'gritter',
						files: ['/Assets/components/jquery.gritter/js/jquery.gritter.js']
					},
					{
						serie: true,
						name: 'ui.slider',
						files: ['/Assets/components/jquery-ui.custom/jquery-ui.custom.js', '/Assets/components/jqueryui-touch-punch/jquery.ui.touch-punch.js', '/Assets/components/angular-ui-slider/src/slider.js']
					},
					{
						serie: true,
						name: 'angularSpinner',
						files: ['/Assets/components/spin.js/spin.js',	'/Assets/components/spin.js/jquery.spin.js', '/Assets/components/angular-spinner/angular-spinner.js']
					},
					{
						name: 'AceApp',
						files: ['js/controllers/pages/elements.js']
					},
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: [
							'/Assets/components/jquery.gritter/css/jquery.gritter.css',
							'/Assets/components/jquery-ui.custom/jquery-ui.custom.css',
							'css/pages/elements.css'
							]
					}]);
			}]
		}
      })
	  .state('ui.buttons', {
		url: '/buttons',
		title: 'Buttons',

		templateUrl: '/tests/pages/buttons.html',
		controller: 'ButtonsCtrl',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						name: 'AceApp',
						files: ['js/controllers/pages/buttons.js']
					}]);
			}]
		}
      })
	  .state('ui.content-slider', {
		url: '/content-slider',
		title: 'Content Slider',

		templateUrl: '/tests/pages/content-slider.html',
		controller: 'ContentSliderCtrl',
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'ngAside',
						files: ['/Assets/components/angular-aside/dist/js/angular-aside.js']
					},
					{
						name: 'AceApp',
						files: ['js/controllers/pages/content-slider.js']
					},
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: ['/Assets/components/angular-aside/dist/css/angular-aside.css']
					}]);
			}]
		}
      })
	  .state('ui.treeview', {
		url: '/treeview',
		title: 'Treeview',

		templateUrl: '/tests/pages/treeview.html',
		controller: 'TreeviewCtrl',
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'treeControl',
						files: ['/Assets/components/angular-tree-control/angular-tree-control.js']
					},
					{
						name: 'AceApp',
						files: ['js/controllers/pages/treeview.js']
					}]);
			}]
		}
      })
	  .state('ui.nestable-list', {
		url: '/nestable-list',
		title: 'Nestable Lists',

		templateUrl: '/tests/pages/nestable-list.html',
		controller: 'NestableCtrl',
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'ngNestable',
						files: ['/Assets/components/jquery.nestable/jquery.nestable.js', '/Assets/components/angular-nestable/src/angular-nestable.js']
					},
					{
						name: 'AceApp',
						files: ['js/controllers/pages/nestable.js']
					}]);
			}]
		}
      })


	  
	  .state('table', {
		url: '/table',
		title: 'Tables',
		icon: 'fa fa-list',

		templateUrl: '/tests/pages/table.html',
		controller: 'TableCtrl',
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						serie: true,
						name: 'dataTables',
						files: ['/Assets/components/datatables/media/js/jquery.dataTables.js', '/Assets/components/datatables/jquery.dataTables.bootstrap.js', '/Assets/components/angular-datatables/dist/angular-datatables.js']
					},					
					{
						name: 'AceApp',
						files: ['js/controllers/pages/table.js']
					}]);
			}]
		}
      })
	  
	  
	  .state('form', {
		'abstract': true,
		//url: '/form',
		title: 'Forms',
		template: '<ui-view/>',
		icon: 'fa fa-pencil-square-o'
      })
	  .state('form.form-elements', {
		url: '/form-elements',
		title: 'Form Elements',

		templateUrl: '/tests/pages/form-elements.html',
		controller: 'FormCtrl',
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						serie: true,
						name: 'ui.slider',
						files: ['/Assets/components/jquery-ui.custom/jquery-ui.custom.js', '/Assets/components/jqueryui-touch-punch/jquery.ui.touch-punch.js', '/Assets/components/angular-ui-slider/src/slider.js']
					},
					
					{
						name: 'text_area',
						files: ['/Assets/components/jquery-inputlimiter/jquery.inputlimiter.js', '/Assets/components/angular-elastic/elastic.js']
					},
					
					{
						name: 'mask',
						files: ['/Assets/components/angular-ui-mask/dist/mask.js']
					},
					
					{
						name: 'chosen',
						files: ['/Assets/components/chosen/chosen.jquery.js', '/Assets/components/angular-chosen-localytics/chosen.js']
					},
					
					{
						name: 'spinner',
						files: ['/Assets/components/fuelux/js/spinbox.js']
					},

					{
						name: 'datepicker',
						files: ['/Assets/components/bootstrap-datepicker/dist/js/bootstrap-datepicker.js']
					},
					
					{
						serie: true,
						name: 'daterange',
						files: ['/Assets/components/moment/moment.js', '/Assets/components/bootstrap-daterangepicker/daterangepicker.js', '/Assets/components/angular-daterangepicker/js/angular-daterangepicker.js']
					},
					
					{
						name: 'timepicker',
						files: ['/Assets/components/bootstrap-timepicker/js/bootstrap-timepicker.js']
					},
					
					{
						serie: true,
						name: 'datetimepicker',
						files: ['/Assets/components/moment/moment.js', '/Assets/components/eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js']
					},
					
					{
						name: 'colorpicker',
						files: ['/Assets/components/angular-bootstrap-colorpicker/js/bootstrap-colorpicker-module.js', '/Assets/components/angular-bootstrap-colorpicker/css/colorpicker.css']
					},
					
					{
						name: 'knob',
						files: ['/Assets/components/jquery-knob/js/jquery.knob.js', '/Assets/components/angular-knob/src/angular-knob.js']
					},
					
					
					{
						serie: true,
						name: 'typeahead',
						files: ['/Assets/components/typeahead.js/dist/bloodhound.js', '/Assets/components/typeahead.js/dist/typeahead.jquery.js', '/Assets/components/angular-typeahead/angular-typeahead.js']
					},
					
					{
						name: 'multiselect',
						files: ['/Assets/components/angular-bootstrap-multiselect/angular-bootstrap-multiselect.js']
					},
					
					{
						name: 'select2',
						files: ['/Assets/components/ui-select/dist/select.js']
					},
					
					
					{
						name: 'AceApp',
						files: ['js/controllers/pages/form-elements.js']
					},

					
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: [
								'/Assets/components/chosen/chosen.css',
								'/Assets/components/jquery-ui.custom/jquery-ui.custom.css',
								'/Assets/components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css',
								'/Assets/components/bootstrap-timepicker/css/bootstrap-timepicker.css',
								'/Assets/components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.css',
								'/Assets/components/bootstrap-daterangepicker/daterangepicker.css',
								'/Assets/components/select2.v3/select2.css'
								]
					}]);
			}]
		}
      })
	  .state('form.form-wizard', {
		url: '/form-wizard',
		title: 'Form Wizard',

		templateUrl: '/tests/pages/form-wizard.html',
		controller: 'WizardCtrl',
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([{
						name: 'ngMessages',
						files: ['/Assets/components/angular-messages/angular-messages.js']
					},
					
					{
						name: 'wizard',
						files: ['/Assets/components/angular-wizard/dist/angular-wizard.js']
					},
					
					{
						name: 'mask',
						files: ['/Assets/components/angular-ui-mask/dist/mask.js']
					},
					
					{
						name: 'chosen',
						files: ['/Assets/components/chosen/chosen.jquery.js', '/Assets/components/angular-chosen-localytics/chosen.js']
					},
					
					{
						name: 'AceApp',
						files: ['js/controllers/pages/form-wizard.js']
					},
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: ['/Assets/components/chosen/chosen.css']
					}]);
			}]
		}
		
      })
	  .state('form.wysiwyg', {
		url: '/wysiwyg',
		title: 'Wysiwyg & Markdown',

		templateUrl: '/tests/pages/wysiwyg.html',
		controller: 'WysiwygCtrl',

		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						name: 'wysiwyg',
						serie: true,
						files: [
								'/Assets/components/bootstrap/js/dropdown.js',//for wysiwyg dropdowns
								'/Assets/components/jquery.hotkeys/index.js',
								'/Assets/components/bootstrap-wysiwyg/bootstrap-wysiwyg.js']
					},
					
					{
						name: 'markdown',
						files: ['/Assets/components/markdown/lib/markdown.js','/Assets/components/bootstrap-markdown/js/bootstrap-markdown.js']
					},
					
					{
						name: 'AceApp',	
						files: ['js/controllers/pages/wysiwyg.js']
					}
				]);
			}]
		}	


      })
	  .state('form.dropzone', {
		url: '/dropzone',
		title: 'Dropzone File Upload',

		templateUrl: '/tests/pages/dropzone.html',
		controller: 'DropzoneCtrl',

		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						name: 'dropzone',
						serie: true,
						files: ['/Assets/components/dropzone/dist/dropzone.js', '/Assets/components/angular-dropzone/lib/angular-dropzone.js']
					},
					
					{
						name: 'AceApp',	
						files: ['js/controllers/pages/dropzone.js']
					},
					
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: ['/Assets/components/dropzone/dist/dropzone.css']
					}
				]);
			}]
		}
      })
	  
	  
	  .state('widgets', {
		url: '/widgets',
		title: 'Widgets',
		icon: 'fa fa-list-alt',

		templateUrl: '/tests/pages/widgets.html',
		controller: 'WidgetCtrl',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						serie: true,
						name: 'sortable',
						files: ['/Assets/components/jquery-ui.custom/jquery-ui.custom.js', '/Assets/components/jqueryui-touch-punch/jquery.ui.touch-punch.js']
					},
										
					{
						name: 'AceApp',	
						files: ['js/controllers/pages/widgets.js']
					}
				]);
			}]
		}
      })
	  
	  .state('calendar', {
		url: '/calendar',
		title: 'Calendar',

		templateUrl: '/tests/pages/calendar.html',
		controller: 'CalendarCtrl',
		
		icon: 'fa fa-calendar',
		badge: '<i class="ace-icon fa fa-exclamation-triangle red bigger-130"></i>',
		'badge-class': 'badge-transparent',
		tooltip: '2&nbsp;Important&nbsp;Events',
		'tooltip-class': 'tooltip-error',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						serie: true,
						name: 'calendar',
						files: ['/Assets/components/moment/moment.js', '/Assets/components/fullcalendar/dist/fullcalendar.js', '/Assets/components/angular-ui-calendar/src/calendar.js']
					},
					
					{
						serie: true,
						name: 'drag',
						files: ['/Assets/components/jquery-ui.custom/jquery-ui.custom.js', '/Assets/components/jqueryui-touch-punch/jquery.ui.touch-punch.js', '/Assets/components/angular-dragdrop/src/angular-dragdrop.js']
					},			
					
					{
						name: 'bootbox',
						files: ['/Assets/components/bootbox.js/bootbox.js']
					},
					
					{
						name: 'AceApp',	
						files: ['js/controllers/pages/calendar.js']
					},
					
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: ['/Assets/components/fullcalendar/dist/fullcalendar.css']
					}
				]);
			}]
		}
      })
	  
	  .state('gallery', {
		url: '/gallery',
		title: 'Gallery',
		
		icon: 'fa fa-picture-o',

		templateUrl: '/tests/pages/gallery.html',
		controller: 'GalleryCtrl',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
					{
						name: 'gallery',
						files: ['/Assets/components/jquery-colorbox/jquery.colorbox.js', '/Assets/components/angular-colorbox/js/angular-colorbox.js']
					},
					
					{
						name: 'AceApp',	
						files: ['js/controllers/pages/gallery.js']
					},
					
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: ['/Assets/components/jquery-colorbox/example1/colorbox.css']
					}
				]);
			}]
		}
		
      })

	  .state('more', {
		'abstract': true,
		//url: '/more',
		title: 'More Pages',
		template: '<ui-view/>',
		icon: 'fa fa-tag'
      })
	  .state('more.profile', {
		url: '/profile',
		title: 'User Profile',

		templateUrl: '/tests/pages/profile.html',
		controller: 'ProfileCtrl',
		
		resolve: {
			lazyLoad: ['$ocLazyLoad', function($ocLazyLoad) {
				return $ocLazyLoad.load([
		
					{
						name: 'datepicker',
						serie: true,
						files: ['/Assets/components/moment/moment.js', '/Assets/components/bootstrap-datepicker/dist/js/bootstrap-datepicker.js']
					},
					
					{
						name: 'spinner',
						files: ['/Assets/components/fuelux/js/spinbox.js']
					},
					
					{
						serie: true,
						name: 'jquery-ui',
						files: ['/Assets/components/jquery-ui.custom/jquery-ui.custom.js', '/Assets/components/jqueryui-touch-punch/jquery.ui.touch-punch.js']
					},
					
					{
						name: 'x-editable',	
						serie: true,
						files: ['/Assets/components/x-editable/bootstrap-editable.js', '/Assets/components/x-editable/ace-editable.js']
					},
					
					{
						name: 'AceApp',	
						files: ['js/controllers/pages/profile.js']
					},
					
					{
						name: 'AceApp',
						insertBefore: '#main-ace-style',
						files: ['/Assets/components/jquery-ui.custom/jquery-ui.custom.css',
								'/Assets/components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.css',
								'/Assets/components/x-editable/bootstrap-editable.css']
					}
				]);
			}]
		}
      })
	  .state('more.even', {
		'abstract': true,
		  
		title: 'Even More',
		template: '<ui-view/>'

      })
	  
	  .state('more.even.error', {
		url: '/error',
		title: 'Error',
		templateUrl: '/tests/pages/error.html'
      })
	  
	  
	  /**
	  .state('more.inbox', {
		url: '/inbox',
		title: 'Inbox',

		templateUrl: '/tests/pages/inbox.html'
      })
	  .state('more.pricing', {
		url: '/pricing',
		title: 'Pricing',

		templateUrl: '/tests/pages/pricing.html'
      })
	  .state('more.invoice', {
		url: '/invoice',
		title: 'Invoice',

		templateUrl: '/tests/pages/invoice.html'
      })
	  .state('more.timeline', {
		url: '/timeline',
		title: 'Timeline',

		templateUrl: '/tests/pages/timeline.html'
      })
	  
	  
	  .state('other', {
		'abstract': true,
		title: 'Other Pages',
		template: '<ui-view/>',
		icon: 'fa fa-file-o',
		badge: '5', 
		'badge-class': 'badge-primary'
      })
	  .state('other.faq', {
		url: '/faq',
		title: 'FAQ',

		templateUrl: '/tests/pages/faq.html'
      })
	  .state('other.error', {
		url: '/error',
		title: 'Error',

		templateUrl: '/tests/pages/error.html'
      })
	  .state('other.grid', {
		url: '/grid',
		title: 'Grid',

		templateUrl: '/tests/pages/grid.html'
      })
	  .state('other.blank', {
		url: '/blank',
		title: 'Blank',

		templateUrl: '/tests/pages/blank.html'
      })
	  */
  })
  .run(function($rootScope) {

  });
