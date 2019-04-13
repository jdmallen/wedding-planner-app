/* eslint prefer-destructuring: 0 */

export default function getBrowserInfo()
{
	const browser = {};

	if (/edge\/[0-9]{2}/i.test(navigator.userAgent))
	{
		browser.agent = "edge";
		browser.majorVersion =
			parseInt(/edge\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /edge\/([0-9.]+)/i.exec(navigator.userAgent)[1];
		browser.supported = true;
	}
	else if (/chrome\/[0-9]{2}/i.test(navigator.userAgent))
	{
		browser.agent = "chrome";
		browser.majorVersion =
			parseInt(/chrome\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /chrome\/([0-9.]+)/i.exec(navigator.userAgent)[1];
		browser.supported = true;
	}
	else if (/firefox\/[0-9]{2}/i.test(navigator.userAgent))
	{
		browser.agent = "firefox";
		browser.majorVersion =
			parseInt(/firefox\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /firefox\/([0-9.]+)/i.exec(navigator.userAgent)[1];
		browser.supported = true;
	}
	else if (/msie [0-9]{1}/i.test(navigator.userAgent))
	{
		browser.agent = "msie";
		browser.majorVersion =
			parseInt(/MSIE ([0-9]{1})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /MSIE ([0-9.]+)/i.exec(navigator.userAgent)[1];
		browser.supported = false;
	}
	else if (/opr\/[0-9]{2}/i.test(navigator.userAgent))
	{
		browser.agent = "opera";
		browser.majorVersion =
			parseInt(/opr\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /opera\/([0-9.]+)/i.exec(navigator.userAgent)[1];
		browser.supported = true;
	}
	else if (/Trident\/[7]{1}/i.test(navigator.userAgent))
	{
		browser.agent = "msie";
		browser.majorVersion = 11;
		browser.version = "11";
		browser.supported = false;
	}
	else if (/Safari\/[0-9.]+/i.test(navigator.userAgent))
	{
		browser.agent = "safari";
		browser.majorVersion =
			parseInt(/Version\/([0-9]{2})/i.exec(navigator.userAgent)[1], 10);
		browser.version = /Version\/([0-9.]+)/i.exec(navigator.userAgent)[1];
		browser.supported = true;
	}
	else
	{
		browser.agent = false;
		browser.majorVersion = false;
		browser.version = false;
		browser.supported = false;
	}

	return browser;
}
