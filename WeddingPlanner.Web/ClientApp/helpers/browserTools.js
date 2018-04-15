﻿/* eslint prefer-destructuring: 0 */

export function addWarning() {
	document.getElementById("browser-warning").innerHTML =
		"You are using an <strong>outdated</strong> and " +
		"<strong>unsupported</strong> browser. " +
		"Please <a href=\"https://browsehappy.com/\">upgrade " +
		"your browser</a> to improve your experience and security.";
}

export function getBrowserInfo() {
	const browser = {};

	if (/edge\/[0-9]{2}/i.test(navigator.userAgent)) {
		browser.agent = "edge";
		browser.majorVersion =
			parseInt(/edge\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /edge\/([0-9.]+)/i.exec(navigator.userAgent)[1];
	} else if (/chrome\/[0-9]{2}/i.test(navigator.userAgent)) {
		browser.agent = "chrome";
		browser.majorVersion =
			parseInt(/chrome\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /chrome\/([0-9.]+)/i.exec(navigator.userAgent)[1];
	} else if (/firefox\/[0-9]{2}/i.test(navigator.userAgent)) {
		browser.agent = "firefox";
		browser.majorVersion =
			parseInt(/firefox\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /firefox\/([0-9.]+)/i.exec(navigator.userAgent)[1];
	} else if (/msie [0-9]{1}/i.test(navigator.userAgent)) {
		browser.agent = "msie";
		browser.majorVersion =
			parseInt(/MSIE ([0-9]{1})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /MSIE ([0-9.]+)/i.exec(navigator.userAgent)[1];
		addWarning();
	} else if (/opr\/[0-9]{2}/i.test(navigator.userAgent)) {
		browser.agent = "opera";
		browser.majorVersion =
			parseInt(/opr\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /opera\/([0-9.]+)/i.exec(navigator.userAgent)[1];
	} else if (/Trident\/[7]{1}/i.test(navigator.userAgent)) {
		browser.agent = "msie";
		browser.majorVersion = 11;
		browser.version = "11";
		addWarning();
	} else if (/Safari\/[0-9.]+/i.test(navigator.userAgent)) {
		browser.agent = "safari";
		browser.majorVersion =
			parseInt(/Version\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /Version\/([0-9.]+)/i.exec(navigator.userAgent)[1];
	} else {
		browser.agent = false;
		browser.majorVersion = false;
		browser.version = false;
	}

	if (/Windows NT/.test(navigator.userAgent)) {
		const winver =
			parseFloat(/Windows NT ([0-9]{1,2}\.[0-9]{1})/i
				.exec(navigator.userAgent)[1]);
		browser.os = "windows";
		switch (winver) {
		case 6.0:
			browser.osversion = "Vista";
			break;
		case 6.1:
			browser.osversion = "7";
			break;
		case 6.2:
			browser.osversion = "8";
			break;
		case 6.3:
			browser.osversion = "8.1";
			break;
		case 10.0:
			browser.osversion = "10";
			break;
		default:
			browser.osversion = false;
		}
	} else if (/OS X /.test(navigator.userAgent)) {
		browser.os = "os x";
		browser.osversion =
			/OS X [0-9]{2}_([0-9]{1,2})_[0-9]{1,2}/i
				.exec(navigator.userAgent)[1];
	} else if (/(Linux)/.test(navigator.userAgent)) {
		browser.os = "linux";
		browser.osversion = false;
	}

	return browser;
}
