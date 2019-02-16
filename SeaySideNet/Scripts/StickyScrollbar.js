function stickyScrollBar(options) {
	//if (EncompassBootstrap.IsBrowserIE()) {
	//	return;
	//}
	sizeFakeScrollbar(options);

	var fakeScroll = $('*[data-fakescroll="' + options.tableId + '"]');
	var realScroll = $('*[data-realscroll="' + options.tableId + '"]');
	fakeScroll.scroll(function () {
		realScroll.scrollLeft(fakeScroll.scrollLeft());
	});
	realScroll.scroll(function () {
		fakeScroll.scrollLeft(realScroll.scrollLeft());
	});

	toggleFakeScrollbar(options);

	$(window).on('scroll', function () {
		setTimeout(function () { toggleFakeScrollbar(options); }, 5);
	});
	$(window).on('resize', function () {
		setTimeout(function () { sizeFakeScrollbar(options); toggleFakeScrollbar(options); }, 5);
	});
	$(window).on('zoom', function () {
		setTimeout(function () { sizeFakeScrollbar(options); toggleFakeScrollbar(options); }, 5);
	});
	$('.collapse').on('hidden.bs.collapse', function () {
		setTimeout(function () { sizeFakeScrollbar(options); toggleFakeScrollbar(options); }, 5);
	});
	$('.collapse').on('shown.bs.collapse', function () {
		setTimeout(function () { sizeFakeScrollbar(options); toggleFakeScrollbar(options); }, 5);
	});

	var cpvPrm = Sys.WebForms.PageRequestManager.getInstance();
	if (cpvPrm) {
		cpvPrm.add_endRequest(function () {
			$(options.selector).floatThead("reflow");
			setTimeout(function () { sizeFakeScrollbar(options); toggleFakeScrollbar(options); }, 5);
		});
	}
}

function sizeFakeScrollbar(options) {
	var fakeScroll = $('*[data-fakescroll="' + options.tableId + '"]');
	var realScroll = $('*[data-realscroll="' + options.tableId + '"]');
	fakeScroll.attr("style", "max-width: " + $(options.selector).parent().width() + "px; left: " + realScroll.offset().left + "px;");
	fakeScroll.find('div').attr("style", "height: 1px; width: " + $(options.selector).width() + "px;");
}

function toggleFakeScrollbar(options) {
	var fakeScroll = $('*[data-fakescroll="' + options.tableId + '"]');
	var realScroll = $('*[data-realscroll="' + options.tableId + '"]');
	if ($(options.selector).find('tbody').isInViewport() && realScroll.bottom() > windowBottom()) {
		if (fakeScroll.hasClass('d-none')) {
			fakeScroll.removeClass('d-none');
			fakeScroll.scrollLeft(realScroll.scrollLeft());
		}
	} else {
		fakeScroll.addClass('d-none');
	}
}