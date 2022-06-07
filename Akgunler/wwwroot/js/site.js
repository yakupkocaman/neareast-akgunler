(function ($) {
	'use strict';
	// Horizontal menu toggle fuction for mobile
	$(".navbar.horizontal-layout .navbar-menu-wrapper .navbar-toggler").on("click", function () {
		$(".navbar.horizontal-layout").toggleClass("header-toggled");
	});

	//datatable link
	$('.dataTable tr').click(function () {
		var links = [];
		debugger;
		Array.from($(this).find('a')).forEach(function (item) {
			var link = $(item).attr('href');
			debugger;
			if (links.indexOf(link) == -1) {
				links.push(link);
			}
		});

		if (links.length == 1) {
			location.href = links[0];
		}
	});
})(jQuery);


(function ($) {
	'use strict';
	$(function () {
		var body = $('body');
		var contentWrapper = $('.content-wrapper');
		var scroller = $('.container-scroller');
		var footer = $('.footer');
		var sidebar = $('.sidebar');

		//Add active class to nav-link based on url dynamically
		//Active class can be hard coded directly in html file also as required
		var current = location.pathname.split("/").slice(-1)[0].replace(/^\/|\/$/g, '');
		$('.nav li a', sidebar).each(function () {
			var $this = $(this);
			if (current === "") {
				//for root url
				if ($this.attr('href').indexOf("index.html") !== -1) {
					$(this).parents('.nav-item').last().addClass('active');
					if ($(this).parents('.sub-menu').length) {
						$(this).closest('.collapse').addClass('show');
						$(this).addClass('active');
					}
				}
			} else {
				//for other url
				if ($this.attr('href').indexOf(current) !== -1) {
					$(this).parents('.nav-item').last().addClass('active');
					if ($(this).parents('.sub-menu').length) {
						$(this).closest('.collapse').addClass('show');
						$(this).addClass('active');
					}
				}
			}
		})

		//Close other submenu in sidebar on opening any

		sidebar.on('show.bs.collapse', '.collapse', function () {
			sidebar.find('.collapse.show').collapse('hide');
		});


		//Change sidebar and content-wrapper height
		applyStyles();

		function applyStyles() {
			//Applying perfect scrollbar
			if (!body.hasClass("rtl")) {
				if ($('.settings-panel .tab-content .tab-pane.scroll-wrapper').length) {
					const settingsPanelScroll = new PerfectScrollbar('.settings-panel .tab-content .tab-pane.scroll-wrapper');
				}
				if ($('.chats').length) {
					const chatsScroll = new PerfectScrollbar('.chats');
				}
				if ($('.scroll-container').length) {
					const ScrollContainer = new PerfectScrollbar('.scroll-container');
				}
				if (body.hasClass("sidebar-fixed")) {
					var fixedSidebarScroll = new PerfectScrollbar('#sidebar .nav');
				}
			}
		}

		$('[data-toggle="minimize"]').on("click", function () {
			if ((body.hasClass('sidebar-toggle-display')) || (body.hasClass('sidebar-absolute'))) {
				body.toggleClass('sidebar-hidden');
			} else {
				body.toggleClass('sidebar-icon-only');
			}
		});

		$(".navbar.horizontal-layout .navbar-menu-wrapper .navbar-toggler").on("click", function () {
			$(".navbar.horizontal-layout .nav-bottom").toggleClass("header-toggled");
		});

		//checkbox and radios
		$(".form-check .form-check-label,.form-radio .form-check-label").not(".todo-form-check .form-check-label").append('<i class="input-helper"></i>');

		$('.uppercase-field input').keyup(function () {
			console.log($(this).val())
			$(this).val($(this).val().toUpperCase());
		});
	});
})(jQuery);

function url_slug(s, opt) {
	s = String(s);
	opt = Object(opt);

	var defaults = {
		'delimiter': '-',
		'limit': undefined,
		'lowercase': true,
		'replacements': {},
		'transliterate': (typeof (XRegExp) === 'undefined') ? true : false
	};

	// Merge options
	for (var k in defaults) {
		if (!opt.hasOwnProperty(k)) {
			opt[k] = defaults[k];
		}
	}

	var char_map = {
		// Latin
		'À': 'A', 'Á': 'A', 'Â': 'A', 'Ã': 'A', 'Ä': 'A', 'Å': 'A', 'Æ': 'AE', 'Ç': 'C',
		'È': 'E', 'É': 'E', 'Ê': 'E', 'Ë': 'E', 'Ì': 'I', 'Í': 'I', 'Î': 'I', 'Ï': 'I',
		'Ð': 'D', 'Ñ': 'N', 'Ò': 'O', 'Ó': 'O', 'Ô': 'O', 'Õ': 'O', 'Ö': 'O', 'Ő': 'O',
		'Ø': 'O', 'Ù': 'U', 'Ú': 'U', 'Û': 'U', 'Ü': 'U', 'Ű': 'U', 'Ý': 'Y', 'Þ': 'TH',
		'ß': 'ss',
		'à': 'a', 'á': 'a', 'â': 'a', 'ã': 'a', 'ä': 'a', 'å': 'a', 'æ': 'ae', 'ç': 'c',
		'è': 'e', 'é': 'e', 'ê': 'e', 'ë': 'e', 'ì': 'i', 'í': 'i', 'î': 'i', 'ï': 'i',
		'ð': 'd', 'ñ': 'n', 'ò': 'o', 'ó': 'o', 'ô': 'o', 'õ': 'o', 'ö': 'o', 'ő': 'o',
		'ø': 'o', 'ù': 'u', 'ú': 'u', 'û': 'u', 'ü': 'u', 'ű': 'u', 'ý': 'y', 'þ': 'th',
		'ÿ': 'y',

		// Latin symbols
		'©': '(c)',

		// Greek
		'Α': 'A', 'Β': 'B', 'Γ': 'G', 'Δ': 'D', 'Ε': 'E', 'Ζ': 'Z', 'Η': 'H', 'Θ': '8',
		'Ι': 'I', 'Κ': 'K', 'Λ': 'L', 'Μ': 'M', 'Ν': 'N', 'Ξ': '3', 'Ο': 'O', 'Π': 'P',
		'Ρ': 'R', 'Σ': 'S', 'Τ': 'T', 'Υ': 'Y', 'Φ': 'F', 'Χ': 'X', 'Ψ': 'PS', 'Ω': 'W',
		'Ά': 'A', 'Έ': 'E', 'Ί': 'I', 'Ό': 'O', 'Ύ': 'Y', 'Ή': 'H', 'Ώ': 'W', 'Ϊ': 'I',
		'Ϋ': 'Y',
		'α': 'a', 'β': 'b', 'γ': 'g', 'δ': 'd', 'ε': 'e', 'ζ': 'z', 'η': 'h', 'θ': '8',
		'ι': 'i', 'κ': 'k', 'λ': 'l', 'μ': 'm', 'ν': 'n', 'ξ': '3', 'ο': 'o', 'π': 'p',
		'ρ': 'r', 'σ': 's', 'τ': 't', 'υ': 'y', 'φ': 'f', 'χ': 'x', 'ψ': 'ps', 'ω': 'w',
		'ά': 'a', 'έ': 'e', 'ί': 'i', 'ό': 'o', 'ύ': 'y', 'ή': 'h', 'ώ': 'w', 'ς': 's',
		'ϊ': 'i', 'ΰ': 'y', 'ϋ': 'y', 'ΐ': 'i',

		// Turkish
		'Ş': 'S', 'İ': 'I', 'Ç': 'C', 'Ü': 'U', 'Ö': 'O', 'Ğ': 'G',
		'ş': 's', 'ı': 'i', 'ç': 'c', 'ü': 'u', 'ö': 'o', 'ğ': 'g',

		// Russian
		'А': 'A', 'Б': 'B', 'В': 'V', 'Г': 'G', 'Д': 'D', 'Е': 'E', 'Ё': 'Yo', 'Ж': 'Zh',
		'З': 'Z', 'И': 'I', 'Й': 'J', 'К': 'K', 'Л': 'L', 'М': 'M', 'Н': 'N', 'О': 'O',
		'П': 'P', 'Р': 'R', 'С': 'S', 'Т': 'T', 'У': 'U', 'Ф': 'F', 'Х': 'H', 'Ц': 'C',
		'Ч': 'Ch', 'Ш': 'Sh', 'Щ': 'Sh', 'Ъ': '', 'Ы': 'Y', 'Ь': '', 'Э': 'E', 'Ю': 'Yu',
		'Я': 'Ya',
		'а': 'a', 'б': 'b', 'в': 'v', 'г': 'g', 'д': 'd', 'е': 'e', 'ё': 'yo', 'ж': 'zh',
		'з': 'z', 'и': 'i', 'й': 'j', 'к': 'k', 'л': 'l', 'м': 'm', 'н': 'n', 'о': 'o',
		'п': 'p', 'р': 'r', 'с': 's', 'т': 't', 'у': 'u', 'ф': 'f', 'х': 'h', 'ц': 'c',
		'ч': 'ch', 'ш': 'sh', 'щ': 'sh', 'ъ': '', 'ы': 'y', 'ь': '', 'э': 'e', 'ю': 'yu',
		'я': 'ya',

		// Ukrainian
		'Є': 'Ye', 'І': 'I', 'Ї': 'Yi', 'Ґ': 'G',
		'є': 'ye', 'і': 'i', 'ї': 'yi', 'ґ': 'g',

		// Czech
		'Č': 'C', 'Ď': 'D', 'Ě': 'E', 'Ň': 'N', 'Ř': 'R', 'Š': 'S', 'Ť': 'T', 'Ů': 'U',
		'Ž': 'Z',
		'č': 'c', 'ď': 'd', 'ě': 'e', 'ň': 'n', 'ř': 'r', 'š': 's', 'ť': 't', 'ů': 'u',
		'ž': 'z',

		// Polish
		'Ą': 'A', 'Ć': 'C', 'Ę': 'e', 'Ł': 'L', 'Ń': 'N', 'Ó': 'o', 'Ś': 'S', 'Ź': 'Z',
		'Ż': 'Z',
		'ą': 'a', 'ć': 'c', 'ę': 'e', 'ł': 'l', 'ń': 'n', 'ó': 'o', 'ś': 's', 'ź': 'z',
		'ż': 'z',

		// Latvian
		'Ā': 'A', 'Č': 'C', 'Ē': 'E', 'Ģ': 'G', 'Ī': 'i', 'Ķ': 'k', 'Ļ': 'L', 'Ņ': 'N',
		'Š': 'S', 'Ū': 'u', 'Ž': 'Z',
		'ā': 'a', 'č': 'c', 'ē': 'e', 'ģ': 'g', 'ī': 'i', 'ķ': 'k', 'ļ': 'l', 'ņ': 'n',
		'š': 's', 'ū': 'u', 'ž': 'z'
	};

	// Make custom replacements
	for (var k in opt.replacements) {
		s = s.replace(RegExp(k, 'g'), opt.replacements[k]);
	}

	// Transliterate characters to ASCII
	if (opt.transliterate) {
		for (var k in char_map) {
			s = s.replace(RegExp(k, 'g'), char_map[k]);
		}
	}

	// Replace non-alphanumeric characters with our delimiter
	var alnum = (typeof (XRegExp) === 'undefined') ? RegExp('[^a-z0-9]+', 'ig') : XRegExp('[^\\p{L}\\p{N}]+', 'ig');
	s = s.replace(alnum, opt.delimiter);

	// Remove duplicate delimiters
	s = s.replace(RegExp('[' + opt.delimiter + ']{2,}', 'g'), opt.delimiter);

	// Truncate slug to max. characters
	s = s.substring(0, opt.limit);

	// Remove delimiter from ends
	s = s.replace(RegExp('(^' + opt.delimiter + '|' + opt.delimiter + '$)', 'g'), '');

	return opt.lowercase ? s.toLowerCase() : s;
}

function onCountryChanged(e) {
	$("#provinceSelect").dxSelectBox("instance").getDataSource().filter([$("#countrySelect").dxSelectBox("instance").option("value")]);
	$("#provinceSelect").dxSelectBox("instance").getDataSource().reload();
}


function onProvinceChanged(e) {
	$("#districtSelect").dxSelectBox("instance").getDataSource().filter([$("#provinceSelect").dxSelectBox("instance").option("value")]);
	$("#districtSelect").dxSelectBox("instance").getDataSource().reload();
}

var selectedCountryId, selectedProvinceId, selectedDistrictId;

function initAddressSelects(countryId, provinceId, districtId) {
	selectedCountryId = countryId;
	selectedProvinceId = provinceId;
	selectedDistrictId = districtId;

	loadCountries();

	if (selectedCountryId) {
		loadProvinces();
	}

	if (selectedProvinceId) {
		loadDistricts();
	}
}

function loadCountries() {
	var url = baseUrl + "api/address/countrieslocal";
	

	$.get(url).done(
		function (d) {
			var dropdown = $("#countrySelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedCountryId) {
				dropdown.val(selectedCountryId);
			}

			dropdown.on('change', function (e) {
				selectedCountryId = e.target.value;
				selectedProvinceId = '';
				selectedDistrictId = '';
				$("#provinceSelect").val('');
				$("#districtSelect").val('');
				loadProvinces();
			});
		}
	);

	
}

function loadProvinces() {
	var url = baseUrl + "api/address/provinces?countryId=" + selectedCountryId;

	$.get(url).done(
		function (d) {
			var dropdown = $("#provinceSelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedProvinceId) {
				dropdown.val(selectedProvinceId);
			}

			dropdown.on('change', function (e) {
				selectedProvinceId = e.target.value;
				selectedDistrictId = '';
				$("#districtSelect").val('');
				loadDistricts();
			});
		}
	);
}

function loadDistricts() {
	var url = baseUrl + "api/address/districts?provinceId=" + selectedProvinceId;

	$.get(url).done(
		function (d) {
			var dropdown = $("#districtSelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedDistrictId) {
				dropdown.val(selectedDistrictId);
			}
		}
	);
}

var selectedShippingCountryId, selectedShippingProvinceId, selectedShippingDistrictId,
	selectedDeliveryCountryId, selectedDeliveryProvinceId, selectedDeliveryDistrictId;

function initFreightAddressSelects(shippingCountryId, shippingProvinceId, shippingDistrictId, deliveryCountryId, deliveryProvinceId, deliveryDistrictId) {
	selectedShippingCountryId = shippingCountryId;
	selectedShippingProvinceId = shippingProvinceId;
	selectedShippingDistrictId = shippingDistrictId;
	selectedDeliveryCountryId = deliveryCountryId;
	selectedDeliveryProvinceId = deliveryProvinceId;
	selectedDeliveryDistrictId = deliveryDistrictId;

	loadShippingCountries();

	if (selectedShippingCountryId) {
		loadShippingProvinces();
	}

	if (selectedShippingProvinceId) {
		loadShippingDistricts();
	}

	loadDeliveryCountries();

	if (selectedDeliveryCountryId) {
		loadDeliveryProvinces();
	}

	if (selectedDeliveryProvinceId) {
		loadDeliveryDistricts();
	}
}

function loadShippingCountries() {
	var url = baseUrl + "api/address/countrieslocal";


	$.get(url).done(
		function (d) {
			var dropdown = $("#shippingCountrySelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedShippingCountryId) {
				dropdown.val(selectedShippingCountryId);
			}

			dropdown.on('change', function (e) {
				selectedShippingCountryId = e.target.value;
				selectedShippingProvinceId = '';
				selectedShippingDistrictId = '';
				$("#shippingProvinceSelect").val('');
				$("#shippingDistrictSelect").val('');
				loadShippingProvinces();
			});
		}
	);


}

function loadShippingProvinces() {
	var url = baseUrl + "api/address/provinces?countryId=" + selectedShippingCountryId;

	$.get(url).done(
		function (d) {
			var dropdown = $("#shippingProvinceSelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedShippingProvinceId) {
				dropdown.val(selectedShippingProvinceId);
			}

			dropdown.on('change', function (e) {
				selectedShippingProvinceId = e.target.value;
				selectedShippingDistrictId = '';
				$("#shippingDistrictSelect").val('');
				loadShippingDistricts();
			});
		}
	);
}

function loadShippingDistricts() {
	var url = baseUrl + "api/address/districts?provinceId=" + selectedShippingProvinceId;

	$.get(url).done(
		function (d) {
			var dropdown = $("#shippingDistrictSelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedShippingDistrictId) {
				dropdown.val(selectedShippingDistrictId);
			}
		}
	);
}

function loadDeliveryCountries() {
	var url = baseUrl + "api/address/countrieslocal";


	$.get(url).done(
		function (d) {
			var dropdown = $("#deliveryCountrySelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedDeliveryCountryId) {
				dropdown.val(selectedDeliveryCountryId);
			}

			dropdown.on('change', function (e) {
				selectedDeliveryCountryId = e.target.value;
				selectedDeliveryProvinceId = '';
				selectedDeliveryDistrictId = '';
				$("#deliveryProvinceSelect").val('');
				$("#deliveryDistrictSelect").val('');
				loadDeliveryProvinces();
			});
		}
	);


}

function loadDeliveryProvinces() {
	var url = baseUrl + "api/address/provinces?countryId=" + selectedDeliveryCountryId;

	$.get(url).done(
		function (d) {
			var dropdown = $("#deliveryProvinceSelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedDeliveryProvinceId) {
				dropdown.val(selectedDeliveryProvinceId);
			}

			dropdown.on('change', function (e) {
				selectedDeliveryProvinceId = e.target.value;
				selectedDeliveryDistrictId = '';
				$("#deliveryDistrictSelect").val('');
				loadDeliveryDistricts();
			});
		}
	);
}

function loadDeliveryDistricts() {
	var url = baseUrl + "api/address/districts?provinceId=" + selectedDeliveryProvinceId;

	$.get(url).done(
		function (d) {
			var dropdown = $("#deliveryDistrictSelect");
			dropdown.empty();
			dropdown.append($("<option />").val('').text('Seçiniz'));

			$.each(d, function () {
				dropdown.append($("<option />").val(this.Id).text(this.Name));
			});
			if (selectedDeliveryDistrictId) {
				dropdown.val(selectedDeliveryDistrictId);
			}
		}
	);
}