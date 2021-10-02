# SimpleHTTP

Serves a default.htm .gif .jpg .swf or .js file from it's \wwwroot directory.

Included are a blank page, transparent 1x1 pixel gif, white 1x1 pixel jpg, and 1x1 pixel flash file.

You can modify these to be whatever you like, as long as the filename remains default.x

Speeds up surfing by answering the request for blocked content.

Useful with HOSTS files or DNS Redirector software.

Alternate service name, in order to run multiple instances on the same server:

- SimpleHTTP2 v3.0.0.3
- SimpleHTTP3 v3.0.0.3
- SimpleHTTP4 v3.0.0.3

Tested on Windows XP, Server 2003, Vista, Server 2008, Windows 7, Windows 8

A scripted install/uninstall is not included with this software.

This program runs as a service; without any GUI, taskbar, or system tray icon.

## Installation

1) Ensure the Microsoft .NET Framework 4.x is installed
2) Right-click on the .zip file you downloaded, select properties, click the Unblock button (if this button is not present just proceed)
3) Extract the contents of the .zip file (usually to C:\SimpleHTTP)
4) Edit SimpleHTTP.ini to include the IP of your server and path to the default.x files (usually C:\SimpleHTTP\wwwroot)
5) Right-click on _setup_.bat and select "Run as administrator" to install the service (see note at bottom of this text)

Anytime you make a change in SimpleHTTP.ini you must restart the service for it to take effect.

## Usage

SimpleHTTP will also serve the following files when requested and found in the path...

- /default001.htm through /default005.htm
- /default001.gif through /default005.gif
- /default001.jpg through /default005.jpg
- /default001.swf through /default005.swf

No GUI is displayed, check Task Manager or tcpview to see that it's running.

Note: Under Windows Vista and later OS versions you may receive an error (HRESULT: 0x80131515) when installing the service.
The solution is to save the .zip file, right-click on it, select properties, and click the Unblock button.
Then extract the contents of the .zip file. Right-click on _setup_.bat and select "Run as administrator" and it will work.

## Unintended usage to stop browser hang

Since the new norm of all sites having to be HTTPS, Firefox and other browsers will hang or pause while they attempt to reach your site via HTTPS before actually loading the page via HTTP.  This is extreamly annoying for an internal-only HTTP-only site hosted within your LAN.  You can speed up page loading by running an instance of SimpleHTTP with Port=443  Note that SimpleHTTP DOES NOT actually support HTTPS, but because the response on port 443 will be "unexpected" the browser will give-up more quickly and revert to loading your site via HTTP the way you intend.  This is also useful with various Internet filtering methods where a blocked site's DNS will resolve to that of a non-HTTPS site normally hosting the "Blocked by content filter" message.  For example if BlockedIP=192.168.4.4 in dnsredir.ini then use IP=192.168.4.4 and Port=443 in simplehttp.ini  The normal "Blocked by content filter" message will not appear in the browser but rather a "Secure Connection Failed" (or similar message) will - which should be an indication to end-users that the website is blocked - all without having to forge SSL certs or mistrain users that accepting a self-signed certificate is an acceptable thing to do.

## License

GPL does not allow you to link GPL-licensed components with other proprietary software (unless you publish as GPL too).

GPL does not allow you to modify the GPL code and make the changes proprietary, so you cannot use GPL code in your non-GPL projects.

If you wish to integrate this software into your commercial software package, or you are a corporate entity with more than 10 employees, then you should obtain a per-instance license, or a site-wide license, from http://jpelectron.com/buy

For all other use cases please consider: <a href='https://ko-fi.com/C0C54S4JF' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://cdn.ko-fi.com/cdn/kofi2.png?v=2' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>

[End of Line]
